using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using RepoLayer.Context;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CollabController : ControllerBase
    {
        ICollabeBL collab;
        private readonly IMemoryCache memoryCache;
        // private readonly ApplicationDbContext context;
        FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        public CollabController(ICollabeBL collab, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.collab = collab;
            this.memoryCache = memoryCache;
            this.fundooContext = fundooContext; ;
            this.distributedCache = distributedCache;
        }
        [HttpPost("Add")]
        public IActionResult AddCollab(long noteid, string email)
        {
            try
            {
                long userid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                var result = collab.AddCollab(noteid, userid, email);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Collaborator Added Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to add"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpDelete("Remove")]
        public IActionResult Remove(long CollabId)
        {
            try
            {
                if (collab.Remove(CollabId))
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Deleted Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to Delete"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("Noteid")]
        public IEnumerable<CollabeEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return collab.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllCollabeUsingRedisCache()
        {
            var cacheKey = "CollabeList";
            string serializedCollabeList;
            var CollabeList = new List<CollabeEntity>();
            var redisCollabeList = await distributedCache.GetAsync(cacheKey);
            if (redisCollabeList != null)
            {
                serializedCollabeList = Encoding.UTF8.GetString(redisCollabeList);
                CollabeList = JsonConvert.DeserializeObject<List<CollabeEntity>>(serializedCollabeList);
            }
            else
            {
                CollabeList = await fundooContext.CollabeTable.ToListAsync();
                serializedCollabeList = JsonConvert.SerializeObject(CollabeList);
                redisCollabeList = Encoding.UTF8.GetBytes(serializedCollabeList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisCollabeList, options);
            }
            return Ok(CollabeList);
        }
    }
}
