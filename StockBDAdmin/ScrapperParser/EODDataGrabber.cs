/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : EODDataGrabber.cs
 * Purpose: Grabs End-of-day data from DSE website.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.IO;
using System.Text.RegularExpressions;
using System.Data.SQLite;
using EODData = ScrapperParser.MinuteData;

namespace ScrapperParser
{
    //this class repesents day-end data grabbing
    public class EODDataGrabber
    {
        //EOD data are available only from DSE website
        //DSE has multiple hosts to look for the data
        public string[] dseLink = new string[] {@"http://www.dse.com.bd/mst.txt",
                                                @"http://www.dsebd.org/mst.txt",
                                                @"http://admin.dsebd.org/admin-real/mst.txt"};

        public bool mstGood { get; set; }
        public string destFolder { get; set; }
        public int linkTime { get; set; }

        string dateToday;
        string yearToday;

        Dictionary<string, EODData> dataTable;

        public EODDataGrabber(string dFolder)
        {
            destFolder = dFolder;
            mstGood = false;
            DateTime dt = DateTime.Now;
            dateToday = dt.ToString("yyyy-MM-dd");
            yearToday = dt.Year.ToString();
            dataTable = new Dictionary<string, EODData>();
        }

        //if a srcFile is given, MST will be process from that file
        //otherwise MST file will be downloaded from dse site
        public void GrabAndSave(string srcPathAndFile = null)
        {
            if(srcPathAndFile == null) // no srcFile is given, so download
                GrabMST();
            //process the MST file
            ProcessMST(srcPathAndFile);
            //save the processed data
            Save();
        }

        //grab the MST file from DSE site
        private void GrabMST()
        {
            //stop watch to measure the time to download
            Stopwatch aWatch = new Stopwatch();

            var content = new MemoryStream();
            int totTry = 3;
            while (totTry > 0)
            {
                //start the stop watch 
                aWatch.Start();

                WebRequest webReq = WebRequest.Create(dseLink[totTry-1]);
                using (WebResponse response = webReq.GetResponse())
                {
                    // Get the data stream that is associated with the specified URL.
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        // Read the bytes in responseStream and copy them to content.  
                        responseStream.CopyTo(content);
                    }
                }
                //get the downloaded string
                var strmReader = new StreamReader(content);
                content.Position = 0;
                string rxBuffer = strmReader.ReadToEnd();
                if (isMSTGood(rxBuffer) == true) // MST file is valid and good
                { //save the file and return
                    mstGood = true;
     
                    StringBuilder sbm = new StringBuilder(destFolder +@"mst\"+ yearToday);
                    //if the folder does not exist, create
                    if (Directory.Exists(sbm.ToString()) == false)
                        Directory.CreateDirectory(sbm.ToString());

                    string filePathAndName = destFolder +@"mst\"+ yearToday + @"\"+ dateToday + "-mst.txt";
                    //if the file exists, try delete
                    if (File.Exists(filePathAndName))
                        File.Delete(filePathAndName);
                    using (FileStream fs = new FileStream(filePathAndName, FileMode.CreateNew))
                    {
                        content.WriteTo(fs);
                    }
                    aWatch.Stop();  //stop the watch and measure time
                    linkTime = (int)(aWatch.ElapsedMilliseconds / 1000);
                    return;
                }
                else //try again
                    totTry--;

            } // while totTry 
        }// GrabMST

        //takes a buffer full of mst file and checks if it is a valid one
        //by checking fi
        private bool isMSTGood(string buffer)
        {
            int datePos = buffer.IndexOf(@"TODAY'S SHARE MARKET :", System.StringComparison.CurrentCultureIgnoreCase);
            if (datePos == -1) // not found
                return false;
            string dateSignature = buffer.Substring(datePos + @"TODAY'S SHARE MARKET :".Length, 12).Trim();

            if (dateSignature == dateToday) // date matches
                return true;

            return false;
        }

        //process an MST file
        private void ProcessMST(string srcPathAndFile)
        {
            string rxBuffer;
            if (srcPathAndFile != null) // a path+file name is given
                //read the MST file
                rxBuffer = File.ReadAllText(srcPathAndFile);
            else
                rxBuffer = File.ReadAllText(destFolder + @"mst\" + yearToday + @"\" + dateToday + @"-mst.txt");

            int blockStart = 0;
            int blockEnd = 0;
            bool oddBlock = false;
            while (blockStart != -1)
            {
                blockStart = rxBuffer.IndexOf(@"Instr Code", blockEnd);
                if (blockStart != -1) // block found
                {
                    //lets see if we have the text Max Price within this range
                    //if so, it must be an odd lot block
                    string testBlock = rxBuffer.Substring(blockStart, 50);
                    if (testBlock.IndexOf(@"Max Price") == -1)
                        oddBlock = false;
                    else
                        oddBlock = true;
                }

                if (blockStart == -1) // we are at the end
                    break;

                //get the end of block
                blockEnd = rxBuffer.IndexOf(@"------", blockStart);
                if (blockEnd == -1) // we are at the end
                    break;

                string aBlock = rxBuffer.Substring(blockStart, blockEnd - blockStart - 1);
                ProcessBlock(aBlock, oddBlock);
            } // while true
        } // ProcessMST

        //process a block of data
        private void ProcessBlock(string block, bool oddFlag)
        {
            String lineMarker = @"(\n|\r|\r\n)";
            String dataMarker = @"\s+";

            if (oddFlag == false)
            {
                string[] lines = Regex.Split(block, lineMarker, RegexOptions.Multiline);
                bool firstLine = true;
                foreach (var line in lines )
                {
                    if (firstLine) { firstLine = false; continue; }
                    if (line.Trim() == "") continue;

                    string[] vals = Regex.Split(line, dataMarker);

                    EODData aData = new EODData();
                    aData.Ticker = vals[0].Trim();
                    if (aData.Ticker == "") continue;
                    aData.Open = vals[1].Trim();
                    aData.High = vals[2].Trim();
                    aData.Low = vals[3].Trim();
                    aData.Close = vals[4].Trim();
                    aData.Volume = vals[7].Trim();

                    dataTable.Add(aData.Ticker, aData);
                } // for each match
            }
            else // an oddlot block
            {
                string[] lines = Regex.Split(block, lineMarker, RegexOptions.Multiline);
                bool firstLine = true;
                foreach (var line in lines )
                {

                    if (firstLine) { firstLine = false; continue; }
                    if (line.Trim() == "") continue;

                    string[] vals = Regex.Split(line, dataMarker);


                    string ticker = vals[0].Trim();
                    if (ticker == "") continue;
                    string tickVol = vals[4];
                    if (dataTable.ContainsKey(ticker))
                    {
                        EODData aData;
                        //aData = dataTable[ticker]; 
                        if(dataTable.TryGetValue(ticker, out aData))                  
                        {
                            aData.Volume = (Int32.Parse(aData.Volume) + Int32.Parse(tickVol)).ToString();
                            dataTable[ticker] = aData;
                        }
                    }
                } // for each match
            }
        }// ProcessBlock

        //save the dataTable to a disk file
        private void Save()
        {
            //save the data in text file
            string[] allRows = new string[dataTable.Count];
            int i = 0;
            foreach (KeyValuePair<string, EODData> row in dataTable)
                allRows[i++] = row.Key +"," + row.Value.Open +","+ row.Value.High + ","+
                                row.Value.Low + "," + row.Value.Close +","+ row.Value.Volume;

            Array.Sort(allRows, StringComparer.InvariantCultureIgnoreCase);

            StringBuilder sbm = new StringBuilder(destFolder +  @"csv\" + yearToday);
            //if the folder does not exist, create
            if (Directory.Exists(sbm.ToString()) == false)
                Directory.CreateDirectory(sbm.ToString());

            File.WriteAllLines(destFolder + @"csv\" + yearToday +@"\"+ dateToday + ".csv", allRows);                                                                                                                                                                                                              

        }

        //store to DB
        public void StoreToDB(string dbString)
        {
            SQLiteConnection dbConnection = new SQLiteConnection(dbString);
            dbConnection.Open();

            using(SQLiteCommand command = dbConnection.CreateCommand())
            {
                foreach (KeyValuePair<string, EODData> row in dataTable)
                {
                    string values = row.Value.Open + "," + row.Value.High + "," +
                                row.Value.Low + "," + row.Value.Close + "," + row.Value.Volume;

                    string query = @"INSERT INTO DailyData(cID, Date, Open, High, Low, Close, Volume) VALUES(" +
                                "(SELECT cID FROM Company WHERE Ticker='" + row.Key + "')," + "'" + dateToday.ToString() + "'," +
                                values + ")";

                    command.CommandText = query;
                    command.ExecuteNonQuery();

                    //update Recent Table
                    query = @"UPDATE Recent SET YCP = " + row.Value.Close + " WHERE Ticker = '"+row.Key+"';";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }

            }
            dbConnection.Close();
        }
    }
}
