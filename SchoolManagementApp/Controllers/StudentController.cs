using ClosedXML.Excel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SchoolManagementApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ModelState.AddModelError("file", "请选择一个Excel文件");
                return View();
            }

            try
            {
                Console.WriteLine($"开始处理导入文件: {file.FileName}");

                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    using (var workbook = new XLWorkbook(stream))
                    {
                        var worksheet = workbook.Worksheet(1);
                        var students = new List<Student>();
                        var errors = new List<string>();
                        var processedRows = 0;

                        // 获取当前年份（用于学号生成）
                        var currentYear = DateTime.Now.Year.ToString();

                        // 用于存储已生成的学号，确保唯一性
                        var generatedRollNumbers = new HashSet<string>();

                        // 预加载数据库中已存在的学号
                        var existingRollNumbers = await _context.Students
                            .Select(s => s.RollNumber)
                            .ToListAsync();

                        // 合并为一个集合
                        foreach (var rollNumber in existingRollNumbers)
                        {
                            generatedRollNumbers.Add(rollNumber);
                        }

                        // 获取表头，验证列顺序
                        var headers = new List<string>();
                        for (int col = 1; col <= worksheet.LastColumnUsed().ColumnNumber(); col++)
                        {
                            headers.Add(worksheet.Cell(1, col).GetValue<string>().Trim());
                        }

                        // 验证必要的列存在
                        var requiredColumns = new[] { "姓名", "班级" };
                        foreach (var col in requiredColumns)
                        {
                            if (!headers.Contains(col))
                            {
                                errors.Add($"Excel文件缺少必要的列: '{col}'");
                            }
                        }

                        if (errors.Any())
                        {
                            Console.WriteLine("导入失败: Excel格式不正确");
                            foreach (var error in errors)
                            {
                                Console.WriteLine($"错误: {error}");
                            }

                            ModelState.AddModelError("", "导入失败，请检查Excel格式");
                            foreach (var error in errors)
                            {
                                ModelState.AddModelError("", error);
                            }
                            return View();
                        }

                        // 获取列索引
                        var nameIndex = headers.IndexOf("姓名") + 1;
                        var classNameIndex = headers.IndexOf("班级") + 1;
                        var genderIndex = headers.Contains("性别") ? headers.IndexOf("性别") + 1 : -1;
                        var birthDateIndex = headers.Contains("出生日期") ? headers.IndexOf("出生日期") + 1 : -1;
                        var emailIndex = headers.Contains("邮箱") ? headers.IndexOf("邮箱") + 1 : -1;
                        var phoneIndex = headers.Contains("联系电话") ? headers.IndexOf("联系电话") + 1 : -1;

                        Console.WriteLine($"开始处理数据行，总共有 {worksheet.LastRowUsed().RowNumber() - 1} 行数据");

                        // 预加载所有班级信息并建立映射（忽略大小写和空格）
                        var allClasses = await _context.Classes.ToListAsync();

                        // 检查是否有班级名称重复（忽略大小写和空格）
                        var duplicateClasses = allClasses
                            .GroupBy(c => c.ClassName.Trim().ToLower().Replace(" ", ""))
                            .Where(g => g.Count() > 1)
                            .ToList();

                        if (duplicateClasses.Any())
                        {
                            var duplicateNames = string.Join(", ", duplicateClasses.Select(g => g.Key));
                            errors.Add($"数据库中存在重复的班级名称（忽略大小写和空格）: {duplicateNames}");
                            Console.WriteLine($"警告: 数据库中存在重复的班级名称: {duplicateNames}");
                        }

                        // 建立班级名称到班级对象的映射
                        var classNameToClassMap = allClasses
                            .ToDictionary(
                                c => c.ClassName.Trim().ToLower().Replace(" ", ""),
                                c => c,
                                StringComparer.OrdinalIgnoreCase
                            );

                        // 默认班级（确保数据库中存在）
                        var defaultClassName = "默认班级";
                        var defaultClass = allClasses.FirstOrDefault(c =>
                            c.ClassName.Trim().ToLower() == defaultClassName.ToLower())
                            ?? new Classes { ClassName = defaultClassName };

                        // 如果默认班级不存在，先创建它
                        if (defaultClass.ClassId == 0)
                        {
                            _context.Classes.Add(defaultClass);
                            await _context.SaveChangesAsync();
                            allClasses.Add(defaultClass);
                            classNameToClassMap[defaultClassName.ToLower()] = defaultClass;
                            Console.WriteLine($"创建默认班级: {defaultClassName}, ID: {defaultClass.ClassId}");
                        }

                        // 随机数生成器
                        var random = new Random();

                        // 从第二行开始读取数据
                        for (int row = 2; row <= worksheet.LastRowUsed().RowNumber(); row++)
                        {
                            try
                            {
                                processedRows++;
                                Console.WriteLine($"处理第 {row} 行数据");

                                // 读取姓名（必需字段）
                                var name = worksheet.Cell(row, nameIndex).GetValue<string>()?.Trim();
                                if (string.IsNullOrEmpty(name))
                                {
                                    errors.Add($"第{row}行: 姓名不能为空");
                                    Console.WriteLine($"第 {row} 行错误: 姓名不能为空");
                                    continue;
                                }

                                // 读取班级名称（必需字段）
                                var className = worksheet.Cell(row, classNameIndex).GetValue<string>()?.Trim();
                                if (string.IsNullOrEmpty(className))
                                {
                                    errors.Add($"第{row}行: 班级名称不能为空");
                                    Console.WriteLine($"第 {row} 行错误: 班级名称不能为空");
                                    className = defaultClass.ClassName;
                                }

                                // 尝试匹配班级名称
                                Classes classInfo;
                                var normalizedClassName = className.Trim().ToLower().Replace(" ", "");

                                if (classNameToClassMap.TryGetValue(normalizedClassName, out var matchedClass))
                                {
                                    classInfo = matchedClass;
                                    Console.WriteLine($"第 {row} 行: 班级 '{className}' 匹配成功，ID: {classInfo.ClassId}");
                                }
                                else
                                {
                                    // 尝试模糊匹配
                                    var similarClass = FindSimilarClass(allClasses, className);
                                    if (similarClass != null)
                                    {
                                        classInfo = similarClass;
                                        errors.Add($"第{row}行: 班级 '{className}' 不完全匹配，已分配至 '{classInfo.ClassName}'");
                                        Console.WriteLine($"第 {row} 行: 班级 '{className}' 不完全匹配，已分配至 '{classInfo.ClassName}'，ID: {classInfo.ClassId}");
                                    }
                                    else
                                    {
                                        // 创建新班级
                                        classInfo = new Classes { ClassName = className };
                                        _context.Classes.Add(classInfo);
                                        await _context.SaveChangesAsync();

                                        // 更新映射
                                        classNameToClassMap[normalizedClassName] = classInfo;
                                        allClasses.Add(classInfo);

                                        errors.Add($"第{row}行: 班级 '{className}' 不存在，已创建新班级");
                                        Console.WriteLine($"第 {row} 行: 创建新班级 '{className}'，ID: {classInfo.ClassId}");
                                    }
                                }

                                // 读取性别（可选，提供默认值）
                                var gender = genderIndex > 0
                                    ? worksheet.Cell(row, genderIndex).GetValue<string>()?.Trim()
                                    : "未知";

                                // 处理出生日期（可选，提供默认值）
                                DateTime birthDate;
                                if (birthDateIndex > 0)
                                {
                                    var birthValue = worksheet.Cell(row, birthDateIndex).GetValue<string>()?.Trim();
                                    birthDate = DateTime.TryParse(birthValue, out birthDate)
                                        ? birthDate
                                        : DateTime.Now.AddYears(-18);

                                    if (string.IsNullOrEmpty(birthValue))
                                        errors.Add($"第{row}行: 出生日期为空，使用默认值");
                                    else if (birthDate == DateTime.MinValue)
                                        errors.Add($"第{row}行: 出生日期格式无效 '{birthValue}'，使用默认值");
                                }
                                else
                                {
                                    birthDate = DateTime.Now.AddYears(-18);
                                    errors.Add($"第{row}行: 缺少出生日期列，使用默认值");
                                }

                                // 读取邮箱（可选，提供默认值）
                                var email = emailIndex > 0
                                    ? worksheet.Cell(row, emailIndex).GetValue<string>()?.Trim()
                                    : $"{name}@example.com";

                                // 读取联系电话（可选，提供默认值）
                                var phone = phoneIndex > 0
                                    ? worksheet.Cell(row, phoneIndex).GetValue<string>()?.Trim()
                                    : "00000000000";

                                // 生成唯一学号 - 当前年份+8位随机数
                                string rollNumber;
                                do
                                {
                                    // 生成8位随机数（10000000-99999999）
                                    var randomDigits = random.Next(10000000, 100000000).ToString();
                                    rollNumber = $"{currentYear}{randomDigits}";
                                }
                                while (generatedRollNumbers.Contains(rollNumber));

                                // 添加到已生成的学号集合
                                generatedRollNumbers.Add(rollNumber);

                                // 创建学生对象
                                var student = new Student
                                {
                                    Name = name,
                                    Gender = gender,
                                    DateOfBirth = birthDate,
                                    Email = email,
                                    PhoneNumber = phone,
                                    ClassId = classInfo.ClassId,
                                    RollNumber = rollNumber
                                };

                                students.Add(student);
                                Console.WriteLine($"第 {row} 行: 成功添加学生 {name}，学号: {rollNumber}，班级: {className}");
                            }
                            catch (Exception ex)
                            {
                                errors.Add($"第{row}行: 处理失败 - {ex.Message}");
                                Console.WriteLine($"第 {row} 行: 处理失败 - {ex.Message}");
                                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                            }
                        }

                        Console.WriteLine($"处理完成，共 {processedRows} 行，成功 {students.Count} 行，失败 {errors.Count} 行");

                        // 检查是否有数据要导入
                        if (!students.Any())
                        {
                            Console.WriteLine("没有有效的学生数据可导入");
                            ModelState.AddModelError("", "没有有效的学生数据可导入");
                            if (errors.Any())
                            {
                                ModelState.AddModelError("", "导入过程中出现以下错误:");
                                foreach (var error in errors)
                                {
                                    ModelState.AddModelError("", error);
                                }
                            }
                            return View();
                        }

                        // 使用数据库事务确保数据完整性
                        using (var transaction = _context.Database.BeginTransaction())
                        {
                            try
                            {
                                // 保存学生信息到数据库
                                _context.Students.AddRange(students);
                                var savedCount = await _context.SaveChangesAsync();

                                // 提交事务
                                transaction.Commit();

                                Console.WriteLine($"成功保存 {savedCount} 名学生到数据库");

                                // 显示成功信息和错误信息
                                if (errors.Any())
                                {
                                    Console.WriteLine($"导入完成，但有 {errors.Count} 个错误");
                                    TempData["ImportErrors"] = errors;
                                }

                                TempData["SuccessMessage"] = $"成功导入 {savedCount} 名学生，共处理 {processedRows} 行数据";

                                return RedirectToAction(nameof(Index));
                            }
                            catch (Exception ex)
                            {
                                // 回滚事务
                                transaction.Rollback();
                                Console.WriteLine($"导入过程中发生错误: {ex.Message}");
                                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                                ModelState.AddModelError("", "导入过程中发生错误: " + ex.Message);
                                return View();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"导入文件时发生致命错误: {ex.Message}");
                Console.WriteLine($"异常堆栈: {ex.StackTrace}");
                ModelState.AddModelError("", "导入文件时发生致命错误: " + ex.Message);
                return View();
            }
        }

        // 辅助方法：查找最接近的班级名称匹配
        private Classes FindSimilarClass(List<Classes> classes, string targetName)
        {
            if (string.IsNullOrEmpty(targetName))
                return null;

            var normalizedTarget = targetName.Trim().ToLower();

            // 先尝试精确匹配（忽略大小写）
            var exactMatch = classes.FirstOrDefault(c => c.ClassName.Trim().ToLower() == normalizedTarget);
            if (exactMatch != null)
                return exactMatch;

            // 尝试部分匹配
            var partialMatch = classes.FirstOrDefault(c =>
                normalizedTarget.Contains(c.ClassName.Trim().ToLower()) ||
                c.ClassName.Trim().ToLower().Contains(normalizedTarget));
            if (partialMatch != null)
                return partialMatch;

            // 尝试模糊匹配（使用编辑距离）
            var closestMatch = classes
                .OrderBy(c => ComputeLevenshteinDistance(c.ClassName.Trim().ToLower(), normalizedTarget))
                .FirstOrDefault();

            // 如果编辑距离小于等于2，认为是相似的
            if (closestMatch != null && ComputeLevenshteinDistance(closestMatch.ClassName.Trim().ToLower(), normalizedTarget) <= 2)
                return closestMatch;

            return null;
        }

        // 计算两个字符串之间的编辑距离（Levenshtein距离）
        private int ComputeLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
                return string.IsNullOrEmpty(t) ? 0 : t.Length;

            if (string.IsNullOrEmpty(t))
                return s.Length;

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            // 初始化第一列和第一行
            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            // 填充矩阵
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1),
                        d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }


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