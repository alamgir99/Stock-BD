using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockBDWeb.Models
{
    public class TickerList
    {
        private StockBDContext dbContext;
        public List<SelectListItem> Tickers { get; set; }

        //use the db context and populate list of tickers
        public TickerList(StockBDContext con, string selTicker="")
        {
            dbContext = con;
            Tickers = new List<SelectListItem>();

            var query = from comp in dbContext.Recents orderby comp.Ticker select comp;

            foreach (var q in query)
            {
                if(selTicker == q.Ticker)
                    Tickers.Add(new SelectListItem {Text = q.Ticker, Value = q.Ticker, Selected = true });
                else
                    Tickers.Add(new SelectListItem { Text = q.Ticker, Value = q.Ticker});

            }
        }


    }
}