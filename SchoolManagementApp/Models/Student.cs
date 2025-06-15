using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SchoolManagementApp.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }

        [Required]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "学号")]
        public string RollNumber { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "出生日期")]
        public DateTime DateOfBirth { get; set; }

        [Display(Name = "邮箱")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "联系电话")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "请选择班级")]
        [Display(Name = "班级")]
        public int ClassId { get; set; }

        // 导航属性
        public Classes Class { get; set; }


        [DataType(DataType.Password)]
        [JsonIgnore] // 在JSON序列化时忽略此字段
        public string Password { get; set; } = "123456"; // 默认密码
    }
}