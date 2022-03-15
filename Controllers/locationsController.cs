using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentinglatest.Models;

namespace CarRentinglatest.Controllers
{
    public class locationsController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();

        // GET: locations
        public ActionResult Index()
        {
            var location = db.location.Include(l => l.User).Include(l => l.voiture1);
            return View(location.ToList());
        }

        // GET: locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // GET: locations/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.User, "ID", "FirstName");
            ViewBag.voiture = new SelectList(db.voiture, "immatriculation", "carburant");
            return View();
        }

        // POST: locations/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userid,voiture,date_debut,date_fin")] location location)
        {
            if (ModelState.IsValid)
            {
                db.location.Add(location);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.User, "ID", "FirstName", location.userid);
            ViewBag.voiture = new SelectList(db.voiture, "immatriculation", "carburant", location.voiture);
            return View(location);
        }

        // GET: locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.User, "ID", "FirstName", location.userid);
            ViewBag.voiture = new SelectList(db.voiture, "immatriculation", "carburant", location.voiture);
            return View(location);
        }

        // POST: locations/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userid,voiture,date_debut,date_fin")] location location)
        {
            if (ModelState.IsValid)
            {
                db.Entry(location).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.User, "ID", "FirstName", location.userid);
            ViewBag.voiture = new SelectList(db.voiture, "immatriculation", "carburant", location.voiture);
            return View(location);
        }

        // GET: locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            location location = db.location.Find(id);
            if (location == null)
            {
                return HttpNotFound();
            }
            return View(location);
        }

        // POST: locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            location location = db.location.Find(id);
            db.location.Remove(location);
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
