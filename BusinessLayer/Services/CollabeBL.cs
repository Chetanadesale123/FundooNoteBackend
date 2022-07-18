using BusinessLayer.Interface;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CollabeBL:ICollabeBL
    {
        ICollabRL collabRL;
        public CollabeBL(ICollabRL collabRL)
        {
            this.collabRL = collabRL;
        }
        public CollabeEntity AddCollab(long noteid, long userid, string email)
        {
            try
            {
                return this.collabRL.AddCollab(noteid, userid, email);
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
                return this.collabRL.Remove(CollabId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<CollabeEntity> GetAllByNoteID(long noteid)
        {
            try
            {
                return this.collabRL.GetAllByNoteID(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
