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
    public class ScheduledClassController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /ScheduledClass/

        public ViewResult Index()
        {
            var satscheduledclasses = db.SATScheduledClasses.Include("SATCours").Include("SATScheduledClassStatus");
            return View(satscheduledclasses.ToList());
        }

        //
        // GET: /ScheduledClass/Details/5

        public ViewResult Details(int id)
        {
            SATScheduledClass satscheduledclass = db.SATScheduledClasses.Single(s => s.scheduledClassId == id);
            return View(satscheduledclass);
        }

        //
        // GET: /ScheduledClass/Create

        public ActionResult Create()
        {
            ViewBag.courseId = new SelectList(db.SATCourses, "courseId", "courseName");
            ViewBag.statusId = new SelectList(db.SATScheduledClassStatuses, "SCSID", "SCSName");
            return View();
        } 

        //
        // POST: /ScheduledClass/Create

        [HttpPost]
        public ActionResult Create(SATScheduledClass satscheduledclass)
        {
            if (ModelState.IsValid)
            {
                db.SATScheduledClasses.AddObject(satscheduledclass);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.courseId = new SelectList(db.SATCourses, "courseId", "courseName", satscheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.SATScheduledClassStatuses, "SCSID", "SCSName", satscheduledclass.statusId);
            return View(satscheduledclass);
        }
        
        //
        // GET: /ScheduledClass/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATScheduledClass satscheduledclass = db.SATScheduledClasses.Single(s => s.scheduledClassId == id);
            ViewBag.courseId = new SelectList(db.SATCourses, "courseId", "courseName", satscheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.SATScheduledClassStatuses, "SCSID", "SCSName", satscheduledclass.statusId);
            return View(satscheduledclass);
        }

        //
        // POST: /ScheduledClass/Edit/5

        [HttpPost]
        public ActionResult Edit(SATScheduledClass satscheduledclass)
        {
            if (ModelState.IsValid)
            {
                db.SATScheduledClasses.Attach(satscheduledclass);
                db.ObjectStateManager.ChangeObjectState(satscheduledclass, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseId = new SelectList(db.SATCourses, "courseId", "courseName", satscheduledclass.courseId);
            ViewBag.statusId = new SelectList(db.SATScheduledClassStatuses, "SCSID", "SCSName", satscheduledclass.statusId);
            return View(satscheduledclass);
        }

        //
        // GET: /ScheduledClass/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATScheduledClass satscheduledclass = db.SATScheduledClasses.Single(s => s.scheduledClassId == id);
            return View(satscheduledclass);
        }

        //
        // POST: /ScheduledClass/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATScheduledClass satscheduledclass = db.SATScheduledClasses.Single(s => s.scheduledClassId == id);
            db.SATScheduledClasses.DeleteObject(satscheduledclass);
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