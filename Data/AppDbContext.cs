using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using TopikApp.Models;

namespace TopikApp.Data
{
    public class AppDbContext : DbContext
    {
        // Các DbSet tương ứng với bảng trong database
        public DbSet<User> Users { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<ExamResult> ExamResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Đọc cấu hình từ appsettings.json
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                // Sử dụng MySQL với Pomelo.EntityFrameworkCore.MySql
                optionsBuilder.UseMySql(
                    configuration.GetConnectionString("DefaultConnection"),
                    ServerVersion.AutoDetect(configuration.GetConnectionString("DefaultConnection")),
                    options => options.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null)
                );
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình quan hệ và ràng buộc dữ liệu
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(u => u.Username).IsUnique();
                entity.Property(u => u.Role).HasDefaultValue("User");
            });

            modelBuilder.Entity<Exam>(entity =>
            {
                entity.Property(e => e.Title).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Level).IsRequired();
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.HasOne(q => q.Exam)
                      .WithMany(e => e.Questions)
                      .HasForeignKey(q => q.ExamId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ExamResult>(entity =>
            {
                entity.HasOne(er => er.User)
                      .WithMany(u => u.ExamResults)
                      .HasForeignKey(er => er.UserId);

                entity.HasOne(er => er.Exam)
                      .WithMany(e => e.ExamResults)
                      .HasForeignKey(er => er.ExamId);
            });
        }
    }
}