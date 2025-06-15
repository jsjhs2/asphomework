using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class Classes
    {
        [Key]
        public int ClassId { get; set; }

        [Required(ErrorMessage = "班级名称是必填项")]
        [StringLength(50, ErrorMessage = "班级名称不能超过50个字符")]
        [Display(Name = "班级名称")]
        public string ClassName { get; set; }

        [StringLength(20, ErrorMessage = "班级代码不能超过20个字符")]
        [Display(Name = "班级代码")]
        public string ClassCode { get; set; }

        [StringLength(50, ErrorMessage = "班主任姓名不能超过50个字符")]
        [Display(Name = "班主任")]
        public string TeacherName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "创建时间")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [Display(Name = "最后更新")]
        public DateTime? LastUpdatedDate { get; set; }

        [Display(Name = "班级描述")]
        [StringLength(200, ErrorMessage = "描述不能超过200个字符")]
        public string Description { get; set; }

        // 导航属性
        public ICollection<Student> Students { get; set; }
    }
}