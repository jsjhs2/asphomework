using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using SchoolManagementApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SchoolManagementApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class CoursesController : Controller
    {
        private static List<Course> _courses = new List<Course>
        {
            new Course { CourseCode = "CS101", CourseName = "计算机基础", Department = "计算机学院", Credit = 3 },
            new Course { CourseCode = "MATH201", CourseName = "高等数学", Department = "数学学院", Credit = 4 },
            new Course { CourseCode = "ENG301", CourseName = "大学英语", Department = "外国语学院", Credit = 2 }
        };

        public IActionResult Index()
        {
            return View(_courses);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Course course)
        {
            if (_courses.Any(c => c.CourseCode == course.CourseCode))
            {
                ModelState.AddModelError("CourseCode", "课程编号已存在");
            }
            if (ModelState.IsValid)
            {
                _courses.Add(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public IActionResult Edit(string id)
        {
            var course = _courses.FirstOrDefault(c => c.CourseCode == id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(string id, Course course)
        {
            var existing = _courses.FirstOrDefault(c => c.CourseCode == id);
            if (existing == null) return NotFound();
            if (id != course.CourseCode && _courses.Any(c => c.CourseCode == course.CourseCode))
            {
                ModelState.AddModelError("CourseCode", "课程编号已存在");
            }
            if (ModelState.IsValid)
            {
                existing.CourseName = course.CourseName;
                existing.Department = course.Department;
                existing.Credit = course.Credit;
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public IActionResult Delete(string id)
        {
            var course = _courses.FirstOrDefault(c => c.CourseCode == id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string id)
        {
            var course = _courses.FirstOrDefault(c => c.CourseCode == id);
            if (course != null)
            {
                _courses.Remove(course);
            }
            return RedirectToAction(nameof(Index));
        }
    }
} 