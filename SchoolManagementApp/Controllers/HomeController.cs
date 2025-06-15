using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Models;

namespace SchoolManagementApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}