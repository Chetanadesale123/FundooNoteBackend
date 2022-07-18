using Microsoft.AspNetCore.Http;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepoLayer.Entities
{
    public class NoteEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long noteid { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime reminder { get; set; }
        public string colour { get; set; }
        public string image { get; set; }
        // public string subDirectory { get; set; }
        public bool isArchived { get; set; }
        public bool isPinned { get; set; }
        public bool isDeleted { get; set; }
        public DateTime? createAt { get; set; }//nullable
        public DateTime? editAt { get; set; }
        [ForeignKey("Users")]
        public long userid { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
