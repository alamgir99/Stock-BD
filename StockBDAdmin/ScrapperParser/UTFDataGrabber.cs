/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : UTFDataGrabber.cs
 * Purpose: Grabs data from United Financial website.
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
    // from United Financial web site
    public class UTFDataGrabber : DataGrabber
    {
        //link from Square Securities site
        const string dUrl = @"http://www.uftcl.com/MDSData/Display/ByTradeCode";
        const string rUrl = @"http://www.uftcl.com";
        //selected columns of the Table that have the data we need
        //ticker, Open=YCP, High=High, Low=Low, Close=Close, Vol=Vol
        private int[] sColumns = new int[] { 1, 6, 3, 4, 2, 10 };
        // data grabber identifier
        public const string srcID = @"United";
        
        //constructor
        public UTFDataGrabber(string dFolder, string tFolder, EventHandler handler)
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
            int tabEndAt = snapData.IndexOf("</TABLE", tabBeginAt+"<TABLE".Length, System.StringComparison.CurrentCultureIgnoreCase)+"</TABLE".Length;

            //chop out the table ingoring noise
            string strTable = snapData.Substring(tabBeginAt, tabEndAt - tabBeginAt + 1);

            //process the html table data and return
            return DataTable.ProcessHTMLTable(strTable, selColumns);
        }
    } // UTFDataGrabber
} //ScrapperParser

