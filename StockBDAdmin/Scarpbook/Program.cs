/**
 * 
 * scrap/test module
 * 
 * 
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.IO;
using System.Threading;
using ScrapperParser;

namespace Scarpbook
{
    class Program
    {
        //static string dFolder = @"E:\4Devel\CSProjs\StockBD\";

        static void Main(string[] args)
        {
            FundaDataGrabber dtGrabber = new FundaDataGrabber(@"Data Source=D:\4Devel\CSProjs\StockBDDB\StockBD.s3db;Version=3;", null);

            dtGrabber.GrabAndSave();
           // Console.Write("Took {0} seconds.", dtGrabber.linkTime);
            Console.Write("End of program");
            Console.ReadKey();
        }
    }
}
