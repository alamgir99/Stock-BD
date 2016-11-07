using StockBDWeb.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockBDWeb.Controllers
{
    public class DownloadController : Controller
    {
        //
        // GET: /Download/

        public ActionResult Index()
        {
            //show the download page
            return View();
        }


        [HttpPost]
        public ActionResult DoDownload()
        {
            // get the from date
            DateTime fromDt = Convert.ToDateTime(Request.Form["date_from"]);
            // get the to date
            DateTime toDt = Convert.ToDateTime(Request.Form["date_to"]);
            // get type of data
            DataFileType dT = (DataFileType)Enum.Parse(typeof(DataFileType), Request.Form["data_type"]);
            // create the Download model
            Download dn = new Download(fromDt, toDt, dT);

            //download  path
            string dnFile = dn.GetDownloadFilePath();

            //check if the download is a valid one
            if (dn.isOK == true)
            {
                //get the download file path content and return it to the browser
                return File(dnFile, "application/octet-stream", "data-" + Path.GetRandomFileName().Replace(".", "") + ".zip");
            }
            else
            {
                //throw new HttpException(404, "Invalid dates or data types.");//RedirectTo NoFoundPage
                ViewBag.NotFound = true;
                return View("Index");
            }   
        }

    }
}
