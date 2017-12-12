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
    public class ScheduledClassStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /ScheduledClassStatus/

        public ViewResult Index()
        {
            return View(db.SATScheduledClassStatuses.ToList());
        }

        //
        // GET: /ScheduledClassStatus/Details/5

        public ViewResult Details(int id)
        {
            SATScheduledClassStatus satscheduledclassstatus = db.SATScheduledClassStatuses.Single(s => s.SCSID == id);
            return View(satscheduledclassstatus);
        }

        //
        // GET: /ScheduledClassStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /ScheduledClassStatus/Create

        [HttpPost]
        public ActionResult Create(SATScheduledClassStatus satscheduledclassstatus)
        {
            if (ModelState.IsValid)
            {
                db.SATScheduledClassStatuses.AddObject(satscheduledclassstatus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(satscheduledclassstatus);
        }
        
        //
        // GET: /ScheduledClassStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATScheduledClassStatus satscheduledclassstatus = db.SATScheduledClassStatuses.Single(s => s.SCSID == id);
            return View(satscheduledclassstatus);
        }

        //
        // POST: /ScheduledClassStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(SATScheduledClassStatus satscheduledclassstatus)
        {
            if (ModelState.IsValid)
            {
                db.SATScheduledClassStatuses.Attach(satscheduledclassstatus);
                db.ObjectStateManager.ChangeObjectState(satscheduledclassstatus, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(satscheduledclassstatus);
        }

        //
        // GET: /ScheduledClassStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATScheduledClassStatus satscheduledclassstatus = db.SATScheduledClassStatuses.Single(s => s.SCSID == id);
            return View(satscheduledclassstatus);
        }

        //
        // POST: /ScheduledClassStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATScheduledClassStatus satscheduledclassstatus = db.SATScheduledClassStatuses.Single(s => s.SCSID == id);
            db.SATScheduledClassStatuses.DeleteObject(satscheduledclassstatus);
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