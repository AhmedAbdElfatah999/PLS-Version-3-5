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
    public class student_subjectController : Controller
    {
        private readonly entitycoreContext _context;

        public student_subjectController(entitycoreContext context)
        {
            _context = context;
        }

        // GET: student_subject
        public async Task<IActionResult> Index()
        {
            var entitycoreContext = _context.student_subject_registration.Include(s => s.students).Include(s => s.subject).Include(s => s.teachers);
            return View(await entitycoreContext.ToListAsync());
        }

        // GET: student_subject/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_subject_registration = await _context.student_subject_registration
                .Include(s => s.students)
                .Include(s => s.subject)
                .Include(s => s.teachers)
                .FirstOrDefaultAsync(m => m.id == id);
            if (student_subject_registration == null)
            {
                return NotFound();
            }

            return View(student_subject_registration);
        }

        // GET: student_subject/Create
        public IActionResult Create()
        {
            ViewData["student_id"] = new SelectList(_context.students, "code", "code");
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id");
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id");
            return View();
        }

        // POST: student_subject/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,student_id,subject_id,teacher_id,grade,date")] student_subject_registration student_subject_registration)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student_subject_registration);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["student_id"] = new SelectList(_context.students, "code", "code", student_subject_registration.student_id);
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", student_subject_registration.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", student_subject_registration.teacher_id);
            return View(student_subject_registration);
        }

        // GET: student_subject/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_subject_registration = await _context.student_subject_registration.FindAsync(id);
            if (student_subject_registration == null)
            {
                return NotFound();
            }
            ViewData["student_id"] = new SelectList(_context.students, "code", "code", student_subject_registration.student_id);
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", student_subject_registration.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", student_subject_registration.teacher_id);
            return View(student_subject_registration);
        }

        // POST: student_subject/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,student_id,subject_id,teacher_id,grade,date")] student_subject_registration student_subject_registration)
        {
            if (id != student_subject_registration.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student_subject_registration);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!student_subject_registrationExists(student_subject_registration.id))
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
            ViewData["student_id"] = new SelectList(_context.students, "code", "code", student_subject_registration.student_id);
            ViewData["subject_id"] = new SelectList(_context.subject, "id", "id", student_subject_registration.subject_id);
            ViewData["teacher_id"] = new SelectList(_context.teachers, "id", "id", student_subject_registration.teacher_id);
            return View(student_subject_registration);
        }

        // GET: student_subject/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student_subject_registration = await _context.student_subject_registration
                .Include(s => s.students)
                .Include(s => s.subject)
                .Include(s => s.teachers)
                .FirstOrDefaultAsync(m => m.id == id);
            if (student_subject_registration == null)
            {
                return NotFound();
            }

            return View(student_subject_registration);
        }

        // POST: student_subject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student_subject_registration = await _context.student_subject_registration.FindAsync(id);
            _context.student_subject_registration.Remove(student_subject_registration);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool student_subject_registrationExists(int id)
        {
            return _context.student_subject_registration.Any(e => e.id == id);
        }
    }
}
