using StockBDWeb.Others;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockBDWeb.Models
{
    public class MinuteChart
    {
        StockBDContext m_dBContext;
        TickerList m_Tickers;

        //ticker drop down list items and selected value
        public SelectList TickerList { get; set; }
        public string selectTicker { get; set; }
        public DateTime selectDate { get; set; }
        public bool noDataFound { get; set; }
        public string XData { get; set; }
        public string YDataPrice { get; set; }
        public string YDataVolume { get; set; }
        
        //liveSession etc
        public ApplicationSettings m_settings;
        string m_dataFolder;

        public MinuteChart(StockBDContext con)
        {
            m_dBContext = con;
            m_Tickers = new TickerList(con);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");
            selectDate = DateTime.Today;

            /*
            DateTime dt = DateTime.Now;
            XData = "1,2,3,4,5,6,7,8,9,10";
            YDataPrice = "10,11,2,32,3,22,12,12,21,12";
            YDataVolume = "10,20,30,20,40,20,10,20,30,40";
            //noDataFound = true;
            */
            m_settings = new ApplicationSettings();
            m_dataFolder = m_settings.settings["dataFolder"];
        }

        public MinuteChart(StockBDContext con, string ticker)
            : this(con)
        {
            selectTicker = ticker;
            m_Tickers = new TickerList(con, ticker);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");
        }

        public MinuteChart(StockBDContext con, string ticker, DateTime selDate)
            : this(con)
        {
            selectTicker = ticker;
            m_Tickers = new TickerList(con, ticker);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");
            selectDate = selDate;
        }

        public void GetData()
        {
            if (m_settings.settings["liveSession"] == "true" && selectDate == DateTime.Today)
            { // got provide live data
            }
            else
            {
                string minPath = m_dataFolder + @"min\" + selectDate.ToString("yyyy") + @"\" + selectDate.ToString("yyyy-MM-dd") + @"\" + selectTicker+".txt";
                if (File.Exists(minPath))
                {
                    string[] lines = File.ReadAllLines(minPath);

                    foreach (string line in lines)
                    {
                        string[] parts = line.Split(new char[] { ',' });
                        DateTime dt = Convert.ToDateTime(parts[0]);
                        DateTimeOffset dto = new DateTimeOffset(dt);
                        XData = XData + "," + dto.ToUnixTimeSeconds().ToString();
                        YDataPrice = YDataPrice + "," + parts[1];
                        YDataVolume = YDataVolume + "," + parts[2];
                    }

                    //remove starting ,
                    XData = XData.Substring(1);
                    YDataPrice = YDataPrice.Substring(1);
                    YDataVolume = YDataVolume.Substring(1);

                }
                else
                {
                    XData = ""; YDataVolume = ""; YDataPrice = "";
                    noDataFound = true;
                }
            }
        }

        public static string GetLiveData(string ticker, long lastx)
        {
            //ticker -> the symbol we need to provide data for
            //lastx  -> the timestap the chart has already data for
            //we will only provide data that are newer than lastx
            
            string today = DateTime.Now.ToString("yyyy") + @"\" + DateTime.Now.ToString("yyyy-MM-dd") + @"\";
            string fPath = @"c:\www\stockbd\data\min\" + today + ticker + @".txt";
            string[] lines = System.IO.File.ReadAllLines(fPath);
            string priceData="";
            string volData ="";
            long alastx = 0;

            if (lines.Count() == 0)
                return @"Warning: data delay, will resume automatically";

            foreach(string line in lines)
            {
                string[] parts = line.Split(new[] { ',' });
                //pase time stamp into unix format
                DateTime dt = Convert.ToDateTime(parts[0]);
                DateTimeOffset dto = new DateTimeOffset(dt);
                alastx = dto.ToUnixTimeSeconds();
 
                if (alastx <= lastx)
                    continue; //skip the data the chart already has

                priceData = priceData + ",[" + alastx + "," + parts[1] + "]";
                volData = volData + ",[" + alastx + "," + parts[2] + "]";
            }
            priceData = "["+priceData.Substring(1)+"]"; // skip first comma
            volData = "["+volData.Substring(1)+"]";

            return volData + ";" + priceData;
        }
    }
}