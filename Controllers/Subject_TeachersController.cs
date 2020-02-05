using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrivateLessons.Data;
using PrivateLessons.Models;

namespace PrivateLessons.Controllers
{
    public class Subject_TeachersController : Controller
    {
        private readonly entitycoreContext _context;

        public Subject_TeachersController(entitycoreContext context)
        {
            _context = context;
        }

        // GET: Subject_Teachers
        public async Task<IActionResult> Index()
        {
            var entitycoreContext = _context.subject_teachers.Include(s => s.subject).Include(s => s.teachers);
            return View(await entitycoreContext.ToListAsync());
        }

        // GET: Subject_Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject_teachers = await _context.subject_teachers
                .Include(s => s.subject)
                .Include(s => s.teachers)
                .FirstOrDefaultAsync(m => m.id == id);
            if (subject_teachers == null)
            {
                return NotFound();
            }

            return View(subject_teachers);
        }

        // GET: Subject_Teachers/Create
        public IActionResult Create()
        {
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id");
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id");
            return View();
        }

        // POST: Subject_Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,subject_id,teacher_id")] subject_teachers subject_teachers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject_teachers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", subject_teachers.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", subject_teachers.teacher_id);
            return View(subject_teachers);
        }

        // GET: Subject_Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject_teachers = await _context.subject_teachers.FindAsync(id);
            if (subject_teachers == null)
            {
                return NotFound();
            }
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", subject_teachers.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", subject_teachers.teacher_id);
            return View(subject_teachers);
        }

        // POST: Subject_Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,subject_id,teacher_id")] subject_teachers subject_teachers)
        {
            if (id != subject_teachers.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject_teachers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subject_teachersExists(subject_teachers.id))
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
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", subject_teachers.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", subject_teachers.teacher_id);
            return View(subject_teachers);
        }

        // GET: Subject_Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject_teachers = await _context.subject_teachers
                .Include(s => s.subject)
                .Include(s => s.teachers)
                .FirstOrDefaultAsync(m => m.teacher_id == id);
            if (subject_teachers == null)
            {
                return NotFound();
            }

            return View(subject_teachers);
        }

        // POST: Subject_Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject_teachers = await _context.subject_teachers.FindAsync(id);
            _context.subject_teachers.Remove(subject_teachers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool subject_teachersExists(int id)
        {
            return _context.subject_teachers.Any(e => e.id == id);
        }
    }
}
