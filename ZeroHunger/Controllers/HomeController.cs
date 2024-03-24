using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.DTOs;
using ZeroHunger.EF;

namespace ZeroHunger.Controllers
{
    public class HomeController : Controller
    {

        /*
         * Login page
         * With Login validation like email type check which is called from DTOs Login validation
        
          
         */

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginValidation user)
        {
            var db = new HungerEntities2();
            if (user.usertype == "Admin")
            {
                var dbAdmin = db.Admins.FirstOrDefault(x => x.email.Equals(user.email));
                if (dbAdmin != null && dbAdmin.password.Equals(user.password))
                {
                    Session["userID"] = dbAdmin.id;
                    return RedirectToAction("RequestList", "Admin");
                }
            }
            else if (user.usertype == "Restaurant")
            {
                var dbRestaurant = db.Restaurants.FirstOrDefault(x => x.email == user.email && x.password == user.password);
                if (dbRestaurant != null)
                {
                    Session["userID"] = dbRestaurant.id;
                    return RedirectToAction("MyRequestList", "Restaurant");
                }
            }
            else
            {
                var dbEmployee = db.Employees.FirstOrDefault(x => x.email.Equals(user.email));
                if (dbEmployee != null && dbEmployee.password.Equals(user.password))
                {
                    Session["userID"] = dbEmployee.id;
                    return RedirectToAction("MyRequestList", "Employee");
                }
            }
            return View(user);
        }

        //-------------------------------------- Employee Registration ----------------------------------
        [HttpGet]
        public ActionResult EmployeeRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EmployeeRegistration(EmployeeDTO tempEmployee)
        {
            var db = new HungerEntities2();
            #region
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EmployeeDTO, Employee>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<Employee>(tempEmployee);
            #endregion

            db.Employees.Add(data);
            db.SaveChanges();

            return RedirectToAction("Login");
        }

        //------------------------------------- Resturant Registration --------------------------------
        [HttpGet]
        public ActionResult ResturantRegistration()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResturantRegistration(ResturantDTO tempResturant)
        {
            var db = new HungerEntities2();
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ResturantDTO, Restaurant>();
            });
            var mapper = new Mapper(config);
            var data = mapper.Map<Restaurant>(tempResturant);

            db.Restaurants.Add(data);
            db.SaveChanges();
            return RedirectToAction("Login");
        }





    }
}
