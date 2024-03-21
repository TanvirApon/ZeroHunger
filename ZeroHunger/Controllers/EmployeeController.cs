using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult MyRequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            int userID = (int)Session["userID"];
            var list = db.Requests.Where(x => x.employee_id == userID).ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult UpdateRequest(int id)
        {
            if (Session["userID"] == null) { return RedirectToAction("LogIn", "Home"); }

            var db = new HungerEntities2();
            var req = db.Requests.Find(id);
            req.collection_time = DateTime.Now.ToString("yyyy-MM-dd");
            req.status = "Collection Done";

            db.SaveChanges();

            return RedirectToAction("MyRequestList");
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }
}