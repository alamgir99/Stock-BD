using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StockBDWeb.Models;
using System.Web.Script.Serialization;

namespace StockBDWeb.Controllers
{
    public class DailyChartController : Controller
    {

        //
        // GET: /DailyChart/  
        public ActionResult Index()
        {
            StockBDContext dbC = new StockBDContext();
            DailyChart dChart = new DailyChart(dbC);

           // ViewBag.DailyChart = dChart;// = dChart.TickerList; 
            return View(dChart);
        }
        // GET: /DailyChart/Show
        public ActionResult Show(string selectTicker, ChartDuration selectDuration = ChartDuration.OneMonth)
        {
            StockBDContext dbC = new StockBDContext();
            DailyChart dChart = new DailyChart(dbC, selectTicker, selectDuration);
           // ViewBag.Dailychart = dChart;
            dChart.GetData();
            ViewBag.ChosenTicker = selectTicker;
            ViewBag.PriceData = dChart.sPriceData;
            ViewBag.VolumeData = dChart.sVolumeData;
            return View(dChart);
        }
    }
}
