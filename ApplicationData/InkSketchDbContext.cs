using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend_InkSketch.ApplicationData;

public partial class InkSketchDbContext : DbContext
{
    public InkSketchDbContext()
    {
    }

    public InkSketchDbContext(DbContextOptions<InkSketchDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FavoritesImage> FavoritesImages { get; set; }

    public virtual DbSet<FavoritesView> FavoritesViews { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<ImagesTag> ImagesTags { get; set; }

    public virtual DbSet<ImagesView> ImagesViews { get; set; }

    public virtual DbSet<ProfileView> ProfileViews { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<TagsView> TagsViews { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersTag> UsersTags { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=IgorPc\\SQLEXPRESS; Database=InkSketchDb; Trusted_Connection=True; TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FavoritesImage>(entity =>
        {
            entity.HasKey(e => e.FavoriteImageId);

            entity.Property(e => e.FavoriteImageId).HasColumnName("favorite_image_id");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Image).WithMany(p => p.FavoritesImages)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoritesImages_Images");

            entity.HasOne(d => d.User).WithMany(p => p.FavoritesImages)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FavoritesImages_Users");
        });

        modelBuilder.Entity<FavoritesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("FavoritesView");

            entity.Property(e => e.FavoriteImageId).HasColumnName("favorite_image_id");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.SourceImage).HasColumnName("source_image");
            entity.Property(e => e.UserId).HasColumnName("user_id");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.SourceImage).HasColumnName("source_image");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Images)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Images_Users");
        });

        modelBuilder.Entity<ImagesTag>(entity =>
        {
            entity.HasKey(e => e.ImageTagId);

            entity.Property(e => e.ImageTagId).HasColumnName("image_tag_id");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Image).WithMany(p => p.ImagesTags)
                .HasForeignKey(d => d.ImageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImagesTags_Images");

            entity.HasOne(d => d.Tag).WithMany(p => p.ImagesTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ImagesTags_Tags");
        });

        modelBuilder.Entity<ImagesView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ImagesView");

            entity.Property(e => e.HexColor)
                .HasMaxLength(50)
                .HasColumnName("hex_color");
            entity.Property(e => e.ImageId).HasColumnName("image_id");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.SourceImage).HasColumnName("source_image");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
        });

        modelBuilder.Entity<ProfileView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("ProfileView");

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Tagname)
                .HasMaxLength(50)
                .HasColumnName("tagname");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.HexColor)
                .HasMaxLength(50)
                .HasColumnName("hex_color");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
        });

        modelBuilder.Entity<TagsView>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("TagsView");

            entity.Property(e => e.HexColor)
                .HasMaxLength(50)
                .HasColumnName("hex_color");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.UserTagId).HasColumnName("user_tag_id");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.CoverImage).HasColumnName("cover_image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(30)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(30)
                .HasColumnName("phone");
            entity.Property(e => e.RegistrationDate)
                .HasColumnType("date")
                .HasColumnName("registration_date");
        });

        modelBuilder.Entity<UsersTag>(entity =>
        {
            entity.HasKey(e => e.UserTagId);

            entity.Property(e => e.UserTagId).HasColumnName("user_tag_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.UsersTags)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersTags_Tags");

            entity.HasOne(d => d.User).WithMany(p => p.UsersTags)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UsersTags_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
