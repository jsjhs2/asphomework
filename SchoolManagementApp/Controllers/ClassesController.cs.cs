using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.Models;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace SchoolManagementApp.Controllers
{
    [Authorize(Roles = "Admin,Teacher")]
    public class ClassesController : Controller
    {
        private readonly SchoolContext _context;

        public ClassesController(SchoolContext context)
        {
            _context = context;
        }

        // GET: Classes
        public async Task<IActionResult> Index()
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .ToListAsync();

            return View(classes);
        }

        // GET: Classes/Manage
        public async Task<IActionResult> Manage()
        {
            var classes = await _context.Classes.Include(c => c.Students).ToListAsync();
            return View(classes);
        }

        // GET: Classes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Classes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClassName,ClassCode,TeacherName,Description")] Classes classes)
        {
            if (ModelState.IsValid)
            {
                classes.CreatedDate = DateTime.Now;
                _context.Add(classes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(classes);
        }

        // GET: Classes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes.FindAsync(id);
            if (classes == null)
            {
                return NotFound();
            }
            return View(classes);
        }

        // POST: Classes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClassId,ClassName,ClassCode,TeacherName,Description,CreatedDate")] Classes classes)
        {
            if (id != classes.ClassId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    classes.LastUpdatedDate = DateTime.Now;
                    _context.Update(classes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassesExists(classes.ClassId))
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
            return View(classes);
        }

        // GET: Classes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classes = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(m => m.ClassId == id);

            if (classes == null)
            {
                return NotFound();
            }

            if (classes.Students?.Count > 0)
            {
                ViewBag.ErrorMessage = "无法删除，该班级下还有学生。请先移除或转移学生后再删除班级。";
            }

            return View(classes);
        }

        // POST: Classes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var classes = await _context.Classes
                .Include(c => c.Students)
                .FirstOrDefaultAsync(c => c.ClassId == id);

            if (classes == null)
            {
                return NotFound();
            }

            if (classes.Students?.Count > 0)
            {
                ModelState.AddModelError("", "无法删除，该班级下还有学生。");
                return View("Delete", classes);
            }

            _context.Classes.Remove(classes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClassesExists(int id)
        {
            return _context.Classes.Any(e => e.ClassId == id);
        }
    }
}