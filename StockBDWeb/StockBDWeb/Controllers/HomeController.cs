using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockBDWeb.Models;

namespace StockBDWeb.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            //some cleanup
            //System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(@"c:\www\stockbd\tmp" + @"\dldata\");
           // directory.Empty();

            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
        //
        // GET: //Home/Contact
        [HttpGet]
        public ActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Contact(Contact info)
        {
            //if the provided info is all good, shoot the email
            if (info.isValid())
            {
                info.SendEmail();
                ViewBag.ContactDone = true;
                return View();
            }
            else
            {
                ViewBag.ContactError = true;
                return View();
            }
        }
    }
}
