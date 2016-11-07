/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : CompanyListGrabber.cs
 * Purpose: Grabs list of all companies from DSE website
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;
using System.IO;
using HtmlAgilityPack;

namespace ScrapperParser
{
    //grabs list of companies from DSE
    public class CompanyListGrabber : IDisposable
    {
        private string srcLink = @"http://dse.com.bd/company%20listing.php";
        private SQLiteConnection m_dbConnection;


        public CompanyListGrabber(string dbConString)
        {
            m_dbConnection = new SQLiteConnection(dbConString);
            m_dbConnection.Open();
        }

        public void GrabAndStore()
        {
            try
            {
                WebRequest webReq = WebRequest.Create(srcLink);
                using (WebResponse response = webReq.GetResponse())
                {
                    // Get the data stream that is associated with the specified URL.
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Read the bytes in responseStream and copy them to content.  
                        //get the downloaded string
                        var strmReader = new StreamReader(responseStream);
                        string rxBuffer = strmReader.ReadToEnd();

                        List<MinCompInfo> fData = ProcessCompanyList(rxBuffer);
                        StoreData(fData);
                    }
                }
            }
            catch (WebException we)
            {
                throw;// Console.Write(we.Status);

            }
        }

        //process a buffer full of html text and return company info
        private List<MinCompInfo> ProcessCompanyList(string rxBuffer)
        {
            int startAt, endAt;
            List<MinCompInfo> retTable = new List<MinCompInfo>();

            // Creates an HtmlDocument object from an URL
           HtmlDocument document = new HtmlDocument();

            startAt = rxBuffer.IndexOf(@"Company Listing by Alphabetic Order:", System.StringComparison.CurrentCultureIgnoreCase);
            startAt = rxBuffer.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
            startAt = rxBuffer.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
            //we pick the third table
            startAt = rxBuffer.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = rxBuffer.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string minInfo = rxBuffer.Substring(startAt, endAt - startAt + @"</table".Length + 1);
            while (minInfo.Length > 0)
            {                //load the given html string
                document.LoadHtml(minInfo);

                //get all the rows
                IEnumerable<HtmlNode> allRows = document.DocumentNode.SelectNodes("//tr");
                if (allRows == null) break; // we are done
                HtmlNodeCollection allTds;
                foreach (HtmlNode row in allRows)
                {
                    // Extracts all links within that node
                    allTds = row.SelectNodes("td");
                    string[] tickerName = allTds[0].InnerText.Trim().Split(new string[] { @" (" }, StringSplitOptions.RemoveEmptyEntries);
                   // if (tickerName.Count() == 1) continue; // igonore header row
                    string ticker = tickerName[0].Trim();
                    string name = tickerName[1].Substring(0, tickerName[1].Length - 1).Trim();
                    MinCompInfo aInfo = new MinCompInfo() { Ticker = ticker, Name = name };

                    retTable.Add(aInfo);
                }

                //one block of company is done, try next block
                //we pick the third table
                startAt = rxBuffer.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
                startAt = rxBuffer.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
                endAt = rxBuffer.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

                minInfo = rxBuffer.Substring(startAt, endAt - startAt + @"</table".Length + 1);
            }

            //DSE has Treasuy bonds and Debenchure tickers. Remove them and return the resulting table
            List<MinCompInfo> newTable = new List<MinCompInfo>();
            for (int i = 0; i < retTable.Count; i++)
            {
                if (retTable[i].Ticker.StartsWith("DEB", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T05Y", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T10Y", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T15Y", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T20Y", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T5Y", StringComparison.InvariantCulture)) continue;

                newTable.Add(retTable[i]);
            }
            return newTable;
        }

        //store the basic data into dB
        private void StoreData(List<MinCompInfo> data)
        {
            using (SQLiteCommand command = m_dbConnection.CreateCommand())
            {
                foreach (var d in data)
                {
                    //string query = @"INSERT OR REPLACE INTO Company (Ticker, Name) VALUES (" +
                    //             "'" + d.Ticker + "','" + d.Name + "')";
                    string query = @"INSERT INTO Company (Ticker, Name) VALUES (" +
                                "'" + d.Ticker + "','" + d.Name + "')";
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    //update Recent Table
                    query = @"INSERT INTO Recent (Ticker) VALUES (" +
                                "'" + d.Ticker + "')";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            m_dbConnection.Close();
        }
        
        //dispose things
        public void Dispose()
        {
            this.Dispose(true);
        } 
        protected virtual void Dispose(bool disposing)
        {

            m_dbConnection.Close();
            this.Dispose(disposing);
           
        }
    }
}
