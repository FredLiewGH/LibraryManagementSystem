using LMS.EFCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.EFCore.Data
{
    public partial class LMSDBContext : DbContext
    {
        public LMSDBContext()
        {
        }

        public LMSDBContext(DbContextOptions<LMSDBContext> options)
        : base(options)
        {
        }

        public DbSet<Tblmembers> tblmembers { get; set; }
        public DbSet<Tblbooks> tblbooks { get; set; }
        public DbSet<Tblborrows> tblborrows { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.UseCollation("utf8mb4_0900_ai_ci")
                    .HasCharSet("utf8mb4");

            builder.Entity<Tblmembers>(entity =>
            {
                entity.HasKey(e => e.MemberID);

                entity.Property(e => e.MemberName)
                .IsRequired()
                .HasMaxLength(100);

                entity.HasIndex(e => new { e.PhoneNo, e.Email })
                .IsUnique();

                entity.Property(e => e.Gender)
                .IsRequired()
                .HasMaxLength(6);      

                entity.Property(e => e.PhoneNo)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedTimestamp)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRowVersion();
            });

            builder.Entity<Tblbooks>(entity =>
            {
                entity.HasKey(e => e.BookID);

                entity.Property(e => e.BookName)
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(e => e.Author)
                .IsRequired()
                .HasMaxLength(300);

                entity.Property(e => e.Publisher)
                    .IsRequired()
                    .HasMaxLength(100);
               
                entity.Property(e => e.Status)
                     .IsRequired()
                     .HasMaxLength(16)
                     .HasDefaultValue("AVAILABLE");

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedTimestamp)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRowVersion();
            });

            builder.Entity<Tblborrows>(entity =>
            {
                entity.HasKey(e => e.BorrowID);

                // Foreign key
                entity.HasOne<Tblmembers>()
                      .WithMany()
                      .HasForeignKey(e => e.MemberID)
                      .OnDelete(DeleteBehavior.Restrict);

                // Foreign key
                entity.HasOne<Tblbooks>()
                      .WithMany()
                      .HasForeignKey(e => e.BookID)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.CreatedTimestamp).HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.UpdatedTimestamp)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRowVersion();
            });

            OnModelCreatingPartial(builder);
        }

        partial void OnModelCreatingPartial(ModelBuilder builder);
    }
}
