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
    public class EnrollmentController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Enrollment/

        public ViewResult Index()
        {
            var satenrollments = db.SATEnrollments.Include("SATScheduledClass").Include("SATStudent");
            return View(satenrollments.ToList());
        }

        //
        // GET: /Enrollment/Details/5

        public ViewResult Details(int id)
        {
            SATEnrollment satenrollment = db.SATEnrollments.Single(s => s.enrollmentId == id);
            return View(satenrollment);
        }

        //
        // GET: /Enrollment/Create

        public ActionResult Create()
        {
            ViewBag.scheduledClassId = 
                new SelectList(db.SATScheduledClasses.Where(x => x.statusId == 1), "scheduledClassId", "Display");
            ViewBag.studentId = 
                new SelectList(db.SATStudents.Where(x => x.statusId == 1), "studentId", "Display");
            return View();
        } 





        //
        // POST: /Enrollment/Create

        [HttpPost]
        public ActionResult Create(SATEnrollment satenrollment)
        {
            if (ModelState.IsValid)
            {
                db.SATEnrollments.AddObject(satenrollment);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            ViewBag.scheduledClassId = 
                new SelectList(db.SATScheduledClasses, "scheduledClassId", "instructorName", satenrollment.scheduledClassId);
            ViewBag.studentId = 
                new SelectList(db.SATStudents, "studentId", "firstName", satenrollment.studentId);
            return View(satenrollment);
        }
        
        //
        // GET: /Enrollment/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATEnrollment satenrollment = db.SATEnrollments.Single(s => s.enrollmentId == id);
            ViewBag.scheduledClassId = new SelectList(db.SATScheduledClasses, "scheduledClassId", "Display", satenrollment.scheduledClassId);
            ViewBag.studentId = new SelectList(db.SATStudents, "studentId", "Display", satenrollment.studentId);
            return View(satenrollment);
        }




        //
        // POST: /Enrollment/Edit/5

        [HttpPost]
        public ActionResult Edit(SATEnrollment satenrollment)
        {
            if (ModelState.IsValid)
            {
                db.SATEnrollments.Attach(satenrollment);
                db.ObjectStateManager.ChangeObjectState(satenrollment, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.scheduledClassId = new SelectList(db.SATScheduledClasses, "scheduledClassId", "Display", satenrollment.scheduledClassId);
            ViewBag.studentId = new SelectList(db.SATStudents, "studentId", "Display", satenrollment.studentId);
            return View(satenrollment);
        }

        //
        // GET: /Enrollment/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATEnrollment satenrollment = db.SATEnrollments.Single(s => s.enrollmentId == id);
            return View(satenrollment);
        }

        //
        // POST: /Enrollment/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATEnrollment satenrollment = db.SATEnrollments.Single(s => s.enrollmentId == id);
            db.SATEnrollments.DeleteObject(satenrollment);
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