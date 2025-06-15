using Microsoft.EntityFrameworkCore;
using System;

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

        // 添加 Classes 的 DbSet
        public DbSet<Classes> Classes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
    }
}