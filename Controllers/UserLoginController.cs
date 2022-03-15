using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Web.Security;
using CarRentinglatest.Models;

namespace CarRentinglatest.Controllers
{
    public class UserLoginController : Controller
    {
        // GET: /UserLogin/  
        public string status;

        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User e)
        {
            //String SqlCon = ConfigurationManager.ConnectionStrings["CarsDBEntities"].ConnectionString;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-QBVH575\\SQLEXPRESS;Initial Catalog=CarsDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
            string SqlQuery = "select Email,Password from [User] where Email=@Email and Password=@Password";
            con.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, con); ;
            cmd.Parameters.AddWithValue("@Email", e.Email);
            cmd.Parameters.AddWithValue("@Password", e.Password);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["Email"] = e.Email.ToString();
                if (e.Email.ToString() == "admin@gmail.com") return RedirectToAction("Index", "Voitures"); 
                return RedirectToAction("Welcome");
            }
            else
            {
                ViewData["Message"] = "User Login Details Failed!!";
            }
            if (e.Email.ToString() != null)
            {
                Session["Email"] = e.Email.ToString();
                status = "1";
            }
            else
            {
                status = "3";
            }

            con.Close();
            return View();
            //return new JsonResult { Data = new { status = status } };  
        }

        [HttpGet]
        public ActionResult Welcome(User e)
        {
            User user = new User();
            DataSet ds = new DataSet();

            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-QBVH575\\SQLEXPRESS;Initial Catalog=CarsDB;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
            {
                using (SqlCommand cmd = new SqlCommand("sp_GetEnrollmentDetails", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Email", SqlDbType.VarChar, 30).Value = Session["Email"].ToString();
                    con.Open();
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    sda.Fill(ds);
                    List<User> userlist = new List<User>();
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        User uobj = new User();
                        uobj.ID = Convert.ToInt32(ds.Tables[0].Rows[i]["ID"].ToString());
                        uobj.FirstName = ds.Tables[0].Rows[i]["FirstName"].ToString();
                        uobj.LastName = ds.Tables[0].Rows[i]["LastName"].ToString();
                        uobj.Password = ds.Tables[0].Rows[i]["Password"].ToString();
                        uobj.Email = ds.Tables[0].Rows[i]["Email"].ToString();
                        uobj.Gender = ds.Tables[0].Rows[i]["Gender"].ToString();

                        userlist.Add(uobj);
                    }
                   
                }
                con.Close();

            }
            return RedirectToAction("Acceuil","Client");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session["Email"] = null; 

            return RedirectToAction("Acceuil", "Client");
        }

    }
}