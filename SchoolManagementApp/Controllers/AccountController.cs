// Controllers/AccountController.cs
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Models;
using SchoolManagementApp.Services;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserAuthenticationService _authService;
        private readonly SchoolContext _context;

        public AccountController(IUserAuthenticationService authService, SchoolContext context)
        {
            _authService = authService;
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
                // 验证用户
                var user = await _authService.AuthenticateUser(model.Username, model.Password);

                if (user != null)
                {
                    await SignInUser(user, model.RememberMe);

                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "用户名或密码不正确");
            }

            return View(model);
        }

        // 退出登录
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        // AccountController.cs
        [HttpPost, ActionName("Logout")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutConfirmed()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }
        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var hashedPassword = _authService.HashPassword(model.Password);
                var newUser = new User
                {
                    Username = model.Username,
                    PasswordHash = hashedPassword,
                    Role = model.Role,
                    DisplayName = model.DisplayName
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }
        private async Task SignInUser(User user, bool rememberMe)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("UserId", user.UserId.ToString()),
                new Claim("DisplayName", user.DisplayName ?? user.Username)
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                ExpiresUtc = rememberMe ? System.DateTimeOffset.UtcNow.AddDays(30) : System.DateTimeOffset.UtcNow.AddHours(8)
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }
    }
}