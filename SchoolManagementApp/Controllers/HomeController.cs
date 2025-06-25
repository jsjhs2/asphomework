using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly SchoolContext _context;

        public HomeController(SchoolContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "StudentDashboard");
            }
            var classes = await _context.Classes.Include(c => c.Students).ToListAsync();
            return View(classes);
        }
    }
}