using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoMainService;
using FluentValidation.Mvc;
using MVCFluentValidation.cs.Models;
using System.Configuration;

namespace MVCFluentValidation.cs.Controllers
{
    public class HomeController : Controller
    {
        private IBaiduMapService mapService;

        public HomeController(IBaiduMapService mapService)
        {
            this.mapService = mapService;
        }

        public ActionResult Index()
        {
            ViewBag.Message = ConfigurationManager.AppSettings["TestConfigSource"]; //"Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = mapService.GetAddressInfo("", "");
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Create([CustomizeValidatorAttribute(RuleSet = "Create")] MapInfo info)
        {
            if (ModelState.IsValid)
                return RedirectToAction("Index");

            var errors = ModelState.Values.ToList().FindAll(item => item.Errors.Count > 0);
            var errorStrList = new List<string>(errors.Count);

            foreach (var item in errors)
            {
                errorStrList.Add(item.Errors.First().ErrorMessage);
            }

            return Json(errorStrList);
            //return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
