using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class GradeCreateViewModel
    {
        [Required(ErrorMessage = "请选择学生")]
        [Display(Name = "学生")]
        public int StudentId { get; set; }

        public List<Student> Students { get; set; } = new List<Student>();

        [MinLength(3, ErrorMessage = "请至少添加三门课程成绩")]
        public List<GradeEntry> Grades { get; set; } = new List<GradeEntry>();
    }

    public class GradeEntry
    {
        [Required(ErrorMessage = "科目不能为空")]
        [Display(Name = "科目")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "分数不能为空")]
        [Range(0, 100, ErrorMessage = "分数必须在0-100之间")]
        [Display(Name = "分数")]
        public decimal Score { get; set; }
    }
}