using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockBDWeb.Models;
using System.IO;
using StockBDWeb.Others;

namespace StockBDWeb.Controllers
{
    public class MinuteChartController : Controller
    {
        //
        // GET: /MinuteChart/  
        public ActionResult Index()
        {
            StockBDContext dbC = new StockBDContext();
            MinuteChart minChart = new MinuteChart(dbC);

            return View(minChart);
        }
        // GET: /MinuteChart/Show
        public ActionResult Show(string selectTicker, DateTime selectDate)
        {
            StockBDContext dbC = new StockBDContext();
            MinuteChart minChart = new MinuteChart(dbC, selectTicker, selectDate);
            ViewBag.ChosenTicker = selectTicker;
            minChart.GetData();
            return View(minChart);
        }

        //AJAX data provider
        //make it static 
        public  string MinServer(string ticker, long lastx)
        {
            string result = MinuteChart.GetLiveData(ticker, lastx);

            //log the incoming requests for now
           // System.IO.File.AppendAllText(@"c:\www\stockbd\tmp\minreqlog.txt", lastx.ToString() + " : " + ticker + "\n");

            return result;
        }

    }
}
