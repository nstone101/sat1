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
    [Authorize(Roles = "admin")]
    public class StudentController : Controller
    {
        private SATEntities db = new SATEntities();

        //
        // GET: /Student/
        //to Make a search within the students page/table
        public ViewResult Index(string search, bool showInactive = false)
        {
            //select all student and turn the collection into a list
            var satstudents = db.SATStudents.
                Include("SATStudentStatus").OrderBy(x => x.lastName).
                ToList();

            if(showInactive)
            {
                satstudents = (from s in satstudents
                              where s.statusId > 3
                              select s).ToList();
            }
            //else
            //{
            //satstudents = (from s in satstudents
            //               where s.statusId
            
            
            //}


            //Check to see if the search string has a value
            if (!string.IsNullOrEmpty(search))
            { 
                search = search.ToUpper();
                //search by first name, lastname, and major
                satstudents = (from s in satstudents
                               where s.firstName.ToUpper().Contains(search) ||
                               s.lastName.ToUpper().Contains(search) ||

                               //major is nullable
                               s.major != null && s.major.ToUpper().Contains(search)

                               select s).ToList(); 
            }
                

            return View(satstudents.ToList());
        }

        //
        // GET: /Student/Details/5

        public ViewResult Details(int id)
        {
            SATStudent satstudent = db.SATStudents.Single(s => s.studentId == id);
            return View(satstudent);
        }

        //
        // GET: /Student/Create

        public ActionResult Create()
        {
            ViewBag.statusId = new SelectList(db.SATStudentStatuses, "SSID", "SSName");
            return View();
        } 

        //
        // POST: /Student/Create

        [HttpPost]
        public ActionResult Create(SATStudent satstudent,
            HttpPostedFileBase pictureUrl)
            //must match the name value of the input
        {
            if (ModelState.IsValid)
            {
                //declare and assign my variable for the imagename
                //to the default image for no product
                string imageName = "noImage.jpg";

                //code for file upload
                //check for image
                if (pictureUrl != null)
                {
                    //get the filename
                    imageName = pictureUrl.FileName;

                    //get the ext
                    string ext = 
                        imageName.Substring(imageName.LastIndexOf('.'));
                    
                    //create new filename and add the ext
                    imageName = Guid.NewGuid() + ext;

                    //save image to webserver
                    pictureUrl.SaveAs(Server.MapPath(
                        "~/Content/images/StudentImages/" + imageName));

                 }//end new file stuff
                //save to the database
                satstudent.pictureUrl = imageName;


                    db.SATStudents.AddObject(satstudent);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.statusId = new SelectList(db.SATStudentStatuses, "SSID", "SSName", satstudent.statusId);
                
                return View(satstudent);
            }
        
        
        //
        // GET: /Student/Edit/5
 
        public ActionResult Edit(int id)
        {
            SATStudent satstudent = db.SATStudents.Single(s => s.studentId == id);

            ViewBag.statusId = new SelectList(db.SATStudentStatuses, "SSID",
            "SSName", satstudent.statusId);
            return View(satstudent);
        }

        //
        // POST: /Student/Edit/5

        [HttpPost]
        public ActionResult Edit(SATStudent satstudent,
            HttpPostedFileBase pictureUrl)
            //must match the name value of the input
        {
            if (ModelState.IsValid)
            {
                //declare and assign my variable for the imagename
                //to the default image for no product
                string imageName = "noImage.jpg";

                //code for file upload
                //check for image
                if (satstudent.pictureUrl != null)
                {
                    //get the filename
                    imageName = pictureUrl.FileName;

                    //get the ext
                    string ext =
                        imageName.Substring(imageName.LastIndexOf('.'));

                    //create new filename and add the ext
                    imageName = Guid.NewGuid().ToString() + ext;

                    //save image to webserver
                    pictureUrl.SaveAs(Server.MapPath(
                        "~/Content/images/studentimages/" + imageName));

                    //end new file stuff
                    //save to the database
                    satstudent.pictureUrl = imageName;
                }

                db.SATStudents.AddObject(satstudent);
                db.ObjectStateManager.ChangeObjectState(satstudent, EntityState.Modified);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.statusId = new SelectList(db.SATStudentStatuses, "SSID", "SSName", satstudent.statusId);

            return View(satstudent);
        }

        //
        // GET: /Student/Delete/5
 
        public ActionResult Delete(int id)
        {
            SATStudent satstudent = db.SATStudents.Single(s => s.studentId == id);
            return View(satstudent);
        }

        //
        // POST: /Student/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {            
            SATStudent satstudent = db.SATStudents.Single(s => s.studentId == id);
            db.SATStudents.DeleteObject(satstudent);
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