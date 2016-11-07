/**
 * Project: StockBD Admin
 * Module : GrabEODData 
 * 
 * File   : GrabEODData.cs
 * Purpose: Grabs End of Day data from dse web site
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ScrapperParser;
using System.IO;

namespace GrabEODData
{
    public class Program
    {
        static int Main(string[] args)
        {
            string destFolder;
            if (args.Length == 0) // no destination folder is given
            {
                Console.WriteLine("\nWarning: Using current folder as destination folder.");
                destFolder = Directory.GetCurrentDirectory()+"\\";
            }
            else
            {
                destFolder = args[0];
            }

            EODDataGrabber dseEOD = new EODDataGrabber(destFolder);
            dseEOD.GrabAndSave();

            //if a dbString is given
     //       if (args.Length == 2)
                //dseEOD.StoreToDB(args[1]); // store the data to DB
                dseEOD.StoreToDB(@"Data Source=E:\4Devel\CSProjs\StockBDDB\StockBD.s3db;Version=3;");

                Console.WriteLine("Done");
            return 0;
        }
    }
}
