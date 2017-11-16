using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TestEngine.DataBase;

namespace TestEngine.Controllers
{
    public class customexamsController : BaseController
    {
        private Demo_sukhvirEntities db = new Demo_sukhvirEntities();

        // GET: customexams
        public ActionResult Index()
        {
            return View(db.customexams.ToList());
        }

        // GET: customexams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customexam customexam = db.customexams.Find(id);
            if (customexam == null)
            {
                return HttpNotFound();
            }
            return View(customexam);
        }

        // GET: customexams/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: customexams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "examid,examname,catid,syllabus,prerequisites,partner,price,ibillproductid,psproductid,comments,logo,duration,questioncount,offline,randomtest,pagename,usepage,printname,resultpage,transcriptpage,showcorporate,courseware,coursepath,productId2CO,priority,positioning,directions,haspassage,passagenum,positive,odeskexam,recertification,isbeta,uploaddate,passingmarks,examlanguage,masterexamid,refid,refid1,foreignexamid,elmlxmlname,isfn,examenablepartners,commoncodelive,isCronbach,JobTitleID,masterXmlfile,SurCatPageUrl,addedby,isfreezed,isfrree,addedbytest,flagged,flagerror,flagged1,showendbtn,idset,isidset,languagetype,languagetype1")] customexam customexam)
        {
            if (ModelState.IsValid)
            {
                db.customexams.Add(customexam);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(customexam);
        }

        // GET: customexams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customexam customexam = db.customexams.Find(id);
            if (customexam == null)
            {
                return HttpNotFound();
            }
            return View(customexam);
        }

        // POST: customexams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "examid,examname,catid,syllabus,prerequisites,partner,price,ibillproductid,psproductid,comments,logo,duration,questioncount,offline,randomtest,pagename,usepage,printname,resultpage,transcriptpage,showcorporate,courseware,coursepath,productId2CO,priority,positioning,directions,haspassage,passagenum,positive,odeskexam,recertification,isbeta,uploaddate,passingmarks,examlanguage,masterexamid,refid,refid1,foreignexamid,elmlxmlname,isfn,examenablepartners,commoncodelive,isCronbach,JobTitleID,masterXmlfile,SurCatPageUrl,addedby,isfreezed,isfrree,addedbytest,flagged,flagerror,flagged1,showendbtn,idset,isidset,languagetype,languagetype1")] customexam customexam)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customexam).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(customexam);
        }

        // GET: customexams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            customexam customexam = db.customexams.Find(id);
            if (customexam == null)
            {
                return HttpNotFound();
            }
            return View(customexam);
        }

        // POST: customexams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            customexam customexam = db.customexams.Find(id);
            db.customexams.Remove(customexam);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
