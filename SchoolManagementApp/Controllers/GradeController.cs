using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    public class GradeController : Controller
    {
        private readonly SchoolContext _context;

        public GradeController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Grade
        public async Task<IActionResult> Index(int? classId, string subject)
        {
            // 获取所有成绩，包括学生和班级信息
            var gradesQuery = _context.Grades
                .Include(g => g.Student)
                .ThenInclude(s => s.Class)
                .AsQueryable();

            // 应用班级筛选
            if (classId.HasValue && classId.Value > 0)
            {
                gradesQuery = gradesQuery.Where(g => g.Student.ClassId == classId.Value);
            }

            // 应用科目筛选
            if (!string.IsNullOrEmpty(subject))
            {
                gradesQuery = gradesQuery.Where(g => g.Subject == subject);
            }

            // 获取成绩列表
            var grades = await gradesQuery.ToListAsync();

            // 准备视图数据
            ViewBag.Classes = await _context.Classes
                .Select(c => new SelectListItem
                {
                    Value = c.ClassId.ToString(),
                    Text = $"{c.ClassName} ({c.ClassCode})"
                })
                .ToListAsync();

            ViewBag.Subjects = await _context.Grades
                .Select(g => g.Subject)
                .Distinct()
                .ToListAsync();

            // 计算统计数据
            ViewBag.TotalGrades = await _context.Grades.CountAsync();
            ViewBag.TotalStudents = await _context.Students.CountAsync();
            ViewBag.AverageScore = await _context.Grades
                .Select(g => g.Score)
                .DefaultIfEmpty()
                .AverageAsync();
            ViewBag.MaxScore = await _context.Grades
                .MaxAsync(g => (decimal?)g.Score) ?? 0;

            return View(grades);
        }


        // GET: Grade/Create
        public IActionResult Create()
        {
            var model = new GradeCreateViewModel
            {
                Students = _context.Students.ToList(),
                Grades = new List<GradeEntry>
                {
                    new GradeEntry(), // 第一门课
                    new GradeEntry(), // 第二门课
                    new GradeEntry()  // 第三门课
                }
            };

            return View(model);
        }

        // POST: Grade/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GradeCreateViewModel model)
        {
            if (ModelState.IsValid && model.Grades.Count >= 3)
            {
                foreach (var gradeEntry in model.Grades)
                {
                    var grade = new Grade
                    {
                        StudentId = model.StudentId,
                        Subject = gradeEntry.Subject,
                        Score = gradeEntry.Score
                    };

                    _context.Add(grade);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // 重新加载学生列表
            model.Students = _context.Students.ToList();
            return View(model);
        }

        // GET: Grade/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            ViewBag.Students = _context.Students.ToList();
            return View(grade);
        }

        // POST: Grade/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GradeId,StudentId,Subject,Score")] Grade grade)
        {
            if (id != grade.GradeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.GradeId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Students = _context.Students.ToList();
            return View(grade);
        }

        // GET: Grade/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grades
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.GradeId == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grade/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var grade = await _context.Grades.FindAsync(id);
            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GradeExists(int id)
        {
            return _context.Grades.Any(e => e.GradeId == id);
        }
    }
}