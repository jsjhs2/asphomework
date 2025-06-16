using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    [Authorize(Roles = "Student")]
    public class StudentDashboardController : Controller
    {
        private readonly SchoolContext _context;

        public StudentDashboardController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // 获取当前登录学生ID
            var studentId = int.Parse(User.FindFirst("StudentId").Value);

            var student = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (student == null)
            {
                return NotFound();
            }

            // 获取本班级学生
            var classStudents = await _context.Students
                .Where(s => s.ClassId == student.ClassId)
                .ToListAsync();

            // 获取自己的各科成绩
            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId)
                .ToListAsync();

            var viewModel = new StudentDashboardViewModel
            {
                ClassStudents = classStudents,
                Grades = grades
            };

            return View(viewModel);
        }
    }

    public class StudentDashboardViewModel
    {
        public List<Student> ClassStudents { get; set; }
        public List<Grade> Grades { get; set; }
    }
}