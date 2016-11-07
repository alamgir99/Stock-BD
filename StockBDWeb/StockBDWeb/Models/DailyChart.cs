using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockBDWeb.Models
{
    public enum ChartDuration
    {
        OneMonth = 1,
        SixMonths = 2,
        OneYear = 3,
        ThreeYears = 4,
        AllYears = 5
    };

    public struct EODData
    {
        public DateTime Time; // time
        public decimal Open;   // open price
        public decimal High;   // high price
        public decimal Low;   // low  price
        public decimal Close;    // last trade price
        public long Volume; // trade volume
    };

    //Model of a daily chart
    public class DailyChart
    {

        DateTime StartDate { get; set; }
        DateTime EndDate { get; set; }
        StockBDContext m_dBContext;
        TickerList m_Tickers;

        //ticker drop down list items and selected value
        public SelectList TickerList { get; set; }
        public string selectTicker { get; set; }

        //duration drop down list items and selected value
        public SelectList DurationList { get; set; }
        public ChartDuration selectDuration { get; set; }

        // data itself
        public string[] PriceData;
        public string[] VolumeData;

        // serialized data
        public string sPriceData;
        public string sVolumeData;

        public DailyChart(StockBDContext con)
        {
            m_dBContext = con;
            
            m_Tickers = new TickerList(con);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");
            DurationList = new SelectList(Enum.GetValues(typeof(ChartDuration)));
            selectDuration = ChartDuration.OneMonth;
        }

        public DailyChart(StockBDContext con, string ticker) : this(con)
        {
           
            selectTicker = ticker;
            selectDuration = ChartDuration.OneMonth;

            EndDate = DateTime.Today;
            StartDate = DateTime.Today.AddDays(-30);
            m_Tickers = new TickerList(con, ticker);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");
        }

        public DailyChart(StockBDContext con, string ticker, ChartDuration duration) :  this(con)
        {
           
            m_Tickers = new TickerList(con, ticker);
            TickerList = new SelectList(m_Tickers.Tickers, "Value", "Text");

            selectTicker = ticker;
            selectDuration = duration;

            EndDate = DateTime.Today;

            switch (duration)
            {
                case ChartDuration.OneMonth:
                    StartDate = DateTime.Today.AddDays(-25);
                    break;
                case ChartDuration.SixMonths:
                    StartDate = DateTime.Today.AddDays(-150);
                    break;
                case ChartDuration.OneYear:
                    StartDate = DateTime.Today.AddDays(-300);
                    break;
                case ChartDuration.ThreeYears:
                    StartDate = DateTime.Today.AddDays(-900);
                    break;
                case ChartDuration.AllYears:
                    StartDate = EndDate; // use same start and end date for entire data
                    break;
            }
        }

        private long TickerToCid (string ticker)
        {
            long id=0;
             if(m_dBContext == null)
                    return 0;
             var query = from Comp in m_dBContext.Companies where Comp.Ticker == ticker select Comp;
            foreach (var c in query)
                id = c.cID;

            return id;
        }
        /*
        //data exposure
        public void GetData()
            {
                if(m_dBContext == null)
                    yield return new EODData {Time=DateTime.Now, Open=0.0M, High=0.0M, 
                                        Low=0.0M, Close=0.0M, Volume=0};

                var DailyDataTable = m_dBContext.DailyDatas;
               
                long cID = TickerToCid(selectTicker);
                if(cID == 0) 
                    yield return new EODData {Time=DateTime.Now, Open=0.0M, High=0.0M, 
                                        Low=0.0M, Close=0.0M, Volume=0};


                var query = from data in DailyDataTable where (data.cID == cID && data.Date >= StartDate && data.Date <= EndDate ) select data;
                foreach(var q in query) {

                    yield return new EODData {Time=q.Date, Open=q.Open, High=q.High, 
                                        Low=q.Low, Close=q.Close, Volume=q.Volume};
                }
            } */
        
        //data exposure
        public void GetData()
        {
            if (m_dBContext == null)
                return; 

            var DailyDataTable = m_dBContext.DailyDatas;
               
            long cID = TickerToCid(selectTicker);
            if(cID == 0) 
             return;


            var query = from data in DailyDataTable where (data.cID == cID && data.Date >= StartDate && data.Date <= EndDate ) select data;
            int dLen = query.Count();

            PriceData = new string[dLen];
            VolumeData = new string[dLen];

            int i=0;
            foreach(var q in query) {
                PriceData[i] = q.Date.Subtract(new DateTime(1970, 1,1)).TotalMilliseconds + "," + q.Open.ToString() + "," + q.High.ToString() + "," +
                            q.Low.ToString() + "," + q.Close.ToString();
                VolumeData[i] = q.Date.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds + "," + q.Volume.ToString();
                i++;
            }

            //serialized
            sPriceData = MySerialize(PriceData);
            sVolumeData = MySerialize(VolumeData);
        }

        private string MySerialize(string[] strs)
        {
            int count = strs.Count();
            if (count == 0) return "[]";
            if (count == 1) return "["+strs[0]+"]";

            string sString = "[" + strs[0] + "]";
            for (int i = 1; i < count; i++)
            {
                sString = sString + "," + "[" + strs[i] + "]";
            }

            return "[" + sString + "]";
        }
    }
}