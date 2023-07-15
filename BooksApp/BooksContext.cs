using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace BooksApp
{
    public partial class BooksContext : DbContext
    {
        public BooksContext()
        {
        }

        public BooksContext(DbContextOptions<BooksContext> options)
            : base(options)
        {
        }

        public  DbSet<Autorzy> Autorzies { get; set; }

        public  DbSet<Kategorie> Kategories { get; set; }

        public  DbSet<Książki> Książkis { get; set; }

        public  DbSet<Wydawnictwa> Wydawnictwas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            optionsBuilder.UseSqlServer("Data Source=TADEUSZ;Initial Catalog=Books;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autorzy>(entity =>
            {
                entity.HasKey(e => e.AutorId).HasName("PK__Autorzy__F58AE929F864FD9A");

                entity.ToTable("Autorzy");

                entity.Property(e => e.Imię).HasMaxLength(50);
                entity.Property(e => e.Nazwisko).HasMaxLength(50);
            });

            modelBuilder.Entity<Kategorie>(entity =>
            {
                entity.HasKey(e => e.KategoriaId).HasName("PK__Kategori__37D2108CA35242ED");

                entity.ToTable("Kategorie");

                entity.Property(e => e.Nazwa).HasMaxLength(50);
            });

            modelBuilder.Entity<Książki>(entity =>
            {
                entity.HasKey(e => e.KsiążkaId).HasName("PK__Książki__9E5BAE9FDD762909");

                entity.ToTable("Książki");

                entity.Property(e => e.Tytuł).HasMaxLength(100);

                entity.HasOne(d => d.Autor).WithMany(p => p.Książkis)
                    .HasForeignKey(d => d.AutorId)
                    .HasConstraintName("FK__Książki__AutorId__2D27B809");

                entity.HasOne(d => d.Kategoria).WithMany(p => p.Książkis)
                    .HasForeignKey(d => d.KategoriaId)
                    .HasConstraintName("FK__Książki__Kategor__2F10007B");

                entity.HasOne(d => d.Wydawnictwo).WithMany(p => p.Książkis)
                    .HasForeignKey(d => d.WydawnictwoId)
                    .HasConstraintName("FK__Książki__Wydawni__2E1BDC42");
            });

            modelBuilder.Entity<Wydawnictwa>(entity =>
            {
                entity.HasKey(e => e.WydawnictwoId).HasName("PK__Wydawnic__EEE56BCE02ED15F2");

                entity.ToTable("Wydawnictwa");

                entity.Property(e => e.Nazwa).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
