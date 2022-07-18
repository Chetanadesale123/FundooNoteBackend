using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.Model
{
    public class NotePostModel
    {
        public string title { get; set; }
        public string description { get; set; }
        public DateTime reminder { get; set; }
        public string colour { get; set; }
        public string image { get; set; }
        // public string subDirectory { get; set; }
        public bool isArchived { get; set; }
        public bool isPinned { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? EditedAt { get; set; }
    }
}
