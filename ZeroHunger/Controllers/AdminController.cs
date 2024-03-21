using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //--------------------------------  Employee Handelling ---------------------------------------
        public ActionResult RequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            return View(db.Requests.ToList());
        }
        [HttpGet]
        public ActionResult AssignEmployee(int id)
        {
            var db = new HungerEntities2();
            var req = db.Requests.Find(id);
            ViewBag.EmployeeID = db.Employees.ToList();
            return View(req);

            //return RedirectToAction("RequestList");
        }
        [HttpPost]
        public ActionResult AssignEmployee(Request request)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            var req = db.Requests.Find(request.id);
            req.employee_id = request.employee_id;
            req.status = "Picking Up";

            db.SaveChanges();
            return RedirectToAction("RequestList");
        }

        //------------------------------------  Log Out -----------------------------------------------
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }
}