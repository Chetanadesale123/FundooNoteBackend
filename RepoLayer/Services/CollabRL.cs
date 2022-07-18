using CommonLayer.Model;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class CollabRL: ICollabRL
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;
        public CollabRL(FundooContext fundooContext, IConfiguration config)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public CollabeEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                CollabeEntity Entity = new CollabeEntity();
                Entity.CollabeEmail = email;
                Entity.userid = userid;
                Entity.noteid = noteid;
                this.fundooContext.CollabeTable.Add(Entity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return Entity;
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool Remove(long CollabId)
        {
            try
            {
                var result = this.fundooContext.CollabeTable.FirstOrDefault(x => x.CollabId == CollabId);
                fundooContext.Remove(result);
                int deletednote = this.fundooContext.SaveChanges();
                if (deletednote > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollabeEntity> GetAllByNoteID(long noteid)
        {
            return fundooContext.CollabeTable.Where(n => n.noteid == noteid).ToList();
        }
    }
}

