/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : TypesAndEnums.cs
 * Purpose: Contains type and constant definitions.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ScrapperParser
{

    //define all custom data types and enums

    public struct MinuteData
    {
        public string Ticker; // symbol name
        public string Open;   // open price
        public string High;   // high price
        public string Low;   // low  price
        public string Close;    // last trade price
        public string Volume; // trade volume
    };

    public struct SnapDelta
    {
        public string Ticker { get; set; } // ticker name
        public string LTP {get; set;} // last trade/close price
        public string Volume {get; set;} // volume 
    };

    public class EventProcessProgress : EventArgs
    {
        public float Progress { get; set; }
    }
    
    
    public static class Helper {
        //empty directory helper
        public static void Empty(this System.IO.DirectoryInfo directory)
        {
            foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
            foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }
    }
}
