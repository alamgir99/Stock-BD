/**
 * Project: StockBD Admin
 * Module : GrabCompanyList 
 * 
 * File   : GrabCompanyList.cs
 * Purpose: Grabs list of companies from dse web site
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapperParser;

namespace GrabCompanyList
{
    public class Program
    {
        //this tool grabs the list of companies and fills just the ticker name
        //in the Company table. A full FundaGrabber then can grab whole lot of data
        static void Main(string[] args)
        {
            string dbConString = @"Data Source=d:\4Devel\CSProjs\StockBDDB\StockBD.s3db;Version=3;";
           // CompanyListGrabber dse = new CompanyListGrabber(dbConString);
           // dse.GrabAndStore();
        }
    }
}
