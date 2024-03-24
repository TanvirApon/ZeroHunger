using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Resturant
        //-----------------------------------  Request  -----------------------------------------------
        public ActionResult MyRequestList()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            var list = db.Requests.Where(x => x.restaurant_id == (int)Session["userID"]);
            return View(db.Requests.ToList());
        }

        [HttpGet]
        public ActionResult AddRequestForm()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            return View();
        }

        [HttpPost]
        public ActionResult AddRequest()
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();

            Request request = new Request()
            {
                request_time = DateTime.Now.ToString(),
                status = "Pending",
                restaurant_id = (int)Session["userID"]
            };
            db.Requests.Add(request);
            db.SaveChanges();

            var checkRequest = db.Requests.FirstOrDefault(x => x.request_time == request.request_time);

            if (checkRequest != null)
            {
                return RedirectToAction("ItemsInRequest", new { id = checkRequest.id });
            }

            return View();
        }
        public ActionResult ItemsInRequest(int id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            var items = db.Items.Where(x => x.request_id == id).ToList();
            ViewBag.reqID = id;
            return View(items);
        }

        [HttpGet]
        public ActionResult AddItem(int id)
        {
            if (Session["userID"] == null)
            {
                return RedirectToAction("LogIn", "Home");
            }
            var db = new HungerEntities2();
            ViewBag.id = id;
            return View();
        }
        [HttpPost]
        public ActionResult AddItem(ItemDTO tempItem)
        {
            var db = new HungerEntities2();
            Item item = new Item()
            {
                name = tempItem.name,
                expiredate = tempItem.expiredate.ToString(),
                quantity = tempItem.quantity,
                request_id = tempItem.request_id,
            };
            db.Items.Add(item);
            db.SaveChanges();

            return RedirectToAction("ItemsInRequest", new { id = item.request_id });
        }
        public ActionResult DeleteItem(int id)
        {
            var db = new HungerEntities2();
            var item = db.Items.Find(id);
            int reqID = (int)item.request_id;
            db.Items.Remove(item);
            db.SaveChanges();
            return RedirectToAction("ItemsInRequest", new { id = reqID });
        }
        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("LogIn", "Home");
        }
    }
}