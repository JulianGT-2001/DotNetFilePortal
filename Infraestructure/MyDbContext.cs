using Microsoft.EntityFrameworkCore;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Infraestructure;

public partial class MyDbContext : DbContext
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
    {
    }

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
            entity.Property(e => e.UploadedBy).HasColumnName("uploaded_by");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
