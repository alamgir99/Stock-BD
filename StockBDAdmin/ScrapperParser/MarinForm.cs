using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using StockBDWeb.Others;

namespace ScrapperParser
{
    public partial class frmMainForm : Form
    {
        //non visual member variables
        private bool isTradingDay; // whether today is a trading day
        private bool isTradingStarted; // whether the trading session started
        private bool isEODDone; // whether EOD grab is successfull
        private int nCurEODRetry; // 
        private int nSnapshotCounter;
        
        DataGrabber dataSource;
        string tempFolder;
        string dataFolder;

        //worker thread
        Thread wThread;

        //application settings
        ApplicationSettings m_settings;

        public frmMainForm()
        {
            InitializeComponent();
            //ideally get from settings file
            tempFolder = txtTempFolder.Text;
            dataFolder = txtMinDataFolder.Text;
            //initialize own member variables
            isTradingDay = false;
            isTradingStarted = false;
            isEODDone = false;
            nCurEODRetry = 1;
            nSnapshotCounter = 1;
            WebRequest.DefaultWebProxy = null;
            wThread = null;
            bkgndWorker.WorkerReportsProgress = true;
            bkgndWorker.WorkerSupportsCancellation = true;

            m_settings = new ApplicationSettings();
        }

        private void frmMainForm_Load(object sender, EventArgs e)
        {
            // change the time format
            tmStartTime.Format = DateTimePickerFormat.Custom;
            tmStartTime.CustomFormat = "hh:mm tt";
            tmStartTime.ShowUpDown = true;

            tmEndTime.Format = DateTimePickerFormat.Custom;
            tmEndTime.CustomFormat = "hh:mm tt";
            tmEndTime.ShowUpDown = true;

            tmEODHitTime.Format = DateTimePickerFormat.Custom;
            tmEODHitTime.CustomFormat = "hh:mm tt";
            tmEODHitTime.ShowUpDown = true;

            //start and end time
            tmStartTime.Value = Convert.ToDateTime(m_settings.settings["startTime"]);
            tmEndTime.Value = Convert.ToDateTime(m_settings.settings["endTime"]);
        }

        private void btnStartStop_Click(object sender, EventArgs e)
        {
            // When Start is pressed, just enable the TradingHourDay
            // timer. Rest of the processes are event based.
            if (timerTradingHourDay.Enabled == true)
            { // we are running, so must be a stop request
                timerTradingHourDay.Enabled = false;
                btnStartStop.Text = "Start";

                //show the current status
                lblStatus.Text = "Stopped";
                lblStatus.BackColor = Color.Red;
            } else { // we are not running, so it is a start request
                timerTradingHourDay.Enabled = true;
                btnStartStop.Text = "Stop";

                //show the current status
                lblStatus.Text = "Monitoring";
                lblStatus.BackColor = Color.Green;

                //clear Message and errors
                lstMessages.Items.Clear();
                lstErrors.Items.Clear();

                //create data source
                if(radMinDataSourceDSE.Checked == true)
                    dataSource = new DSEDataGrabber(dataFolder, tempFolder, OnComplete); 
                else if(radMinDataSourceEPL.Checked == true)
                    dataSource = new BRACDataGrabber(dataFolder, tempFolder, OnComplete);
                else if(radMinDataSourceSQ.Checked == true)
                    dataSource = new SqDataGrabber(dataFolder, tempFolder, OnComplete);
                else if (radMinDataSourceUTFD.Checked == true)
                    dataSource = new UTFDataGrabber(dataFolder, tempFolder, OnComplete);
            }

        }

        private void timerTradingHourDay_Tick(object sender, EventArgs e)
        {
            //this timer does 3 things
            // (1). sees if it is a trading day (not weekends or holidays) solely
            //      relying on the checkboxes in GUI and current day name
            // (2). sees if it is a trading hour.
            // (3). sees if it is a trading hour and trading session is over
            
            /*-------------------- Trading day and hours begins ------------*/
            //this timer ideally should be called just before a trading
            //session. As we have 10.55am as starting time (whereas actual
            //session begins at 11.00am) we have 5 minutes for house keeping

            DateTime dt =  DateTime.Now;
            int nHour = dt.Hour;
            int nMin = dt.Minute;

            //debug only
            //lstMessages.Items.Add(new ListViewItem(new string[] { dt.ToShortTimeString(), @"Monitoring trading hour/day." }));

            if (nHour >= tmStartTime.Value.Hour && nHour <= tmEndTime.Value.Hour) // we are in the window
            {  
                if (nMin >= tmStartTime.Value.Minute)
                { // a prospective trading day
                    if (isTradingStarted) return;
 
                    DayOfWeek toDay = dt.DayOfWeek;

                    if ((toDay == DayOfWeek.Sunday && chkSunDay.Checked == true) ||
                         (toDay == DayOfWeek.Monday && chkMonDay.Checked == true) ||
                         (toDay == DayOfWeek.Tuesday && chkTuesDay.Checked == true) ||
                         (toDay == DayOfWeek.Wednesday && chkWedDay.Checked == true) ||
                         (toDay == DayOfWeek.Thursday && chkThursDay.Checked == true))
                    {
                        // a trading session is about to begin
                        isTradingDay = true;
                        isTradingStarted = true;
                        isEODDone = false;
                        nCurEODRetry = 1;
                        //turn on minute grabber
                        timerMinGrabber.Enabled = true;
                        lstMessages.Items.Clear();
                        //show a message
                        lstMessages.Items.Add(new ListViewItem(new string[] {dt.ToString(), @"Trading Session is about to begin."}));
                        
                        //live status
                        lblLive.Text = "Live";
                        lblLive.BackColor = Color.Green;

                        //set the settings so that the web site knows
                        m_settings.settings["liveSession"] = "true";
                        m_settings.SaveSettings();
                    }
                }
            }
            else if (nHour == tmEndTime.Value.Hour)
            {
                if (nMin == tmEndTime.Value.Minute)
                {
                    // a sesseion comes to an end
                    if (isTradingStarted == false) return;
                    isTradingStarted = false;

                    //turn off minute grabber
                    timerMinGrabber.Enabled = false;
                    //show a message
                    lstMessages.Items.Add(new ListViewItem(new string[] { dt.ToShortTimeString(), @"Trading Session is about to end." }));

                    //live status
                    lblLive.Text = "Off";
                    lblLive.BackColor = Color.Gray;

                    //set the settings so that the web site knows
                    m_settings.settings["liveSession"] = "false";
                    m_settings.SaveSettings();
                }
            }
            else
            {
                //live status
                lblLive.Text = "Off";
                lblLive.BackColor = Color.Gray;
                //set the settings so that the web site knows
                m_settings.settings["liveSession"] = "false";
                m_settings.SaveSettings();

            }

            /*-------------------- Trading day and hours ends ------------*/

            /*-------------------- Trading day and EOD begins ------------*/
            if (isTradingDay)
            {
                if (nHour == tmEODHitTime.Value.Hour) // we are on the same hour
                {
                    if (nMin == tmEODHitTime.Value.Minute)
                    { // time to hit the EOD grabber
                        timerEODGrabber.Enabled = true;
                    }
                }
            }
            /*-------------------- Trading day and EOD ends ------------*/
            
            //live non live status
            if (m_settings.settings["liveSession"] == "true")
            {
                lblLive.Text = "Live";
            }
            else
            {
                lblLive.Text = "Off";
            }
        }

        //timer event for grabbing EOD data
        //it is a 15 minute timer
        //if the attemp is successful on first attempt, the timer is
        //disable untill next day. if unsuccessfull a total of txtNumTry
        //attempt is made
        private void timerEODGrabber_Tick(object sender, EventArgs e)
        {
            if (isEODDone == true)
                return;

            bool eod_done = true;
            // try to get EOD mst
            try
            {
                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Grabbing MST file..." }));
                Application.DoEvents();

                EODDataGrabber dseEOD = new EODDataGrabber(dataFolder);
                dseEOD.GrabAndSave();
                string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
                dseEOD.StoreToDB(dbConString);

                int lastItem = lstMessages.Items.Count - 1;
                lstMessages.Items[lastItem].SubItems[1].Text = @"Grabbing MST file... Done. Took " + dseEOD.linkTime + " sec";

            }
            catch (Exception ex)
            {
                lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"timerEODGrabber_Tick " + ex.Message }));
            }

            // if success
            if (eod_done)
            {
                timerEODGrabber.Enabled = false;
                isEODDone = true;
                //end of trading day
                isTradingDay = false;

                //some cleanup
                System.IO.DirectoryInfo directory = new System.IO.DirectoryInfo(tempFolder+@"\dldata\");
                directory.Empty();
            }
            else
            {
                //if fails
                 nCurEODRetry += 1; // try again
                 if (nCurEODRetry >= Int32.Parse(txtNumRetry.Text)) // too many retry
                     timerEODGrabber.Enabled = false;
            }
        }

        //notify event handler for minute data
        public void OnComplete(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            DataGrabber dse = (DataGrabber)sender;
            int lastItem = lstMessages.Items.Count - 1;
            lstMessages.Items[lastItem].SubItems[1].Text = @"Minute data... Done. Took " + dse.LinkTime() + " sec";
        }

        private void timerMinGrabber_Tick(object sender, EventArgs e)
        {
            DateTime dt = DateTime.Now;
            string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
            //show a message
            int lastItem = lstMessages.Items.Count-1;
            if (lastItem < 0)  lastItem = 0;
            lstMessages.Items.Add(new ListViewItem(new string[] { dt.ToShortTimeString(), @"Getting minute data..." }));
            try
            {
                dataSource.GrabAndSave(nSnapshotCounter, chkSaveTempFile.Checked);
                dataSource.StoreDataToDB(dbConString, nSnapshotCounter);
                nSnapshotCounter++;
            }
            catch (Exception ex)
            {
                lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), nSnapshotCounter.ToString() +@" : timerMinGrabber_Tick " + ex.Message }));
                //timerMinGrabber.Enabled = false; // stop further timer events of grabbing minute data
            }

        }

        //select the DB File
        private void btnPathDB_Click(object sender, EventArgs e)
        {

            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                txtDBPath.Text = openFileDlg.FileName;
            }

        }

        //select temp path
        private void btnBrowsePathTemp_Click(object sender, EventArgs e)
        {
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtTempFolder.Text = folderBrowserDlg.SelectedPath;
                tempFolder = txtTempFolder.Text + @"\";
            }
        }

        //select min data path
        private void btnBrowsePathMinData_Click(object sender, EventArgs e)
        {
            if (folderBrowserDlg.ShowDialog() == DialogResult.OK)
            {
                txtMinDataFolder.Text = folderBrowserDlg.SelectedPath;
                dataFolder = txtMinDataFolder.Text + @"\";
            }
        }

        /*
        //update the list of ticker symbols- manual - background worker version
        private void btnUpdateTicker_Click(object sender, EventArgs e)
        {
            if (bkgndWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                bkgndWorker.RunWorkerAsync();
            }
        }
        */

        /*
            string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
            CompanyListGrabber dse;

            //disable the Command button
            btnUpdateTicker.Enabled = false;
            try
            {
                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Updating ticker list." }));
                dse = new CompanyListGrabber(dbConString);
                wThread = new Thread(new ThreadStart(dse.GrabAndStore));
                wThread.Start();
                while (!wThread.IsAlive) ;
                wThread.Join(1000);

                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Ticker list update done." }));
                //enable the Command button
                btnUpdateTicker.Enabled = true;
                wThread = null; // reset to zero for reuse
            }
            catch (Exception ex)
            {
                lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), ex.Message }));
            }
        }
        */
       
        //update the list of ticker symbols- manual
        private void btnUpdateTicker_Click(object sender, EventArgs e)
        {
            string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
            CompanyListGrabber dse;

            //disable the Command button
            btnUpdateTicker.Enabled = false;
                try
                {
                    lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Updating ticker list." }));
                    dse = new CompanyListGrabber(dbConString);
                    wThread = new Thread(new ThreadStart(dse.GrabAndStore));
                    wThread.Start();
                    while (!wThread.IsAlive);
                    wThread.Join(1000);

                    lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Ticker list update done." }));
                    //enable the Command button
                    btnUpdateTicker.Enabled = true;
                    wThread = null; // reset to zero for reuse
                }
                catch (Exception ex)
                {
                    lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"btnUpdateTicker_Click " + ex.Message }));
                }       
        }
       
        /*
        //update funda data - manual
        private void btnUpdateFunda_Click(object sender, EventArgs e)
        {
            if (wThread != null) return;

            string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
            FundaDataGrabber dseFunda; 
            try
            {
                prgBar.Maximum = 100;
                dseFunda = new FundaDataGrabber(dbConString);
                btnUpdateFunda.Enabled = false;
                wThread = new Thread(new ThreadStart(dseFunda.GrabAndSave));
                wThread.Start();
                while (!wThread.IsAlive) ;

                wThread.Join(1000);  
                lstMessages.Items.Add(new ListViewItem(new string[]{DateTime.Now.ToString(), @"Funda update done."}));
                wThread = null;
                btnUpdateFunda.Enabled = true;
            }
            catch (Exception ex)
            {
                lstErrors.Items.Add(new ListViewItem(new string[]{DateTime.Now.ToString(), ex.Message}));
            }

        }
        */
        private void btnUpdateFunda_Click(object sender, EventArgs e)
        {
            if (bkgndWorker.IsBusy != true)
            {
                // Start the asynchronous operation.
                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Updating funda ..." }));
                bkgndWorker.RunWorkerAsync();
                btnUpdateFunda.Text = @"Cancel Funda";
            }
            else
            {
                bkgndWorker.CancelAsync();
                btnUpdateFunda.Text = @"Canceling ...";
                btnUpdateFunda.Enabled = false;
            }
        }
        //background worker
        private void bkgndWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string dbConString = @"Data Source=" + txtDBPath.Text + ";Version=3;";
            FundaDataGrabber dseFunda;
            try
            {
                prgBar.Maximum = 100;
                prgBar.Value = 0;
                dseFunda = new FundaDataGrabber(dbConString, sender);
                dseFunda.GrabAndSave();
            }
            catch (Exception ex)
            {
                throw;
                //lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), ex.Message }));
            }
        }
        //PROGRESS
        private void bkgndWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //System.Windows.Forms.MessageBox.Show("Progress :" + e.ProgressPercentage.ToString());
            prgBar.Value = e.ProgressPercentage;
        }
        //complete
        private void bkgndWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (prgBar.Value < 99)
            {
                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Funda update cancelled." }));
            }
            else if (e.Error != null)
            {
                lstErrors.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), e.Error.Message }));
            }
            else
            {
                lstMessages.Items.Add(new ListViewItem(new string[] { DateTime.Now.ToString(), @"Funda update done." }));
            }
            btnUpdateFunda.Enabled = true;
            btnUpdateFunda.Text = @"Update Funda";
            prgBar.Value = 0;
        }

        private void btnGetEODNow_Click(object sender, EventArgs e)
        {
            timerEODGrabber_Tick(sender, e);
        }
       }
}
