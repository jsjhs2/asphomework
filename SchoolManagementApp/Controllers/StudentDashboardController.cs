using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System.Linq;
using System.Security.Claims;
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
            // 获取当前登录学生的ID
            var studentIdClaim = User.FindFirst("StudentId");
            if (studentIdClaim == null || !int.TryParse(studentIdClaim.Value, out int studentId))
            {
                return RedirectToAction("Login", "Account");
            }

            // 获取当前学生信息
            var currentStudent = await _context.Students
                .Include(s => s.Class)
                .FirstOrDefaultAsync(s => s.StudentId == studentId);

            if (currentStudent == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // 获取同班级的学生
            var classmates = await _context.Students
                .Where(s => s.ClassId == currentStudent.ClassId && s.StudentId != studentId)
                .OrderBy(s => s.Name)
                .ToListAsync();

            // 获取当前学生的成绩
            var grades = await _context.Grades
                .Where(g => g.StudentId == studentId)
                .OrderBy(g => g.Subject)
                .ToListAsync();

            // 计算平均分
            var averageScore = grades.Any() ? grades.Average(g => g.Score) : 0;

            var viewModel = new StudentDashboardViewModel
            {
                CurrentStudent = currentStudent,
                Classmates = classmates,
                Grades = grades,
                AverageScore = averageScore
            };

            return View(viewModel);
        }
    }

    public class StudentDashboardViewModel
    {
        public Student CurrentStudent { get; set; }
        public System.Collections.Generic.List<Student> Classmates { get; set; }
        public System.Collections.Generic.List<Grade> Grades { get; set; }
        public decimal AverageScore { get; set; }
    }
}