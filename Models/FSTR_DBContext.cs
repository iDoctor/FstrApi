﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FstrApi.Models
{
    public partial class FSTR_DBContext : DbContext
    {
        public FSTR_DBContext()
        {
        }

        public FSTR_DBContext(DbContextOptions<FSTR_DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<PerevalAdded> PerevalAddeds { get; set; } = null!;
        public virtual DbSet<PerevalArea> PerevalAreas { get; set; } = null!;
        public virtual DbSet<PerevalImage> PerevalImages { get; set; } = null!;
        public virtual DbSet<SprActivitiesType> SprActivitiesTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=FSTR_DB;Username=postovski;Password=12345");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PerevalAdded>(entity =>
            {
                entity.ToTable("pereval_added");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('untitled_table_195_id_seq'::regclass)");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_added");

                entity.Property(e => e.Images)
                    .HasColumnType("json")
                    .HasColumnName("images");

                entity.Property(e => e.RawData)
                    .HasColumnType("json")
                    .HasColumnName("raw_data");
            });

            modelBuilder.Entity<PerevalArea>(entity =>
            {
                entity.ToTable("pereval_areas");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdParent).HasColumnName("id_parent");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.Entity<PerevalImage>(entity =>
            {
                entity.ToTable("pereval_images");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('pereval_added_id_seq'::regclass)");

                entity.Property(e => e.DateAdded)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("date_added")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Img).HasColumnName("img");
            });

            modelBuilder.Entity<SprActivitiesType>(entity =>
            {
                entity.ToTable("spr_activities_types");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('untitled_table_200_id_seq'::regclass)");

                entity.Property(e => e.Title).HasColumnName("title");
            });

            modelBuilder.HasSequence("id_seq");

            modelBuilder.HasSequence("pereval_areas_id_seq");

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}