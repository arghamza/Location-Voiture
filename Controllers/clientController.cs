using CarRentinglatest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CarRentinglatest.Controllers
{
    public class clientController : Controller
    {
        // GET: client
        private CarsDBEntities db = new CarsDBEntities();
       
        public ActionResult paiement(string id)
        {
            //paiement should take string id but for testing we'll give 125-45-4
            
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
        [HttpPost]
        public ActionResult paiement(string id , DateTime date_deb , DateTime date_fin , string email )
        {
            if (date_deb > DateTime.Now && date_fin > date_deb)
            {
                location loc = new location();
                loc.date_debut = date_deb;
                loc.date_fin = date_fin;
              
                User us= db.User.Where(user => user.Email ==email ).First();
                loc.userid = us.ID;
                loc.voiture = id; 
                db.location.Add(loc);
              
                db.SaveChanges();
                return RedirectToAction("Acceuil");
            }

            return View(id); 
               
        }
        public ActionResult Acceuil()
        {
            

            return View(db.voiture.Take(21));
        }
        public ActionResult locations()
        {
            string email = Session["email"].ToString(); 
            User us = db.User.Where(user => user.Email == email).First();
           
           
            return View(db.location.Where(location => location.userid == us.ID).ToList());
        }
      public ActionResult profil(User user)
        {
            if(user!=null)
            {
                string email = Session["email"].ToString();
                User us = db.User.Where(user_ => user_.Email == email).First();
                return View(db.User.Find(us.ID));
            }
            else
            {
                return View(user); 
            }
        }
        [HttpPost]
        public ActionResult profil(int id ,string first_name , string last_name ,  string gender , string password)
        {

            User us = db.User.Find(id); 
            us.FirstName = first_name;
            us.LastName = last_name;
            
            us.Password = password;
            us.Gender = gender;
            TryUpdateModel(us);
            db.SaveChanges();

            return RedirectToAction("Acceuil", "Client");
        }
    }
}