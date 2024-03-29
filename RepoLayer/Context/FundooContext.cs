﻿using Microsoft.EntityFrameworkCore;
using RepoLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepoLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options)
                : base(options)
            {
            }
        public DbSet <UserEntity> Users { get; set; }
        public DbSet<NoteEntity> NotesTable { get; set; }
        public DbSet<CollabeEntity> CollabeTable { get; set; }
        public DbSet<LabelEntity> Labels { get; set; }
    }
}
