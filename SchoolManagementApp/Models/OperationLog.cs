using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagementApp.Models
{
    public class OperationLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public DateTime Time { get; set; }
    }
} 