using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;
using PurposeBuddy.Models.Entities;

namespace PurposeBuddy.Models;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chapter> Chapters { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Muxdatum> Muxdata { get; set; }

    public virtual DbSet<Purchase> Purchases { get; set; }

    public virtual DbSet<Stripecustomer> Stripecustomers { get; set; }

    public virtual DbSet<Userprogress> Userprogresses { get; set; }

    /*    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseMySql("server=localhost;port=3306;database=project;user=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));*/

    //No Need to Override 

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("attachment")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CourseId, "Attachment_courseId_idx");

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(191)
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.Url)
                .HasColumnType("text")
                .HasColumnName("url");

            entity.HasOne(d => d.Course).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("Attachment_courseId_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("category")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.Name, "Category_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(191)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Chapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("chapter")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CourseId, "Chapter_courseId_idx");

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(191)
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.IsFree).HasColumnName("isFree");
            entity.Property(e => e.IsPublished).HasColumnName("isPublished");
            entity.Property(e => e.Position).HasColumnName("position");
            entity.Property(e => e.Title)
                .HasMaxLength(191)
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.VideoUrl)
                .HasColumnType("text")
                .HasColumnName("videoUrl");

            entity.HasOne(d => d.Course).WithMany(p => p.Chapters)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("Chapter_courseId_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("course")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CategoryId, "Course_categoryId_idx");

            entity.HasIndex(e => e.Title, "Course_title_idx").HasAnnotation("MySql:FullTextIndex", true);

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.CategoryId)
                .HasMaxLength(191)
                .HasColumnName("categoryId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.ImageUrl)
                .HasColumnType("text")
                .HasColumnName("imageUrl");
            entity.Property(e => e.IsPublished).HasColumnName("isPublished");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Title)
                .HasColumnType("text")
                .HasColumnName("title");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(191)
                .HasColumnName("userId");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("Course_categoryId_fkey");
        });

        modelBuilder.Entity<Muxdatum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("muxdata")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "MuxData_chapterId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.AssetId)
                .HasMaxLength(191)
                .HasColumnName("assetId");
            entity.Property(e => e.ChapterId)
                .HasMaxLength(191)
                .HasColumnName("chapterId");
            entity.Property(e => e.PlaybackId)
                .HasMaxLength(191)
                .HasColumnName("playbackId");

            entity.HasOne(d => d.Chapter).WithOne(p => p.Muxdatum)
                .HasForeignKey<Muxdatum>(d => d.ChapterId)
                .HasConstraintName("MuxData_chapterId_fkey");
        });

        modelBuilder.Entity<Purchase>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("purchase")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.CourseId, "Purchase_courseId_idx");

            entity.HasIndex(e => new { e.UserId, e.CourseId }, "Purchase_userId_courseId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.CourseId)
                .HasMaxLength(191)
                .HasColumnName("courseId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(191)
                .HasColumnName("userId");

            entity.HasOne(d => d.Course).WithMany(p => p.Purchases)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("Purchase_courseId_fkey");
        });

        modelBuilder.Entity<Stripecustomer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("stripecustomer")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.StripeCustomerId, "StripeCustomer_stripeCustomerId_key").IsUnique();

            entity.HasIndex(e => e.UserId, "StripeCustomer_userId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.StripeCustomerId)
                .HasMaxLength(191)
                .HasColumnName("stripeCustomerId");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(191)
                .HasColumnName("userId");
        });

        modelBuilder.Entity<Userprogress>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("userprogress")
                .UseCollation("utf8mb4_unicode_ci");

            entity.HasIndex(e => e.ChapterId, "UserProgress_chapterId_idx");

            entity.HasIndex(e => new { e.UserId, e.ChapterId }, "UserProgress_userId_chapterId_key").IsUnique();

            entity.Property(e => e.Id)
                .HasMaxLength(191)
                .HasColumnName("id");
            entity.Property(e => e.ChapterId)
                .HasMaxLength(191)
                .HasColumnName("chapterId");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(3)")
                .HasColumnType("datetime(3)")
                .HasColumnName("createdAt");
            entity.Property(e => e.IsCompleted).HasColumnName("isCompleted");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime(3)")
                .HasColumnName("updatedAt");
            entity.Property(e => e.UserId)
                .HasMaxLength(191)
                .HasColumnName("userId");

            entity.HasOne(d => d.Chapter).WithMany(p => p.Userprogresses)
                .HasForeignKey(d => d.ChapterId)
                .HasConstraintName("UserProgress_chapterId_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
