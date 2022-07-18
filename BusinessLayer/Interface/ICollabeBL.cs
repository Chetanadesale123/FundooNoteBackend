using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
namespace BusinessLayer.Interface
{
    public interface ICollabeBL
    {
        public CollabeEntity AddCollab(long noteid, long userid, string email);
        public bool Remove(long CollabId);
        IEnumerable<CollabeEntity> GetAllByNoteID(long noteid);
    }
}
