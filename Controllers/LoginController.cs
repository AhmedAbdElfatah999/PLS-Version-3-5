using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrivateLessons.Data;
using PrivateLessons.Models;
using Microsoft.AspNetCore.Session;


namespace PrivateLessons.Controllers
{
    public class LoginController : Controller
    {
       
        public static string userName;
        public static string Password;

       private readonly entitycoreContext _context;

        public LoginController(entitycoreContext context)
        {
            _context = context;
        }

        // GET: Login
       public async Task<IActionResult> Index(string xusername,string xpassword)
                   {
                              
                      if (!String.IsNullOrEmpty(xusername) && !String.IsNullOrEmpty(xpassword))
                           {
                                 userName=xusername;
                                 Password=xpassword;
                                  await Task.Delay(1);
                                  return RedirectToAction("Details");
                           
                            }
                              else
                                  {
                                     return View();
                                 }        
                   }

             
        // GET: Login/Details/5
        public async Task<IActionResult> Details()
        { 
                var user = _context.users
                .Where(m => m.username ==userName && m.password==Password);
            if (userName==null || Password==null)
                {
                return RedirectToAction("Index");
               }
               else if(user ==null){
                   return RedirectToAction("Index");
               }

             else{
            user =_context.users
                .Where(m => m.username ==userName && m.password==Password);
                 await Task.Delay(1);              
              return View(user);
              
             } 
          
        }
        //Login/My Courses student
           public async Task<IActionResult> MyCourses()
        { 
             int xid =default;
            var xuser = _context.users
                .Where(m => m.username ==userName && m.password==Password);
            foreach (var item in xuser)
            {
               xid=item.id;   
            }    
        
            var MyCourses =_context.student_subject_registration.Where(s=> s.student_id==xid).Include(entry=>entry.subject);
            //.ThenInclude(entry=>entry.teachers);

            if (MyCourses !=null)
                {
                     await Task.Delay(1);
                    return View(MyCourses);
              
               }
        
             else{             
              
              return RedirectToAction("Details");
             } 
          
        }
        //course Details for student
        public async Task<IActionResult> CourseDetails(int? id)
        {   
            if (id == null)
            {
                return NotFound();
            }
             var xsubject =await _context.subject_teachers
                .FirstOrDefaultAsync(m => m.subject_id == id);

             var user = await _context.users
                .SingleOrDefaultAsync(m => m.id ==xsubject.teacher_id);
            
            var subject = await _context.subject
                .FirstOrDefaultAsync(m => m.id == id);

            List<string> allData = new List<string>();
            allData.Add(subject.name);
            allData.Add(subject.date.ToString());
            allData.Add(subject.time);
            allData.Add(subject.price.ToString());
            allData.Add(subject.place);
            allData.Add(user.f_name);
            allData.Add(user.l_name);
           
            if (subject == null)
            {
                return NotFound();
            }
         
            return View(allData);
        }
       
       //Teacher's Courses
        public async Task<IActionResult> TeacherCourses()
        { 
             int xid =default;
            var xuser = _context.users
                .Where(m => m.username ==userName && m.password==Password);
            foreach (var item in xuser)
            {
               xid=item.id;   
            }    
            var TeacherCourses =_context.subject_teachers
            .Where(s=> s.teacher_id==xid).Include(entry=>entry.subject);
            //.ThenInclude(entry=>entry.teachers);

            if (TeacherCourses !=null)
                {
                     await Task.Delay(1);
                    return View(TeacherCourses);
              
               }
        
             else{             
              
              return RedirectToAction("Details");
             } 
          
        }


       //Teacher can Delete course
        public IActionResult DeleteCourse()
        {
            return RedirectToAction("Delete","Subjects");
        }


      //teacher can edit the course
       public IActionResult EditCourse(int? id)
        {
            return RedirectToAction("Edit","Subjects");
        }

        public IActionResult CreateCourse()
        {
            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse([Bind("id,name,date,time,price,place")] subject subject)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subject);
                await _context.SaveChangesAsync();
                var teacher=await _context.users
                .SingleOrDefaultAsync(entry=>entry.username==userName && entry.password==Password);
               _context.subject_teachers.Add(new subject_teachers{teacher_id=teacher.id,subject_id=subject.id});
                 await _context.SaveChangesAsync();
                return RedirectToAction(nameof(TeacherCourses));
            }
            return View("Details");
        }

      

        // POST: Login/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,f_name,l_name,email,type_id")] users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }

        // GET: Login/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Login/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,f_name,l_name,email,type_id")] users users)
        {
            if (id != users.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usersExists(users.id))
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
            return View(users);
        }

        // GET: Login/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var users = await _context.users
                .FirstOrDefaultAsync(m => m.id == id);
            if (users == null)
            {
                return NotFound();
            }

            return View(users);
        }

        // POST: Login/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var users = await _context.users.FindAsync(id);
            _context.users.Remove(users);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool usersExists(int id)
        {
            return _context.users.Any(e => e.id == id);
        }
    }
}
