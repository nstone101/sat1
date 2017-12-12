using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SAT1;

namespace SAT1.Controllers
{ 
    
    public class CourseController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Course/
        
        public ViewResult Index(bool? showInactive, string department)
        {
            var satcourses = db.SATCourses.
                Include("SATCourseStatus").
                OrderBy(x => x.courseName).ToList();

            if (!User.IsInRole("admin"))
            {
                satcourses = (from s in satcourses
                              where s.statusId == 1
                              select s).ToList();
            }
            if (showInactive != null)
            {
                satcourses = (from s in satcourses
                              where s.statusId == ((showInactive == true) ? 2 : 1)
                              select s).ToList(); 
            
            
            }

           
            if(!String.IsNullOrEmpty(department))
            { 
                satcourses = (from c in satcourses
                             where c.department == department
                             select c).ToList();
            }
            //for a status ddl - add a SelectList to the Viewbag 
            ViewBag.StatusId = new SelectList(
                db.SATCourseStatuses, "CSID", "CSName");

            ViewBag.department = new SelectList(
                db.SATCourses.Select(x => new { x.department }).Distinct(), "department", "department");
            
            
                
            return View(satcourses.ToList());
        }

        //
        // GET: /Course/Details/5
        
        public ViewResult Details(int id)
        {
            
            SATCours satcours = db.SATCourses.Single(s => s.courseId == id);
            return View(satcours);
        }

        //
        // GET: /Course/Create
        [Authorize(Roles="admin")]
        public ActionResult Create()
        {
           
            ViewBag.statusId = new SelectList(db.SATCourseStatuses, "CSID", "CSName");
            return View();
            
        } 

        //
        // POST: /Course/Create

        [HttpPost, Authorize(Roles="admin")]
        public ActionResult Create(SATCours satcours)
        {
            if (ModelState.IsValid)
            {
                db.SATCourses.AddObject(satcours);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.statusId = new SelectList(db.SATCourseStatuses, "CSID", "CSName", satcours.statusId);
            return View(satcours);
        }
        
        //
        // GET: /Course/Edit/5
        [Authorize(Roles="admin")]
        public ActionResult Edit(int id)
        {
            SATCours satcours = db.SATCourses.Single(s => s.courseId == id);
            ViewBag.statusId = new SelectList(db.SATCourseStatuses, "CSID", "CSName", satcours.statusId);
            return View(satcours);
        }

        //
        // POST: /Course/Edit/5

        [HttpPost]
        public ActionResult Edit(SATCours satcours)
        {
            if (ModelState.IsValid)
            {
                db.SATCourses.Attach(satcours);
                db.ObjectStateManager.ChangeObjectState(satcours, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.statusId = new SelectList(db.SATCourseStatuses, "CSID", "CSName", satcours.statusId);
            return View(satcours);
        }

        //
        // GET: /Course/Delete/5
        [Authorize(Roles="admin")]
        public ActionResult Delete(int id)
        {
            SATCours satcours = db.SATCourses.Single(s => s.courseId == id);
            return View(satcours);
        }

        //
        // POST: /Course/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATCours satcours = db.SATCourses.Single(s => s.courseId == id);
            satcours.statusId = 2;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}