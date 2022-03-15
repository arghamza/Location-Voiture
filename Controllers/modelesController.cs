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
    public class modelesController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();

        // GET: modeles
        public ActionResult Index()
        {
            return View(db.modele.ToList());
        }

        // GET: modeles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modele modele = db.modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // GET: modeles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: modeles/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,marque,serie")] modele modele)
        {
            if (ModelState.IsValid)
            {
                db.modele.Add(modele);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(modele);
        }

        // GET: modeles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modele modele = db.modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // POST: modeles/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,marque,serie")] modele modele)
        {
            if (ModelState.IsValid)
            {
                db.Entry(modele).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(modele);
        }

        // GET: modeles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            modele modele = db.modele.Find(id);
            if (modele == null)
            {
                return HttpNotFound();
            }
            return View(modele);
        }

        // POST: modeles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            modele modele = db.modele.Find(id);
            db.modele.Remove(modele);
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
