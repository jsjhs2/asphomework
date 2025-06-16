using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    [Authorize]
    public class StudentController : Controller
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Student

        public async Task<IActionResult> Index(string searchTerm)
        {
            // 获取所有学生，包括班级信息
            var students = _context.Students.Include(s => s.Class).AsQueryable();

            // 应用搜索条件
            if (!string.IsNullOrEmpty(searchTerm))
            {
                students = students.Where(s =>
                    s.Name.Contains(searchTerm) ||
                    s.RollNumber.Contains(searchTerm)
                );

                ViewBag.SearchTerm = searchTerm;
            }

            // 获取班级列表用于高级搜索
            ViewBag.Classes = await _context.Classes.ToListAsync();

            return View(await students.ToListAsync());
        }
        // GET: Student/Create
        // GET: Student/Create
        public IActionResult Create()
        {
            // 加载班级列表
            ViewBag.Classes = _context.Classes
                .Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),
                    Text = $"{c.ClassName} ({c.ClassCode})"
                })
                .ToList();

            return View();
        }

        // POST: Student/Create
        [HttpPost]  // 明确标记为POST请求
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,RollNumber,DateOfBirth,Email,PhoneNumber,ClassId")] Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                  

                    _context.Add(student);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                // 记录详细错误日志
                Console.WriteLine($"创建学生失败: {ex.Message}");
                Console.WriteLine(ex.StackTrace);

                // 将错误信息传递给视图
                ModelState.AddModelError(string.Empty, "创建学生时发生错误: " + ex.Message);
            }

            // 如果模型验证失败，重新加载班级列表并返回视图
            ViewBag.Classes = _context.Classes
                .Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),
                    Text = $"{c.ClassName} ({c.ClassCode})"
                })
                .ToList();

            return View(student);
        }

        // GET: Student/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // 加载班级列表
            ViewBag.Classes = _context.Classes
                .Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),
                    Text = $"{c.ClassName} ({c.ClassCode})"
                })
                .ToList();

            // 加载学生成绩
            ViewBag.Grades = await _context.Grades
                .Where(g => g.StudentId == id)
                .ToListAsync();

            return View(student);
        }

        // POST: Student/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Name,RollNumber,DateOfBirth,Email,PhoneNumber,ClassId")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "学生信息更新成功！";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            // 重新加载班级列表
            ViewBag.Classes = _context.Classes
                .Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),
                    Text = $"{c.ClassName} ({c.ClassCode})"
                })
                .ToList();

            return View(student);
        }

        // GET: Student/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .FirstOrDefaultAsync(m => m.StudentId == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            // 检查是否有成绩记录
            var hasGrades = await _context.Grades.AnyAsync(g => g.StudentId == id);
            if (hasGrades)
            {
                TempData["ErrorMessage"] = "无法删除，该学生有成绩记录。请先删除相关成绩记录。";
                return RedirectToAction(nameof(Index));
            }

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "学生删除成功！";
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.StudentId == id);
        }
    }
}