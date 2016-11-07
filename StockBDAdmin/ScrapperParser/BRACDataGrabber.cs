/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : BRACDataGrabber.cs
 * Purpose: Grabs data from BRAC EPL website.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.IO;

namespace ScrapperParser
{
    // this class implements the data grabbing functions
    // from BRAC EPL web site
    public class BRACDataGrabber : DataGrabber
    {
        //link from DSE site
        const string dUrl = @"http://bracepl.com/brokerage/index.php/home/cstock_price";
        const string rUrl = @"http://bracepl.com/";
        //selected columns of the Table that have the data we need
        //Ticker, Open=YCP, High=High, Low=Low, Close=Close, Vol=Vol
        private int[] sColumns = new int[] { 1, 6, 3, 4, 2, 9};
        // data grabber identifier
        public const string srcID = @"BRAC";
        
        //constructor
        public BRACDataGrabber(string dFolder, string tFolder, EventHandler handler)
        {
            //pass the values to the base class
            urlString = dUrl;
            refString = rUrl;
            selColumns = sColumns;
            destFolder = dFolder;
            tempFolder = tFolder;
            fileString = "snapshot";
            OnCompleteDataSnap += handler;
        }

        //implements the interface methods
        public override string GetDataSourceID()
        {
            return srcID;
        }

        //process downloaded data and make a snapshot table
        //for further processing
        public override List<MinuteData> ProcessSnapshot(int sId, string snapData)
        {
            //snapData = snapData.ToLower(); // convert to upper case for convenience
            int tabBeginAt = snapData.IndexOf("<TABLE",System.StringComparison.CurrentCultureIgnoreCase);
            //for BRAC, there are tables within table. 3rd table is the one we need
            tabBeginAt = snapData.IndexOf("<TABLE", tabBeginAt + "<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase);
            tabBeginAt = snapData.IndexOf("<TABLE", tabBeginAt + "<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase);
        
            int tabEndAt = snapData.IndexOf("</TABLE", tabBeginAt+"<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase)+"</TABLE".Length;

            //chop out the table ingoring noise
            string strTable = snapData.Substring(tabBeginAt, tabEndAt - tabBeginAt + 1);

            //process the html table data and return
            List<MinuteData> retTable = DataTable.ProcessHTMLTable(strTable, selColumns);
            //BRAC has Debenchure and Treasury bond tickers. Remove them and return the resulting table
            List<MinuteData> newTable = new List<MinuteData>();
            for (int i = 0; i < retTable.Count; i++)
            {
                if (retTable[i].Ticker.StartsWith("DEB", StringComparison.InvariantCulture)) continue;
                if (retTable[i].Ticker.StartsWith("T05Y", StringComparison.InvariantCulture)) continue;
     
                newTable.Add(retTable[i]);
            }
            return newTable;
        }
    } // BRACDataGrabber
} //ScrapperParser
