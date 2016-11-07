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
using ScrapperParser;
using System.Threading;
using System.Net;

namespace TestScrapperParser
{

    //DSE and BRAC reliable!
    class Program
    {
        static void Main(string[] args)
        {
            string tempFolder = @"D:\4Devel\CSProjs\StockBD\temp\";
            string dataFolder = @"D:\4Devel\CSProjs\StockBD\mindata\";

            WebRequest.DefaultWebProxy = null;
       
            /*
           DSEDataGrabber dseData = new DSEDataGrabber(dataFolder, tempFolder, OnComplete);
           dseData.GrabAndSave(1);
          Thread.Sleep(1000 * 70);
           dseData.GrabAndSave(2);
           Thread.Sleep(1000 * 70);
           dseData.GrabAndSave(3);
           Thread.Sleep(1000 * 70);
           dseData.GrabAndSave(4);
           Thread.Sleep(1000 * 70);
            */
            /*
            SqDataGrabber sqData = new SqDataGrabber(dataFolder, tempFolder, OnComplete);
            sqData.GrabAndSave(1);
            Thread.Sleep(1000 * 70);
            sqData.GrabAndSave(2);
            Thread.Sleep(1000 * 70);
            sqData.GrabAndSave(3);
            Thread.Sleep(1000 * 70);
            sqData.GrabAndSave(4);
            Thread.Sleep(1000 * 70);
            */
           BRACDataGrabber brData = new BRACDataGrabber(dataFolder, tempFolder, OnComplete);
            brData.GrabAndSave(1, true);
            Thread.Sleep(1000 * 70);
            brData.GrabAndSave(2, true);
            Thread.Sleep(1000 * 70);
            brData.GrabAndSave(3, true);
            Thread.Sleep(1000 * 70);
            brData.GrabAndSave(4, true);
            Thread.Sleep(1000 * 70);

          //  UTFDataGrabber utData = new UTFDataGrabber(dataFolder, tempFolder, OnComplete);
          //  utData.GrabAndSave(1);

           Console.WriteLine("Program at end");
            Console.ReadKey();
        }

        //notify event handler
        public static void OnComplete(object sender, EventArgs e)
        {
            Console.Write("Download done, ");
            DataGrabber dse = (DataGrabber)sender;
            Console.WriteLine("time took : {0}", dse.LinkTime());

        }
    }
}
