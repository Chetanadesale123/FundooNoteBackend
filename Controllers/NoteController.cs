using BusinessLayer.Interface;
using CommonLayer.Model;
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
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundooNotes.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMemoryCache memoryCache;
        // private readonly ApplicationDbContext context;
        FundooContext fundooContext;
        private readonly IDistributedCache distributedCache;
        INoteBL inoteBl;
        public NoteController(INoteBL inoteBl, IMemoryCache memoryCache, FundooContext fundooContext, IDistributedCache distributedCache)
        {
            this.inoteBl = inoteBl;
            this.memoryCache = memoryCache;
            this.fundooContext = fundooContext; ;
            this.distributedCache = distributedCache;
        }
        [Authorize]
        [HttpPost("Add")]
        public IActionResult AddNotes(NotePostModel addnote)
        {
            try
            {
                long noteid = Convert.ToInt32(User.Claims.First(e => e.Type == "id").Value);
                var result = inoteBl.AddNote(addnote, noteid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Added Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to add note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpDelete("Delete")]
        public IActionResult DeleteNotes(long noteid)
        {
            try
            {
                if (inoteBl.DeleteNote(noteid))
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Deleted Successfully"
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to delete note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Update")]
        public IActionResult updateNotes(NotePostModel addnote, long noteid)
        {
            try
            {
                var result = inoteBl.UpdateNotes(addnote, noteid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        Success = true,
                        message = "Note Updated Successfully",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        Success = false,
                        message = "Unable to Update note"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Pin")]
        public IActionResult Ispinnedornot(long noteid)
        {
            try
            {
                var result = inoteBl.IsPinnedORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note unPinned ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note Pinned Successfully"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Archive")]
        public IActionResult IsArchivedORNot(long noteid)
        {
            try
            {
                var result = inoteBl.IsArchivedORNot(noteid);
                if (result != null)
                {
                    return this.Ok(new
                    {
                        message = "Note Unarchived ",
                        Response = result
                    });
                }
                else
                {
                    return this.BadRequest(new
                    {
                        message = "Note Archived Successfully"
                    });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("AllNotes")]
        public IEnumerable<NoteEntity> GetAllNote()
        {
            try
            {
                return inoteBl.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpGet("UserId")]
        public IEnumerable<NoteEntity> GetAllNotesbyuser(long userid)
        {
            try
            {
                return inoteBl.GetAllNotesbyuserid(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        [Authorize]
        [HttpPut("Upload")]
        public IActionResult UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var result = inoteBl.UploadImage(noteid, img);
                if (result != null)
                {
                    return this.Ok(new { message = "uploaded ", Response = result });
                }
                else
                {
                    return this.BadRequest(new { message = "Not uploaded" });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpGet("redis")]
        public async Task<IActionResult> GetAllNotesUsingRedisCache()
        {
            var cacheKey = "noteList";
            string serializednoteList;
            var noteList = new List<NoteEntity>();
            var redisnoteList = await distributedCache.GetAsync(cacheKey);
            if (redisnoteList != null)
            {
                serializednoteList = Encoding.UTF8.GetString(redisnoteList);
                noteList = JsonConvert.DeserializeObject<List<NoteEntity>>(serializednoteList);
            }
            else
            {
                noteList = await fundooContext.NotesTable.ToListAsync();
                serializednoteList = JsonConvert.SerializeObject(noteList);
                redisnoteList = Encoding.UTF8.GetBytes(serializednoteList);
                var options = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));
                await distributedCache.SetAsync(cacheKey, redisnoteList, options);
            }
            return Ok(noteList);
        }
        //[Authorize]
        //[HttpPost(nameof(Upload))]
        //    public IActionResult Upload([Required] List<IFormFile> formFiles, [Required] string subDirectory)
        //    {
        //        try
        //        {
        //        inoteBl.UploadFile(formFiles, subDirectory);

        //            return Ok(new { formFiles.Count, Size = inoteBl.SizeConverter(formFiles.Sum(f => f.Length)) });
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //    }


        //[Authorize]
        //[HttpGet(nameof(Download))]
        //    public IActionResult Download([Required] string subDirectory)
        //    {

        //        try
        //        {
        //            var (fileType, archiveData, archiveName) = inoteBl.DownloadFiles(subDirectory);

        //            return File(archiveData, fileType, archiveName);
        //        }
        //        catch (Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }

        //    }
    }
}
