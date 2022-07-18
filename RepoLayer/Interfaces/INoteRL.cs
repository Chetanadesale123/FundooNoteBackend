using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Interfaces
{
    public interface INoteRL
    {
        public NoteEntity AddNote(NotePostModel notes, long userid);
        public bool DeleteNote(long noteid);
        public NoteEntity UpdateNotes(NotePostModel notes, long noteid);
        public NoteEntity IsPinnedORNot(long noteid);
        public NoteEntity IsArchivedORNot(long noteid);
        IEnumerable<NoteEntity> GetAllNotes();
        IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid);
        public NoteEntity UploadImage(long noteid, IFormFile img);
        //void UploadFile(List<IFormFile> files, string subDirectory);
        //(string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory);
        //string SizeConverter(long bytes);
    }
}

