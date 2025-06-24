using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagementApp.Controllers
{
    [Authorize]
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
        public IActionResult Create(int? classId = null)
        {
            var classes = _context.Classes.ToList();
            var students = classId.HasValue && classId.Value > 0
                ? _context.Students.Where(s => s.ClassId == classId.Value).ToList()
                : _context.Students.ToList();
            var subjects = _context.Courses.Select(c => c.CourseName).Distinct().ToList();
            var model = new GradeCreateViewModel
            {
                ClassId = classId,
                Classes = classes,
                Students = students,
                Subjects = subjects,
                Grades = new List<GradeEntry>
                {
                    new GradeEntry(),
                    new GradeEntry(),
                    new GradeEntry()
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
            // 重新加载班级、学生、科目列表
            model.Classes = _context.Classes.ToList();
            model.Students = model.ClassId.HasValue && model.ClassId.Value > 0
                ? _context.Students.Where(s => s.ClassId == model.ClassId.Value).ToList()
                : _context.Students.ToList();
            model.Subjects = _context.Courses.Select(c => c.CourseName).Distinct().ToList();
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
        // GET: Grade/Export
        

        // 导出到Excel
 
        

        private string GetRating(decimal score)
        {
            if (score < 60) return "不及格";
            if (score < 70) return "及格";
            if (score < 80) return "中等";
            if (score < 90) return "良好";
            return "优秀";
        }

        // 导出到CSV
        [HttpGet]
        [Route("Grade/Export")]
        public async Task<IActionResult> Export(int? classId, string subject)
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

            var csv = new StringBuilder();
            csv.AppendLine("学生姓名,班级,科目,分数,评级");

            foreach (var grade in grades)
            {
                var studentName = EscapeCsvValue(grade.Student.Name);
                var className = grade.Student.Class != null ? EscapeCsvValue(grade.Student.Class.ClassName) : "未分配班级";
                var subjectName = EscapeCsvValue(grade.Subject);
                var score = grade.Score;
                var rating = GetRating(score);

                csv.AppendLine($"{studentName},{className},{subjectName},{score},{rating}");
            }

            // 使用带BOM的UTF-8编码
            var encoding = new UTF8Encoding(true);
            var bytes = encoding.GetBytes(csv.ToString());

            var fileName = $"grades_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
            return File(bytes, "text/csv", fileName);
        }

        // 添加CSV值转义方法，处理包含逗号或引号的值
        private string EscapeCsvValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            // 如果值包含逗号、引号或换行符，则用引号括起来
            if (value.Contains(',') || value.Contains('"') || value.Contains('\n'))
            {
                // 双引号需要转义为两个双引号
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
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