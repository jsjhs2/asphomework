using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
                // 检查教师账户
                if (model.Username == "sys" && model.Password == "123456")
                {
                    await SignInTeacher(model.Username);
                    return RedirectToAction("Index", "Grade");
                }

                // 检查学生账户
                var student = await _context.Students
                    .FirstOrDefaultAsync(s => s.RollNumber == model.Username && s.Password == model.Password);

                if (student != null)
                {
                    await SignInStudent(student);
                    return RedirectToAction("Index", "StudentHome");
                }

                ModelState.AddModelError(string.Empty, "用户名或密码错误");
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