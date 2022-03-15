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
    public class voituresController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();

        // GET: voitures
        public ActionResult Index()
        {
            var voiture = db.voiture.Include(v => v.categorie1).Include(v => v.modele1);
            return View(voiture.ToList());
        }

        // GET: voitures/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voiture voiture = db.voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // GET: voitures/Create
        public ActionResult Create()
        {
            ViewBag.categorie = new SelectList(db.categorie, "id", "title");
            ViewBag.modele = new SelectList(db.modele, "id", "marque");
            return View();
        }

        // POST: voitures/Create
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "immatriculation,modele,categorie,date_mec,carburant,prix_loc,image")] voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.voiture.Add(voiture);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.categorie = new SelectList(db.categorie, "id", "title", voiture.categorie);
            ViewBag.modele = new SelectList(db.modele, "id", "marque", voiture.modele);
            return View(voiture);
        }

        // GET: voitures/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voiture voiture = db.voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            ViewBag.categorie = new SelectList(db.categorie, "id", "title", voiture.categorie);
            ViewBag.modele = new SelectList(db.modele, "id", "marque", voiture.modele);
            return View(voiture);
        }

        // POST: voitures/Edit/5
        // Pour vous protéger des attaques par survalidation, activez les propriétés spécifiques auxquelles vous souhaitez vous lier. Pour 
        // plus de détails, consultez https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "immatriculation,modele,categorie,date_mec,carburant,prix_loc,image")] voiture voiture)
        {
            if (ModelState.IsValid)
            {
                db.Entry(voiture).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.categorie = new SelectList(db.categorie, "id", "title", voiture.categorie);
            ViewBag.modele = new SelectList(db.modele, "id", "marque", voiture.modele);
            return View(voiture);
        }

        // GET: voitures/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            voiture voiture = db.voiture.Find(id);
            if (voiture == null)
            {
                return HttpNotFound();
            }
            return View(voiture);
        }

        // POST: voitures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            voiture voiture = db.voiture.Find(id);
            db.voiture.Remove(voiture);
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
