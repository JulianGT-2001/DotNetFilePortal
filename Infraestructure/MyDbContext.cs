using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infraestructure;

public partial class MyDbContext : IdentityDbContext<ApplicationUser>
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<FileEntity> TbFiles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=Seguridad");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FileEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tbf_id_pk");

            entity.ToTable("tb_file");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.MimeType).HasColumnName("mime_type");
            entity.Property(e => e.OriginalName).HasColumnName("original_name");
            entity.Property(e => e.Path).HasColumnName("path");
            entity.Property(e => e.SizeInBytes).HasColumnName("size_in_bytes");
            entity.Property(e => e.UploadedAt)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("uploaded_at");
        });

        modelBuilder.Entity<FileUser>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tfu_id_pk");

            entity.ToTable("db_file_user");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.FileId).HasColumnName("file_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(e => e.File)
                .WithMany(e => e.FileUsers)
                .HasConstraintName("tfu_file_id_fk")
                .HasForeignKey(e => e.FileId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.User)
                .WithMany(e => e.FileUsers)
                .HasConstraintName("tfu_user_id_fk")
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        base.OnModelCreating(modelBuilder);
    }
}
