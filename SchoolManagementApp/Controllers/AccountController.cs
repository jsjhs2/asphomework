using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SchoolContext _context;

        public AccountController(SchoolContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                // 教师账户（仍使用密码验证）
                if (model.Username == "sys" && model.Password == "123456")
                {
                    await SignInTeacher(model.Username);
                    return RedirectToAction("Index", "Home");
                }

                // 学生账户（仅验证学号是否存在）
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.RollNumber == model.Username);

                if (student != null)
                {
                    await SignInStudent(student);
                    return RedirectToAction("Index", "StudentDashboard");
                }

                ModelState.AddModelError(string.Empty, "用户名不存在或密码错误");
            }

            return View(model);
        }


        // 退出登录
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
        // AccountController.cs

        // GET: /Account/ChangePassword
        [Authorize(Roles = "Student")]  // 仅限学生访问
        public IActionResult ChangePassword()
        {
            return View();
        }

        // POST: /Account/ChangePassword
        //[HttpPost]
        //[Authorize(Roles = "Student")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        //{
        //    if (!ModelState.IsValid) return View(model);

        //    var studentId = int.Parse(User.FindFirst("StudentId").Value);
        //    var student = await _context.Students.FindAsync(studentId);
        //    if (student == null) return NotFound();

        //    // 验证当前密码
        //    var passwordHasher = new PasswordHasher<Student>();
        //    var result = passwordHasher.VerifyHashedPassword(student, student.PasswordHash, model.CurrentPassword);
        //    if (result != PasswordVerificationResult.Success)
        //    {
        //        ModelState.AddModelError("CurrentPassword", "当前密码不正确。");
        //        return View(model);
        //    }

        //    // 更新密码
        //    student.PasswordHash = passwordHasher.HashPassword(student, model.NewPassword);
        //    _context.Update(student);
        //    await _context.SaveChangesAsync();

        //    ViewBag.SuccessMessage = "密码修改成功！";
        //    return View();
        //}
        private async Task SignInTeacher(string username)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "Teacher"),
                new Claim("DisplayName", "系统管理员")
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // 可以设置更多属性，如过期时间等
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private async Task SignInStudent(Student student)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, student.RollNumber),
                new Claim(ClaimTypes.Role, "Student"),
                new Claim("DisplayName", student.Name),
                new Claim("StudentId", student.StudentId.ToString())
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                // 可以设置更多属性
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}