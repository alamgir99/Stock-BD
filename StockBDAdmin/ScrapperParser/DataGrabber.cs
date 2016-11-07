/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : DataGrabber.cs
 * Purpose: Defines the basic class for a data grabber
 *          Source specific grabber should sub-class this.
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
using System.Windows;
using System.Data.SQLite;

namespace ScrapperParser
{
    //base class for any data grabber
    public abstract class DataGrabber : DataSourceInterface
    {
        //link from web site
        protected string urlString { set; get; }
        protected string refString { set; get; }
        //selected columns of the Table that have the data we need
        protected int[] selColumns { set; get; } 

        //total time taken over a session
        private int totLinkTime;
        protected int avgLinkTime; // average time for one snapshot

        //last and current snapshot array
        private List<MinuteData> lastSnapshot;
        private List<MinuteData> curSnapshot;
        // difference between two snaps
        private List<SnapDelta> deltaSnapshot;

        //file and folder settings
        public string destFolder { get; set; } //destination folder that we will save the minute data on
        public string fileString { get; set; } //and the file name template
        public string tempFolder { get; set; } //temp folder for temporary file storage

        //notify event handler
        public event EventHandler OnCompleteDataSnap;

        //constructor
        public DataGrabber(string dUrl, string rUrl, int[] sCol, string sId) : this()
        {
            urlString = dUrl;
            refString = rUrl;
            selColumns = sCol;
        }

        //basic no parameter constructor
        public DataGrabber()
        {
            lastSnapshot = new List<MinuteData>();
            curSnapshot = new List<MinuteData>();
            deltaSnapshot = new List<SnapDelta>();
            destFolder = "";
            fileString = "snapshot";
            totLinkTime = 0;
        }

        //this method measures the time it takes to grab
        //a snapshot of data from the source
        public int LinkTime()
        {
            return avgLinkTime;
        }

        //grabs one snapshot of data from the source and save
        public void GrabAndSave(int sId, bool saveTemp)
        {
            //sId is snapshot number

            Stopwatch aWatch = new Stopwatch();
            //start the stop watch 
            aWatch.Start();

            var content = new MemoryStream();
            WebRequest webReq = WebRequest.Create(urlString);

            using (WebResponse response = webReq.GetResponse())
            {
                // Get the data stream that is associated with the specified URL.
                using (Stream responseStream = response.GetResponseStream())
                {
                    // Read the bytes in responseStream and copy them to content.  
                    responseStream.CopyTo(content);
                }
            }
            //stop the watch and measure total and average time
            aWatch.Stop();

            if (saveTemp) // if we need to save the html file for debug
            {
                //debug begins 
                string tmpFname = tempFolder + "rxBuffer" + sId.ToString() + ".html";
                //if the file exists, try delete
                if (File.Exists(tmpFname))
                    File.Delete(tmpFname);
                using (FileStream fs = new FileStream(tmpFname, FileMode.CreateNew))
                {
                    content.WriteTo(fs);
                }
            }
         
            // debug ends
            totLinkTime += (int)(aWatch.ElapsedMilliseconds / 1000);
            avgLinkTime = (int)(totLinkTime / sId);

            //get the downloaded string
            var strmReader = new StreamReader(content);
            content.Position = 0;
            string rxBuffer = strmReader.ReadToEnd();


            // string rxBuffer = File.ReadAllText(@"E:\4Devel\CSProjs\StockBD\DSE"+sId.ToString()+".html");
            //string rxBuffer = File.ReadAllText(@"E:\4Devel\CSProjs\StockBD\SQUARE" + sId.ToString() + ".html");
            //string rxBuffer = File.ReadAllText(@"E:\4Devel\CSProjs\StockBD\BRACEPL" + sId.ToString() + ".html");
            //string rxBuffer = File.ReadAllText(@"E:\4Devel\CSProjs\StockBD\UFTCL" + sId.ToString() + ".html");
            
             //process the data
             List<MinuteData> tmpSnapshot = ProcessSnapshot(sId, rxBuffer);


             //discard last snapshot, current one is now last one
             //temporary one is current one
             lastSnapshot = curSnapshot;
             curSnapshot = tmpSnapshot;

             //calculate delta
             if(sId > 1)
                 CalculateDelta();
             //save the data on disk
                 SaveData(sId);
            //save current snapshot
                 if (saveTemp)
                     SaveTempSnapshot(sId);

             //call the event handler to notify the caller of completion
             OnCompleteDataSnap(this, EventArgs.Empty);
        } // GrabAndSave

        //get the derived class implement this
        public abstract List<MinuteData> ProcessSnapshot(int sId, string snapData);

        // returns a string identifying the source
        public abstract string GetDataSourceID(); 

        //return true if the newly downloaded snapshot
        //is a genuine new snapshot
        private bool IsNewSnapshot(List<MinuteData> newSnap, List<MinuteData> oldSnap)
        {
            //replace with a smarter one: todon
            int rowIndex = 0;
            foreach (var row in newSnap)
            {
                if(newSnap[rowIndex].Ticker == oldSnap[rowIndex].Ticker)
                    if(Int32.Parse(newSnap[rowIndex].Volume) > Int32.Parse(oldSnap[rowIndex].Volume))
                        return true;
                rowIndex++;
            }
            return false;
        }


        //save current snapshot
        public void SaveTempSnapshot(int sId)
        {
            //form the file name
            StringBuilder sbs = new StringBuilder(tempFolder + fileString + "-" + sId.ToString("d3") + ".csv");
            string fileName = sbs.ToString();
            //if the file exists, try delete
            if (File.Exists(fileName))
                File.Delete(fileName);
            //save the current snapshot
            using (FileStream fs = new FileStream(fileName, FileMode.CreateNew))
            {
                StreamWriter writer = new StreamWriter(fs);
                foreach (var row in curSnapshot)
                {
                    writer.WriteLine("{0},{1},{2},{3},{4},{5}", row.Ticker, row.Open, row.High, row.Low, row.Close, row.Volume);
                }
                writer.Flush();
            }
        }
        //saves the difference between current and last snapshot
        //into disk file
        private void SaveData(int sId)
        {
            //deal with minute data
            DateTime dt = DateTime.Now;
            string year = dt.Year.ToString();
            string date = dt.ToString("yyyy-MM-dd");
            string time = dt.ToString("HH:mm:ss");
            StringBuilder sbm = new StringBuilder(destFolder +@"min\"+ year + @"\" + date + @"\");
            //if the folder does not exist, create
            if(Directory.Exists(sbm.ToString()) == false)
                Directory.CreateDirectory(sbm.ToString());

            foreach (var row in deltaSnapshot)
            {
                string fName = sbm.ToString() + row.Ticker + ".txt";
                using (FileStream fs = new FileStream(fName, FileMode.Append))
                {
                    StreamWriter writer = new StreamWriter(fs);
                    writer.WriteLine("{0},{1},{2}", time, row.LTP, row.Volume);
                    writer.Flush();
                }
            }
        }// SaveData()

        //store certain data to the database
        public void StoreDataToDB(string dbConString, int snapID)
        {
            string query;

            SQLiteConnection dbConnection = new SQLiteConnection(dbConString);
            dbConnection.Open();
            
            try
            {
                using (SQLiteCommand command = dbConnection.CreateCommand())
                {
                    foreach (var row in curSnapshot)
                    {
                        if (snapID == 1)
                        {
                            //writer.WriteLine("{0},{1},{2},{3},{4},{5}", row.Ticker, row.Open, row.High, row.Low, row.Close, row.Volume);
                            //update Recent Table
                            query = @"UPDATE Recent SET Open = " + row.Open + ", LTP =" + row.Close + " WHERE Ticker = '" + row.Ticker + "';";
                            command.CommandText = query;
                            command.ExecuteNonQuery();
                        }
                       /* else
                        {
                            query = @"UPDATE Recent SET LTP = " + row.Close + " WHERE Ticker = '" + row.Ticker + "';";
                            command.CommandText = query;
                            command.ExecuteNonQuery();                        

                        } */
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                dbConnection.Close();  
            }
            
        }
        //calculates the differences between last snaphot and current one
        private void CalculateDelta()
        {
            SnapDelta aDelta;
            MinuteData aData; 

            //clear the delta table
            deltaSnapshot.Clear();
            for (int i = 0; i < curSnapshot.Count; i++)
            {
                if (curSnapshot[i].Ticker == lastSnapshot[i].Ticker) // we are looking at the same symbol
                {
                    aDelta = new SnapDelta();

                    aDelta.Ticker = curSnapshot[i].Ticker; //copy ticker name
                    // greater volume means theres been trades since last snapshot
                    if (Int32.Parse(curSnapshot[i].Volume) >= Int32.Parse(lastSnapshot[i].Volume))
                    {
                        aDelta.LTP = curSnapshot[i].Close;
                        int dVol = Int32.Parse(curSnapshot[i].Volume) - Int32.Parse(lastSnapshot[i].Volume);
                        if (dVol < 0) // dont allow negative volume
                            dVol = 0;
                        aDelta.Volume = dVol.ToString();
                    }
                    else
                    {
                        aDelta.LTP = lastSnapshot[i].Close;
                        aDelta.Volume = (0).ToString();
                    }
                    //update the delta table
                    deltaSnapshot.Add(aDelta);

                    //fowlowing happens if a sharp rise/fall happens withing the measuring interval 
                    if (float.Parse(curSnapshot[i].High) > float.Parse(lastSnapshot[i].High))
                    {
                        aData = new MinuteData();
                        aData = curSnapshot[i];
                        aData.Close = curSnapshot[i].High;
                        curSnapshot[i] = aData; // HH becomes LTP/Close
                    }
                    if (float.Parse(curSnapshot[i].Low) < float.Parse(lastSnapshot[i].Low))
                    {
                        aData = new MinuteData();
                        aData = curSnapshot[i];
                        aData.Close = curSnapshot[i].Low;
                        curSnapshot[i] = aData; // LL becomes LTP/Close
                    }
                } // if ticker matches
            } // for i
        }// CalculateDelta
    } // DataGrabber
}
