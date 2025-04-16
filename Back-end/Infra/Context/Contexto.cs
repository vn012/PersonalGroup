using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Domain.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aplication.Enum;
using System.Reflection.Emit;
using System.Xml.Linq;

namespace Infra.Contexto
{
    public class Context : DbContext
    {
        private readonly IConfiguration _configuration;

        public Context()
        {

        }

        public Context(DbContextOptions<Context> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=personal_group;Username=postgres;Password=lala123;Timeout=10;SslMode=Prefer;");
        }

        public DbSet<Note> Note { get; set; }
        public DbSet<NoteMedia> NoteMedia { get; set; }
        public DbSet<NoteTags> NoteTag { get; set; }
        public DbSet<MediaType> MediaType { get; set; }
        public DbSet<Tag> Tag { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Note>()
                .HasMany(n => n.MediaItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<NoteTags>()
                .HasKey(nt => new { nt.NoteId, nt.TagId });

            modelBuilder.Entity<NoteMedia>()
                .HasOne(nm => nm.Note)
                .WithMany(n => n.MediaItems)
                .HasForeignKey(nm => nm.NoteId);

            modelBuilder.Entity<NoteMedia>()
                .HasOne(nm => nm.MediaType)
                .WithMany()
                .HasForeignKey(nm => nm.MediaTypeId);

            modelBuilder.Entity<MediaType>().HasData(
                new MediaType { Id = (int)MediaTypeEnum.Link, Name = "Link", Description = "Hyperlink" },
                new MediaType { Id = (int)MediaTypeEnum.Image, Name = "Image", Description = "Imagem" },
                new MediaType { Id = (int)MediaTypeEnum.Pdf, Name = "PDF", Description = "Documento PDF" },
                new MediaType { Id = (int)MediaTypeEnum.Video, Name = "Video", Description = "Vídeo" },
                new MediaType { Id = (int)MediaTypeEnum.Audio, Name = "Audio", Description = "Áudio" },
                new MediaType { Id = (int)MediaTypeEnum.Other, Name = "Other", Description = "Outro" }
            );

        }
    };
}