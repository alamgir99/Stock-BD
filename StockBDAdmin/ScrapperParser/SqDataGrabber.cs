/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : SqDataGrabber.cs
 * Purpose: Grabs data from Square Securities website.
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
    public class SqDataGrabber : DataGrabber
    {
        //link from Square Securities site
        const string dUrl = @"http://ssml.com.bd/Pages/Latest_Price_By_TadingCode.aspx";
        const string rUrl = @"http://ssml.com.bd/";
        //selected columns of the Table that have the data we need
        // ticker, Open=Open, High=High,Low=Low,Close=LTP, Vol=Vol
        private int[] sColumns = new int[] { 0, 6, 4, 5, 1, 10};
        // data grabber identifier
        public const string srcID = @"Square";
        
        //constructor
        public SqDataGrabber(string dFolder, string tFolder, EventHandler handler)
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
            //for Square, this is the marque table, so get the next one
            tabBeginAt = snapData.IndexOf("<TABLE", tabBeginAt + "<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase); 
            //now find the end of the table
            int tabEndAt = snapData.IndexOf("</TABLE", tabBeginAt+"<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase)+"</TABLE".Length;

            //chop out the table ingoring noise
            string strTable = snapData.Substring(tabBeginAt, tabEndAt - tabBeginAt + 1);

            //process the html table data and return
            return DataTable.ProcessHTMLTable(strTable, selColumns);
        }
    } // SqDataGrabber
} //ScrapperParser
