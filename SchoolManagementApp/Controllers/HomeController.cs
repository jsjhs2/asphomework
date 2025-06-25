using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchoolManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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

        public class UpdateLog
        {
            public string User { get; set; }
            public string Action { get; set; }
            public DateTime Time { get; set; }
        }

        public class HomeDashboardViewModel
        {
            public int StudentCount { get; set; }
            public int ClassCount { get; set; }
            public int CourseCount { get; set; }
            public int GradeCount { get; set; }
            public List<Classes> Classes { get; set; }
            public List<UpdateLog> RecentUpdates { get; set; }
        }

        public async Task<IActionResult> Index()
        {
            if (User.IsInRole("Student"))
            {
                return RedirectToAction("Index", "StudentDashboard");
            }
            var studentCount = await _context.Students.CountAsync();
            var classCount = await _context.Classes.CountAsync();
            var courseCount = await _context.Courses.CountAsync();
            var gradeCount = await _context.Grades.CountAsync();
            var classes = await _context.Classes.Include(c => c.Students).ToListAsync();
            var recentUpdates = await _context.OperationLogs
                .OrderByDescending(l => l.Time)
                .Take(5)
                .Select(l => new UpdateLog { User = l.UserName, Action = l.Action, Time = l.Time })
                .ToListAsync();
            // 如果数据库日志不足5条，补充静态演示数据
            var demoLogs = new List<UpdateLog> {
                new UpdateLog { User = "张老师", Action = "完成了数学期中考试成绩录入", Time = new DateTime(2025,6,24,15,30,0) },
                new UpdateLog { User = "李老师", Action = "新增了3个班级和120名学生信息", Time = new DateTime(2025,6,23,10,15,0) },
                new UpdateLog { User = "系统管理员", Action = "更新了系统界面，优化了移动端显示效果", Time = new DateTime(2025,6,22,16,40,0) }
            };
            foreach (var log in demoLogs)
            {
                if (recentUpdates.Count >= 5) break;
                // 避免重复添加
                if (!recentUpdates.Any(l => l.User == log.User && l.Action == log.Action && l.Time == log.Time))
                    recentUpdates.Add(log);
            }
            var vm = new HomeDashboardViewModel
            {
                StudentCount = studentCount,
                ClassCount = classCount,
                CourseCount = courseCount,
                GradeCount = gradeCount,
                Classes = classes,
                RecentUpdates = recentUpdates
            };
            return View(vm);
        }

        public class SchoolStatisticsViewModel
        {
            public List<ClassStatisticsViewModel> ClassStats { get; set; }
            public List<decimal> AllScores { get; set; }
            public int TotalStudentCount { get; set; }
            public decimal SchoolAverage { get; set; }
            public decimal SchoolPassRate { get; set; }
            public decimal SchoolExcellentRate { get; set; }
            public Dictionary<string, int> ScoreRanges { get; set; } // 分数段分布
        }

        public async Task<IActionResult> Statistics(int? classId = null)
        {
            var allClasses = await _context.Classes.ToListAsync();
            ViewBag.AllClasses = allClasses.Select(c => new SelectListItem { Value = c.ClassId.ToString(), Text = c.ClassName }).ToList();

            var classStats = await _context.Classes
                .Select(c => new ClassStatisticsViewModel
                {
                    ClassId = c.ClassId,
                    ClassName = c.ClassName,
                    StudentCount = c.Students.Count(),
                    AverageScore = c.Students.SelectMany(s => s.Grades).Any() ? c.Students.SelectMany(s => s.Grades).Average(g => g.Score) : 0,
                    MaxScore = c.Students.SelectMany(s => s.Grades).Any() ? c.Students.SelectMany(s => s.Grades).Max(g => g.Score) : 0,
                    MinScore = c.Students.SelectMany(s => s.Grades).Any() ? c.Students.SelectMany(s => s.Grades).Min(g => g.Score) : 0,
                    PassRate = c.Students.SelectMany(s => s.Grades).Any() ? (decimal)c.Students.SelectMany(s => s.Grades).Count(g => g.Score >= 60) / (c.Students.SelectMany(s => s.Grades).Count() == 0 ? 1 : c.Students.SelectMany(s => s.Grades).Count()) : 0,
                    ExcellentRate = c.Students.SelectMany(s => s.Grades).Any() ? (decimal)c.Students.SelectMany(s => s.Grades).Count(g => g.Score >= 90) / (c.Students.SelectMany(s => s.Grades).Count() == 0 ? 1 : c.Students.SelectMany(s => s.Grades).Count()) : 0
                })
                .ToListAsync();

            List<decimal> allScores;
            if (classId.HasValue)
            {
                allScores = await _context.Grades
                    .Where(g => g.Student.ClassId == classId.Value)
                    .Select(g => g.Score)
                    .ToListAsync();
            }
            else
            {
                allScores = await _context.Grades.Select(g => g.Score).ToListAsync();
            }
            int totalCount = allScores.Count;
            decimal schoolAvg = totalCount > 0 ? allScores.Average() : 0;
            decimal schoolPassRate = totalCount > 0 ? (decimal)allScores.Count(s => s >= 60) / totalCount : 0;
            decimal schoolExcellentRate = totalCount > 0 ? (decimal)allScores.Count(s => s >= 90) / totalCount : 0;
            var scoreRanges = new Dictionary<string, int>
            {
                { "90-100", allScores.Count(s => s >= 90 && s <= 100) },
                { "80-89", allScores.Count(s => s >= 80 && s < 90) },
                { "70-79", allScores.Count(s => s >= 70 && s < 80) },
                { "60-69", allScores.Count(s => s >= 60 && s < 70) },
                { "0-59", allScores.Count(s => s < 60) }
            };
            var schoolStats = new SchoolStatisticsViewModel
            {
                ClassStats = classStats,
                AllScores = allScores,
                TotalStudentCount = await _context.Students.CountAsync(),
                SchoolAverage = schoolAvg,
                SchoolPassRate = schoolPassRate,
                SchoolExcellentRate = schoolExcellentRate,
                ScoreRanges = scoreRanges
            };
            return View(schoolStats);
        }

        // 新增：定义统计用ViewModel
        public class ClassStatisticsViewModel
        {
            public int ClassId { get; set; }
            public string ClassName { get; set; }
            public int StudentCount { get; set; }
            public decimal AverageScore { get; set; }
            public decimal MaxScore { get; set; }
            public decimal MinScore { get; set; }
            public decimal PassRate { get; set; }
            public decimal ExcellentRate { get; set; }
        }
    }
}