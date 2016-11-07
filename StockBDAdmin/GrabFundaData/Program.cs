/**
 * Project: StockBD Admin
 * Module : GrabFundaData 
 * 
 * File   : GrabFundaData.cs
 * Purpose: Grabs fundamental data from dse web site
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapperParser;

namespace GrabFundaData
{
    //this is the tool for grabbing fundamental data
    public class Program
    {
        static void Main(string[] args)
        {
            string dbConString;
            if (args.Length == 1)
            {
                dbConString = args[0];
                FundaDataGrabber dseFunda = new FundaDataGrabber(dbConString, null);
                dseFunda.GrabAndSave();
            }
            else
            {
                Console.WriteLine("\nError: no dbConnectionString provided");
            }
        }
    }
}
