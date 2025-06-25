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

        public DbSet<Course> Courses { get; set; }

        public DbSet<OperationLog> OperationLogs { get; set; }

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
            
            // 默认课程
            modelBuilder.Entity<Course>().HasData(
                new Course { CourseCode = "CS101", CourseName = "计算机基础", Department = "计算机学院", Credit = 3 },
                new Course { CourseCode = "MATH201", CourseName = "高等数学", Department = "数学学院", Credit = 4 },
                new Course { CourseCode = "ENG301", CourseName = "大学英语", Department = "外国语学院", Credit = 2 },
                new Course { CourseCode = "PHY101", CourseName = "大学物理", Department = "物理学院", Credit = 3.5 },
                new Course { CourseCode = "CHEM101", CourseName = "基础化学", Department = "化学学院", Credit = 3 },
                new Course { CourseCode = "BIO101", CourseName = "生物科学", Department = "生命科学学院", Credit = 2.5 },
                new Course { CourseCode = "HIST101", CourseName = "中国历史", Department = "历史学院", Credit = 2 },
                new Course { CourseCode = "LAW101", CourseName = "法学导论", Department = "法学院", Credit = 2 },
                new Course { CourseCode = "ART101", CourseName = "艺术鉴赏", Department = "艺术学院", Credit = 1.5 },
                new Course { CourseCode = "PE101", CourseName = "体育", Department = "体育学院", Credit = 1 }
            );

            // 默认班级
            modelBuilder.Entity<Classes>().HasData(
                new Classes { ClassId = 1, ClassName = "计算机一班", ClassCode = "CS1", TeacherName = "张老师" },
                new Classes { ClassId = 2, ClassName = "数学一班", ClassCode = "MATH1", TeacherName = "李老师" },
                new Classes { ClassId = 3, ClassName = "英语一班", ClassCode = "ENG1", TeacherName = "王老师" },
                new Classes { ClassId = 4, ClassName = "物理一班", ClassCode = "PHY1", TeacherName = "赵老师" },
                new Classes { ClassId = 5, ClassName = "化学一班", ClassCode = "CHEM1", TeacherName = "钱老师" },
                new Classes { ClassId = 6, ClassName = "生物一班", ClassCode = "BIO1", TeacherName = "孙老师" },
                new Classes { ClassId = 7, ClassName = "历史一班", ClassCode = "HIST1", TeacherName = "周老师" },
                new Classes { ClassId = 8, ClassName = "法学一班", ClassCode = "LAW1", TeacherName = "吴老师" },
                new Classes { ClassId = 9, ClassName = "艺术一班", ClassCode = "ART1", TeacherName = "郑老师" },
                new Classes { ClassId = 10, ClassName = "体育一班", ClassCode = "PE1", TeacherName = "王五" }
            );

            // 默认学生
            modelBuilder.Entity<Student>().HasData(
                new Student { StudentId = 1, Name = "小明", Gender = "男", RollNumber = "2023001", DateOfBirth = new DateTime(2005,1,1), ClassId = 1 },
                new Student { StudentId = 2, Name = "小红", Gender = "女", RollNumber = "2023002", DateOfBirth = new DateTime(2005,2,2), ClassId = 2 },
                new Student { StudentId = 3, Name = "小刚", Gender = "男", RollNumber = "2023003", DateOfBirth = new DateTime(2005,3,3), ClassId = 3 },
                new Student { StudentId = 4, Name = "小丽", Gender = "女", RollNumber = "2023004", DateOfBirth = new DateTime(2005,4,4), ClassId = 4 },
                new Student { StudentId = 5, Name = "小强", Gender = "男", RollNumber = "2023005", DateOfBirth = new DateTime(2005,5,5), ClassId = 5 },
                new Student { StudentId = 6, Name = "小芳", Gender = "女", RollNumber = "2023006", DateOfBirth = new DateTime(2005,6,6), ClassId = 6 },
                new Student { StudentId = 7, Name = "小军", Gender = "男", RollNumber = "2023007", DateOfBirth = new DateTime(2005,7,7), ClassId = 7 },
                new Student { StudentId = 8, Name = "小燕", Gender = "女", RollNumber = "2023008", DateOfBirth = new DateTime(2005,8,8), ClassId = 8 },
                new Student { StudentId = 9, Name = "小鹏", Gender = "男", RollNumber = "2023009", DateOfBirth = new DateTime(2005,9,9), ClassId = 9 },
                new Student { StudentId = 10, Name = "小玉", Gender = "女", RollNumber = "2023010", DateOfBirth = new DateTime(2005,10,10), ClassId = 10 }
            );

            // 默认成绩
            modelBuilder.Entity<Grade>().HasData(
                // 小明的成绩
                new Grade { GradeId = 1, StudentId = 1, Subject = "计算机基础", Score = 85 },
                new Grade { GradeId = 2, StudentId = 1, Subject = "高等数学", Score = 78 },
                new Grade { GradeId = 3, StudentId = 1, Subject = "大学英语", Score = 92 },
                new Grade { GradeId = 4, StudentId = 1, Subject = "大学物理", Score = 88 },
                new Grade { GradeId = 5, StudentId = 1, Subject = "体育", Score = 95 },
                
                // 小红的成绩
                new Grade { GradeId = 6, StudentId = 2, Subject = "高等数学", Score = 90 },
                new Grade { GradeId = 7, StudentId = 2, Subject = "大学英语", Score = 85 },
                new Grade { GradeId = 8, StudentId = 2, Subject = "大学物理", Score = 87 },
                new Grade { GradeId = 9, StudentId = 2, Subject = "基础化学", Score = 93 },
                new Grade { GradeId = 10, StudentId = 2, Subject = "体育", Score = 88 },
                
                // 小刚的成绩
                new Grade { GradeId = 11, StudentId = 3, Subject = "大学英语", Score = 88 },
                new Grade { GradeId = 12, StudentId = 3, Subject = "高等数学", Score = 82 },
                new Grade { GradeId = 13, StudentId = 3, Subject = "大学物理", Score = 75 },
                new Grade { GradeId = 14, StudentId = 3, Subject = "基础化学", Score = 89 },
                new Grade { GradeId = 15, StudentId = 3, Subject = "体育", Score = 92 },
                
                // 小丽的成绩
                new Grade { GradeId = 16, StudentId = 4, Subject = "大学物理", Score = 92 },
                new Grade { GradeId = 17, StudentId = 4, Subject = "高等数学", Score = 88 },
                new Grade { GradeId = 18, StudentId = 4, Subject = "大学英语", Score = 85 },
                new Grade { GradeId = 19, StudentId = 4, Subject = "基础化学", Score = 90 },
                new Grade { GradeId = 20, StudentId = 4, Subject = "体育", Score = 87 },
                
                // 小强的成绩
                new Grade { GradeId = 21, StudentId = 5, Subject = "基础化学", Score = 80 },
                new Grade { GradeId = 22, StudentId = 5, Subject = "高等数学", Score = 85 },
                new Grade { GradeId = 23, StudentId = 5, Subject = "大学英语", Score = 78 },
                new Grade { GradeId = 24, StudentId = 5, Subject = "大学物理", Score = 82 },
                new Grade { GradeId = 25, StudentId = 5, Subject = "体育", Score = 90 },
                
                // 小芳的成绩
                new Grade { GradeId = 26, StudentId = 6, Subject = "生物科学", Score = 87 },
                new Grade { GradeId = 27, StudentId = 6, Subject = "高等数学", Score = 83 },
                new Grade { GradeId = 28, StudentId = 6, Subject = "大学英语", Score = 91 },
                new Grade { GradeId = 29, StudentId = 6, Subject = "基础化学", Score = 89 },
                new Grade { GradeId = 30, StudentId = 6, Subject = "体育", Score = 85 },
                
                // 小军的成绩
                new Grade { GradeId = 31, StudentId = 7, Subject = "中国历史", Score = 78 },
                new Grade { GradeId = 32, StudentId = 7, Subject = "高等数学", Score = 72 },
                new Grade { GradeId = 33, StudentId = 7, Subject = "大学英语", Score = 85 },
                new Grade { GradeId = 34, StudentId = 7, Subject = "大学物理", Score = 68 },
                new Grade { GradeId = 35, StudentId = 7, Subject = "体育", Score = 88 },
                
                // 小燕的成绩
                new Grade { GradeId = 36, StudentId = 8, Subject = "法学导论", Score = 91 },
                new Grade { GradeId = 37, StudentId = 8, Subject = "高等数学", Score = 86 },
                new Grade { GradeId = 38, StudentId = 8, Subject = "大学英语", Score = 89 },
                new Grade { GradeId = 39, StudentId = 8, Subject = "中国历史", Score = 94 },
                new Grade { GradeId = 40, StudentId = 8, Subject = "体育", Score = 82 },
                
                // 小鹏的成绩
                new Grade { GradeId = 41, StudentId = 9, Subject = "艺术鉴赏", Score = 95 },
                new Grade { GradeId = 42, StudentId = 9, Subject = "高等数学", Score = 79 },
                new Grade { GradeId = 43, StudentId = 9, Subject = "大学英语", Score = 87 },
                new Grade { GradeId = 44, StudentId = 9, Subject = "中国历史", Score = 92 },
                new Grade { GradeId = 45, StudentId = 9, Subject = "体育", Score = 89 },
                
                // 小玉的成绩
                new Grade { GradeId = 46, StudentId = 10, Subject = "体育", Score = 98 },
                new Grade { GradeId = 47, StudentId = 10, Subject = "高等数学", Score = 84 },
                new Grade { GradeId = 48, StudentId = 10, Subject = "大学英语", Score = 86 },
                new Grade { GradeId = 49, StudentId = 10, Subject = "大学物理", Score = 80 },
                new Grade { GradeId = 50, StudentId = 10, Subject = "基础化学", Score = 88 }
            );
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