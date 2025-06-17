// Services/AuthenticationService.cs
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Helpers;
using SchoolManagementApp.Models;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementApp.Services
{
    // 重命名接口为 IUserAuthenticationService
    public interface IUserAuthenticationService
    {
        Task<User> AuthenticateUser(string username, string password);
        string HashPassword(string password);
    }

    public class AuthenticationService : IUserAuthenticationService
    {
        private readonly SchoolContext _context;

        public AuthenticationService(SchoolContext context)
        {
            _context = context;
        }

        public async Task<User> AuthenticateUser(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
                return null;

            // 使用通用的密码哈希工具类
            var hashedInput = PasswordHasher.HashPassword(password);
            return hashedInput == user.PasswordHash ? user : null;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}