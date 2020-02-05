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
    public class SubjectsController : Controller
    {
        private readonly entitycoreContext _context;

        public SubjectsController(entitycoreContext context)
        {
            _context = context;
        }

        // GET: Subjects
        public async Task<IActionResult> Index(string searchString)
{
    var Subjects = from m in _context.subject
                 select m;

    if (!String.IsNullOrEmpty(searchString))
    {
        Subjects= Subjects.Where(s => s.name.Contains(searchString));
    }

    return View(await Subjects.ToListAsync());
}

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subject
                .FirstOrDefaultAsync(m => m.id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subjects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,name,date,time,price,place")] subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subject.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,name,date,time,price,place")] subject subject)
        {
            if (id != subject.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!subjectExists(subject.id))
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
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subject = await _context.subject
                .FirstOrDefaultAsync(m => m.id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            
            var subjectTeacher = await _context.subject_teachers.FirstOrDefaultAsync(entry=>entry.subject_id==id);
            _context.subject_teachers.Remove(subjectTeacher);
            await _context.SaveChangesAsync();
            
            var subject = await _context.subject.FindAsync(id);
            _context.subject.Remove(subject);
            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool subjectExists(int id)
        {
            return _context.subject.Any(e => e.id == id);
        }
    }
}
