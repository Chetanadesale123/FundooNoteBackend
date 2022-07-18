using BusinessLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Http;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class NoteBL : INoteBL
    {
        INoteRL noteRL;
        public NoteBL(INoteRL noteRL)
        {
            this.noteRL = noteRL;
        }
        public NoteEntity AddNote(NotePostModel noteModel, long userid)
        {
            try
            {
                return this.noteRL.AddNote(noteModel, userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool DeleteNote(long noteid)
        {
            try
            {
                return this.noteRL.DeleteNote(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UpdateNotes(NotePostModel noteModel, long noteid)
        {
            try
            {
                return this.noteRL.UpdateNotes(noteModel, noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity IsPinnedORNot(long noteid)
        {
            try
            {
                return this.noteRL.IsPinnedORNot(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity IsArchivedORNot(long noteid)
        {
            try
            {
                return this.noteRL.IsArchivedORNot(noteid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            try
            {
                return this.noteRL.GetAllNotes();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            try
            {
                return this.noteRL.GetAllNotesbyuserid(userid);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public NoteEntity UploadImage(long noteid, IFormFile img)
        {
            try
            {
                return this.noteRL.UploadImage(noteid, img);
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public void UploadFile(List<IFormFile> files, string subDirectory)
        //{
        //    try
        //    {
        //         this.noteRL.UploadFile(files,subDirectory);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        //{
        //    try
        //    {
        //        return this.noteRL.DownloadFiles(subDirectory);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
        //public string SizeConverter(long bytes)
        //{
        //    try
        //    {
        //        return this.noteRL.SizeConverter(bytes);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //}
    }
}

