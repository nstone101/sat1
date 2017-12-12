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
    public class StudentStatusController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /StudentStatus/

        public ViewResult Index()
        {
            return View(db.SATStudentStatuses.ToList());
        }

        //
        // GET: /StudentStatus/Details/5

        public ViewResult Details(int id)
        {
            SATStudentStatus satstudentstatus = db.SATStudentStatuses.Single(s => s.SSID == id);
            return View(satstudentstatus);
        }

        //
        // GET: /StudentStatus/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /StudentStatus/Create

        [HttpPost]
        public ActionResult Create(SATStudentStatus satstudentstatus)
        {
            if (ModelState.IsValid)
            {
                db.SATStudentStatuses.AddObject(satstudentstatus);
                db.SaveChanges();
                return RedirectToAction("Index");  
            }

            return View(satstudentstatus);
        }
        
        //
        // GET: /StudentStatus/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATStudentStatus satstudentstatus = db.SATStudentStatuses.Single(s => s.SSID == id);
            return View(satstudentstatus);
        }

        //
        // POST: /StudentStatus/Edit/5

        [HttpPost]
        public ActionResult Edit(SATStudentStatus satstudentstatus)
        {
            if (ModelState.IsValid)
            {
                db.SATStudentStatuses.Attach(satstudentstatus);
                db.ObjectStateManager.ChangeObjectState(satstudentstatus, EntityState.Modified);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(satstudentstatus);
        }

        //
        // GET: /StudentStatus/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATStudentStatus satstudentstatus = db.SATStudentStatuses.Single(s => s.SSID == id);
            return View(satstudentstatus);
        }

        //
        // POST: /StudentStatus/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATStudentStatus satstudentstatus = db.SATStudentStatuses.Single(s => s.SSID == id);
            db.SATStudentStatuses.DeleteObject(satstudentstatus);
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