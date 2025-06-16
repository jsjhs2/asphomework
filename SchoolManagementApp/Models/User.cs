// Models/User.cs
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        [Display(Name = "用户名")]
        public string Username { get; set; }

        [Required]
        [MaxLength(100)]
        [Display(Name = "密码哈希")]
        public string PasswordHash { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "角色")]
        public string Role { get; set; } = "Teacher";

        [Display(Name = "显示名称")]
        public string DisplayName { get; set; }
    }
}