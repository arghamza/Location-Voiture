using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarRentinglatest.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult Voitures()
        {
            return RedirectToAction("Index", "voitures");
        }

        public ActionResult Categories()
        {
            return RedirectToAction("Index", "categories");
        }

        public ActionResult Modeles()
        {
            return RedirectToAction("Index", "modeles");
        }

        public ActionResult Locations()
        {
            return RedirectToAction("Index", "locations");
        }
    }
}
