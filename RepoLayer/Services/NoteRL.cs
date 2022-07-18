using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonLayer.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepoLayer.Context;
using RepoLayer.Entities;
using RepoLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLayer.Services
{
    public class NoteRL : INoteRL
    {
        FundooContext fundooContext;
        private readonly IConfiguration config;
        public const string CLOUD_NAME = "imageupl";
        public const string API_KEY = "913737481261618";
        public const string API_Secret = "aedXJOOgdxKLFdmWGx8p6_RT6vQ";
        public static Cloudinary cloud;
        //private IHostingEnvironment _hostingEnvironment;
        public NoteRL(FundooContext fundooContext, IConfiguration config)
        {
            this.config = config;
            this.fundooContext = fundooContext;
        }
        public NoteEntity AddNote(NotePostModel notes, long userid)
        {
            try
            {
                NoteEntity noteEntity = new NoteEntity();
                noteEntity.title = notes.title;
                noteEntity.description = notes.description;
                noteEntity.reminder = notes.reminder;
                noteEntity.colour = notes.colour;
                noteEntity.image = notes.image;
                noteEntity.isArchived = notes.isArchived;
                noteEntity.isPinned = notes.isPinned;
                noteEntity.isDeleted = notes.IsDeleted;
                noteEntity.userid = userid;
                noteEntity.createAt = notes.CreatedAt;
                noteEntity.editAt = notes.EditedAt;
                // noteEntity.subDirectory = notes.subDirectory;
                this.fundooContext.NotesTable.Add(noteEntity);
                int result = this.fundooContext.SaveChanges();
                if (result > 0)
                {
                    return noteEntity;
                }
                return null;
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
                var result = this.fundooContext.NotesTable.FirstOrDefault(x => x.noteid == noteid);
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
        public NoteEntity UpdateNotes(NotePostModel notes, long noteid)
        {
            try
            {
                NoteEntity result = fundooContext.NotesTable.Where(e => e.noteid == noteid).FirstOrDefault();
                if (result != null)
                {
                    result.title = notes.title;
                    result.description = notes.description;
                    result.colour = notes.colour;
                    result.image = notes.image;
                    result.isArchived = notes.isArchived;
                    // result.subDirectory = notes.subDirectory;
                    result.isPinned = notes.isPinned;
                    fundooContext.NotesTable.Update(result);
                    fundooContext.SaveChanges();
                    return result;
                }
                return null;
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
                NoteEntity result = this.fundooContext.NotesTable.FirstOrDefault(x => x.noteid == noteid);
                if (result.isPinned == true)
                {
                    result.isPinned = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.isPinned = true;
                this.fundooContext.SaveChanges();
                return null;
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
                NoteEntity result = this.fundooContext.NotesTable.FirstOrDefault(x => x.noteid == noteid);
                if (result.isArchived == true)
                {
                    result.isArchived = false;
                    this.fundooContext.SaveChanges();
                    return result;
                }
                result.isArchived = true;
                this.fundooContext.SaveChanges();
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public IEnumerable<NoteEntity> GetAllNotes()
        {
            return fundooContext.NotesTable.ToList();
        }
        public IEnumerable<NoteEntity> GetAllNotesbyuserid(long userid)
        {
            return fundooContext.NotesTable.Where(n => n.userid == userid).ToList();
        }
        public NoteEntity UploadImage(long noteid, IFormFile img)
        {
            try
            {
                var noteId = this.fundooContext.NotesTable.FirstOrDefault(e => e.noteid == noteid);
                if (noteId != null)
                {
                    Account acc = new Account(CLOUD_NAME, API_KEY, API_Secret);
                    cloud = new Cloudinary(acc);
                    var imagePath = img.OpenReadStream();
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(img.FileName, imagePath)
                    };
                    var uploadresult = cloud.Upload(uploadParams);
                    noteId.image = img.FileName;
                    fundooContext.NotesTable.Update(noteId);
                    int upload = fundooContext.SaveChanges();
                    if (upload > 0)
                    {
                        return noteId;
                    }
                }
                return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public void UploadFile(List<IFormFile> files, string subDirectory)
        //{
        //        subDirectory = subDirectory ?? string.Empty;
        //        var target = Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory);

        //        Directory.CreateDirectory(target);

        //        files.ForEach(async file =>
        //        {
        //            if (file.Length <= 0) return;
        //            var filePath = Path.Combine(target, file.FileName);
        //            using (var stream = new FileStream(filePath, FileMode.Create))
        //            {
        //                await file.CopyToAsync(stream);
        //            }
        //        });
        //}
        //public (string fileType, byte[] archiveData, string archiveName) DownloadFiles(string subDirectory)
        //{
        //        var zipName = $"archive-{DateTime.Now.ToString("yyyy_MM_dd-HH_mm_ss")}.zip";

        //        var files = Directory.GetFiles(Path.Combine(_hostingEnvironment.ContentRootPath, subDirectory)).ToList();

        //        using (var memoryStream = new MemoryStream())
        //        {
        //            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
        //            {
        //                files.ForEach(file =>
        //                {
        //                    var theFile = archive.CreateEntry(file);
        //                    using (var streamWriter = new StreamWriter(theFile.Open()))
        //                    {
        //                        streamWriter.Write(File.ReadAllText(file));
        //                    }

        //                });
        //            }

        //            return ("application/zip", memoryStream.ToArray(), zipName);
        //        }
        //}
        //public string SizeConverter(long bytes)
        //{
        //        var fileSize = new decimal(bytes);
        //        var kilobyte = new decimal(1024);
        //        var megabyte = new decimal(1024 * 1024);
        //        var gigabyte = new decimal(1024 * 1024 * 1024);

        //        switch (fileSize)
        //        {
        //            case var _ when fileSize < kilobyte:
        //                return $"Less then 1KB";
        //            case var _ when fileSize < megabyte:
        //                return $"{Math.Round(fileSize / kilobyte, 0, MidpointRounding.AwayFromZero):##,###.##}KB";
        //            case var _ when fileSize < gigabyte:
        //                return $"{Math.Round(fileSize / megabyte, 2, MidpointRounding.AwayFromZero):##,###.##}MB";
        //            case var _ when fileSize >= gigabyte:
        //                return $"{Math.Round(fileSize / gigabyte, 2, MidpointRounding.AwayFromZero):##,###.##}GB";
        //            default:
        //                return "n/a";
        //        }
        //}
    }
}


