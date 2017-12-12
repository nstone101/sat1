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
    public class CourseStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /CourseStatus/

        public ViewResult Index()
        {
            return View(db.SATCourseStatuses.ToList());
        }

        //
        // GET: /CourseStatus/Details/5

        public ViewResult Details(int id)
        {
            SATCourseStatus satcoursestatus = db.SATCourseStatuses.Single(s => s.CSID == id);
            return View(satcoursestatus);
        }

        //
        // GET: /CourseStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /CourseStatus/Create

        [HttpPost]
        public ActionResult Create(SATCourseStatus satcoursestatus)
        {
            if (ModelState.IsValid)
            {
                db.SATCourseStatuses.AddObject(satcoursestatus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(satcoursestatus);
        }
        
        //
        // GET: /CourseStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATCourseStatus satcoursestatus = db.SATCourseStatuses.Single(s => s.CSID == id);
            return View(satcoursestatus);
        }

        //
        // POST: /CourseStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(SATCourseStatus satcoursestatus)
        {
            if (ModelState.IsValid)
            {
                db.SATCourseStatuses.Attach(satcoursestatus);
                db.ObjectStateManager.ChangeObjectState(satcoursestatus, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(satcoursestatus);
        }

        //
        // GET: /CourseStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATCourseStatus satcoursestatus = db.SATCourseStatuses.Single(s => s.CSID == id);
            return View(satcoursestatus);
        }

        //
        // POST: /CourseStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATCourseStatus satcoursestatus = db.SATCourseStatuses.Single(s => s.CSID == id);
            db.SATCourseStatuses.DeleteObject(satcoursestatus);
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