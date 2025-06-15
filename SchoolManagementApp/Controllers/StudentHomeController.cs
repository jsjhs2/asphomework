//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using SchoolManagementApp.Models;
//using System.Threading.Tasks;

//namespace SchoolManagementApp.Controllers
//{
//    [Authorize(Roles = "Student")]
//    public class StudentHomeController : Controller
//    {
//        private readonly SchoolContext _context;

//        public StudentHomeController(SchoolContext context)
//        {
//            _context = context;
//        }

//        public async Task<IActionResult> Index()
//        {
//            // 获取当前登录学生ID
//            var studentId = int.Parse(User.FindFirst("StudentId").Value);

//            var student = await _context.Students
//                .Include(s => s.Class)
//                //.Include(s => s.Grade)
//                .FirstOrDefaultAsync(s => s.StudentId == studentId);

//            if (student == null)
//            {
//                return NotFound();
//            }

//            return View(student);
//        }
//    }
//}