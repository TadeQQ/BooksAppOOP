using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CertificationsApp;

public partial class TasksMenagerContext : DbContext
{
    public TasksMenagerContext()
    {
    }

    public TasksMenagerContext(DbContextOptions<TasksMenagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Kategorie> Kategories { get; set; }

    public virtual DbSet<Tagi> Tagis { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<TaskTag> TaskTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=TADEUSZ;Initial Catalog=tasksMenager;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Kategorie>(entity =>
        {
            entity.HasKey(e => e.KategoriaId).HasName("PK__Kategori__37D2108CFB80EC8F");

            entity.ToTable("Kategorie");

            entity.Property(e => e.KategoriaId).ValueGeneratedNever();
            entity.Property(e => e.Nazwa)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Tagi>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PK__Tagi__657CF9AC9D238B10");

            entity.ToTable("Tagi");

            entity.Property(e => e.TagId).ValueGeneratedNever();
            entity.Property(e => e.Nazwa)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.TaskId).HasName("PK__Tasks__7C6949B1F1F034DD");

            entity.Property(e => e.TaskId).ValueGeneratedNever();
            entity.Property(e => e.DataDodania).HasColumnType("datetime");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Opis)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TerminWykonania).HasColumnType("datetime");
        });

        modelBuilder.Entity<TaskTag>(entity =>
        {
            entity.HasKey(e => e.TaskTagId).HasName("PK__TaskTag__6DB2488F7A05D484");

            entity.ToTable("TaskTag");

            entity.Property(e => e.TaskTagId).ValueGeneratedNever();

            entity.HasOne(d => d.Tag).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TagId)
                .HasConstraintName("FK__TaskTag__TagId__2B3F6F97");

            entity.HasOne(d => d.Task).WithMany(p => p.TaskTags)
                .HasForeignKey(d => d.TaskId)
                .HasConstraintName("FK__TaskTag__TaskId__2A4B4B5E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
