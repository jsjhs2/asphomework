using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class Course
    {
        [Key]
        [Required]
        [Display(Name = "课程编号")]
        public string CourseCode { get; set; } // 课程编号，唯一

        [Required]
        [Display(Name = "课程名")]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "开课院系")]
        public string Department { get; set; }

        [Required]
        [Range(0.5, 20, ErrorMessage = "学分必须在0.5-20之间")]
        [Display(Name = "学分")]
        public double Credit { get; set; }
    }
} 