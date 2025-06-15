using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class Grade
    {
        [Key]
        public int GradeId { get; set; }

        [Required]
        [Display(Name = "学生")]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "科目")]
        public string Subject { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "分数必须在0-100之间")]
        [Display(Name = "分数")]
        public decimal Score { get; set; }

        // 导航属性
        public Student Student { get; set; }
    }
}