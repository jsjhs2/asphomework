// Models/RegisterViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        [MaxLength(50)]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Required(ErrorMessage = "请输入密码")]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认密码")]
        [Compare("Password", ErrorMessage = "密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "角色")]
        public string Role { get; set; } = "Teacher";

        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }
    }
}