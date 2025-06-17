using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Helpers;
using System;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagementApp.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Grade> Grades { get; set; }

        public DbSet<User> Users { get; set; }
        // 添加 Classes 的 DbSet
        public DbSet<Classes> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = 1,
                    Username = "sys",
                    PasswordHash = PasswordHasher.HashPassword("123456"), // 使用通用工具类
                    Role = "Admin",
                    DisplayName = "系统管理员"
                }
            );
            // 为Score属性指定精度
            modelBuilder.Entity<Grade>()
                .Property(g => g.Score)
                .HasPrecision(5, 2);

            // 配置Classes实体
            modelBuilder.Entity<Classes>(entity =>
            {
                entity.Property(c => c.ClassName).IsRequired().HasMaxLength(50);
                entity.Property(c => c.ClassCode).HasMaxLength(20);
                entity.Property(c => c.TeacherName).HasMaxLength(50);
                entity.Property(c => c.Description).HasMaxLength(200);
            });

            // 学生与班级关系
            modelBuilder.Entity<Student>()
                .HasOne(s => s.Class)
                .WithMany(c => c.Students)
                .HasForeignKey(s => s.ClassId);
            
        }
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}