using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface INoteBL
    {
        public NoteEntity AddNote(NotePostModel node, long userid);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NotePostModel notes, long noteid);
        public NoteEntity IsPinnedORNot(long noteid);
        public NoteEntity IsArchivedORNot(long noteid);
        IEnumerable<NoteEntity> GetAllNotes();
        IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public NoteEntity UploadImage(long noteid, IFormFile img);
        //public void UploadFile(List<IFormFile> files, string subDirectory);
        //public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);
        //public string SizeConverter(long bytes);
    }
}
