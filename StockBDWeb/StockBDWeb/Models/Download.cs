using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
//using System.IO.Compression;
using System.Linq;
using System.Web;

namespace StockBDWeb.Models
{
    //types of file
    public enum DataFileType
    {
        CSV = 1,
        MST = 2,
        MIN = 3
    }
    //represents Download view
    public class Download
    {
        private const string dataFolder = @"C:\www\stockbd\data\";
        private const string tempFolder = @"C:\www\stockbd\tmp\dldata\"; 
        public DateTime fromDate { get; set; }
        public DateTime toDate { get; set; }
        public DataFileType dType { get; set; }
        public bool isOK { get; set; }

        public Download(DateTime from, DateTime to, DataFileType dT)
        {
            // if from date is later than earlier
            if (from > to)
            {
                fromDate = to; toDate = from;
            }
            else
            {
                fromDate = from;
                toDate = to;
            }
  
            dType = dT;
            isOK = true;
        }

        //prepares a list of from to to date
        private List<DateTime> DateRange(DateTime from, DateTime to)
        {
            List<DateTime> list = new List<DateTime>();
            if (from == to)
            {
                list.Add(from);
                return list;
            }

            DateTime dt = from;
            while (dt <= to)
            {
                list.Add(dt);
                dt = dt.AddDays(1);

            }

            return list;
        }

        //prepares a download
        public string GetDownloadFilePath()
        {
           
            List<DateTime> dList = DateRange(fromDate, toDate);
            if (dList.Count == 0)
            {
                isOK = false;
                return "";
            }
            string [] fList = new string[dList.Count];
            // build file list
            int i=0;
            foreach (var d in dList)
            {
                if(dType != DataFileType.MIN)// csv or mst files are stored as files
                    fList[i] = dataFolder+dType.ToString().ToLower() + "\\" + d.ToString("yyyy") + "\\" + d.ToString("yyyy-MM-dd") + "." + dType.ToString().ToLower();
                else // minute date file are sotred in folder
                    fList[i] = dataFolder+dType.ToString().ToLower() + "\\" + d.ToString("yyyy") + "\\" + d.ToString("yyyy-MM-dd") + "\\";
                i++;
            }

            //create the zip arcihve
            string zipFileName = tempFolder + Path.GetRandomFileName();
            zipFileName = zipFileName.Replace('.', 'x') + ".zip";

            if (dType != DataFileType.MIN) // csv or mst file
            {
                using (ZipFile zip = new ZipFile())
                {
                    foreach (var f in fList)
                    {
                        if (File.Exists(f))
                        {
                            zip.AddFile(f, @"sbdata");
                        }
                    }
                    zip.Comment  =@"Data downloaded from stock-bd.com";
                    zip.Save(zipFileName);
                    if (zip.Count == 0)
                        isOK = false;
                }
             }
             else // min data folder
             {
                using (ZipFile mzip = new ZipFile() )
                {
                    foreach (var f in fList)
                    {
                        if (Directory.Exists(f))
                        {
                            mzip.AddDirectory(f, @"sbdata\"+f.Substring(f.Length-11));
                        }
                    }
                    mzip.Comment = @"Data downloaded from stock-bd.com";
                    mzip.Save(zipFileName);
                    if (mzip.Count == 0)
                        isOK = false;
                }
             }
        
            //return the zip file
            return zipFileName;
        }
    }


}