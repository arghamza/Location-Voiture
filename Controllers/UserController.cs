using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CarRentinglatest.Models;

namespace CarRentinglatest.Controllers
{
    public class UserController : Controller
    {
        private CarsDBEntities db = new CarsDBEntities();

        // GET: User
        public string value = "";

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(User e)
        {
            if (Request.HttpMethod == "POST")
            {
                User er = new User();
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-QBVH575\\SQLEXPRESS;Initial Catalog=CarsDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                {
                    using (SqlCommand cmd = new SqlCommand("SP_EnrollDetail", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FirstName", e.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", e.LastName);
                        cmd.Parameters.AddWithValue("@Password", e.Password);
                        cmd.Parameters.AddWithValue("@Gender", e.Gender);
                        cmd.Parameters.AddWithValue("@Email", e.Email);
                        cmd.Parameters.AddWithValue("@status", "INSERT");
                        con.Open();
                        
                        if (e.FirstName != null && e.LastName != null && e.Password != null && e.Gender != null && e.Email != null)
                        {
                            ViewData["result"] = cmd.ExecuteNonQuery();
                            con.Close();
                        }
                        else
                        {
                            con.Close();
                            TempData["alertMessage"] = "Un ou plusieurs champs sont vides";
                        }
                    }
                }
            }
            return View();
        }
    }
}
