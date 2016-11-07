namespace ScrapperParser
{
    partial class frmMainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStartStop = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTimeStep = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radMinDataSourceUTFD = new System.Windows.Forms.RadioButton();
            this.tmEndTime = new System.Windows.Forms.DateTimePicker();
            this.tmStartTime = new System.Windows.Forms.DateTimePicker();
            this.radMinDataSourceSQ = new System.Windows.Forms.RadioButton();
            this.radMinDataSourceEPL = new System.Windows.Forms.RadioButton();
            this.radMinDataSourceDSE = new System.Windows.Forms.RadioButton();
            this.label6 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGetEODNow = new System.Windows.Forms.Button();
            this.txtNumRetry = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tmEODHitTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkThursDay = new System.Windows.Forms.CheckBox();
            this.chkWedDay = new System.Windows.Forms.CheckBox();
            this.chkTuesDay = new System.Windows.Forms.CheckBox();
            this.chkMonDay = new System.Windows.Forms.CheckBox();
            this.chkSunDay = new System.Windows.Forms.CheckBox();
            this.timerTradingHourDay = new System.Windows.Forms.Timer(this.components);
            this.timerMinGrabber = new System.Windows.Forms.Timer(this.components);
            this.timerEODGrabber = new System.Windows.Forms.Timer(this.components);
            this.lblStatus = new System.Windows.Forms.Label();
            this.lstMessages = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colMessage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.chkDecember = new System.Windows.Forms.CheckBox();
            this.chkNovember = new System.Windows.Forms.CheckBox();
            this.chkOctober = new System.Windows.Forms.CheckBox();
            this.chkSeptember = new System.Windows.Forms.CheckBox();
            this.chkAugust = new System.Windows.Forms.CheckBox();
            this.chkJuly = new System.Windows.Forms.CheckBox();
            this.chkJune = new System.Windows.Forms.CheckBox();
            this.chkMay = new System.Windows.Forms.CheckBox();
            this.chkApril = new System.Windows.Forms.CheckBox();
            this.chkMarch = new System.Windows.Forms.CheckBox();
            this.chkFebruary = new System.Windows.Forms.CheckBox();
            this.chkJanuary = new System.Windows.Forms.CheckBox();
            this.txtFundaDay = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.lstErrors = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnBrowsePathMinData = new System.Windows.Forms.Button();
            this.btnBrowsePathTemp = new System.Windows.Forms.Button();
            this.chkSaveTempFile = new System.Windows.Forms.CheckBox();
            this.txtMinDataFolder = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTempFolder = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnPathDB = new System.Windows.Forms.Button();
            this.btnUpdateFunda = new System.Windows.Forms.Button();
            this.btnUpdateTicker = new System.Windows.Forms.Button();
            this.txtDBPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.folderBrowserDlg = new System.Windows.Forms.FolderBrowserDialog();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.bkgndWorker = new System.ComponentModel.BackgroundWorker();
            this.lblLive = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStartStop
            // 
            this.btnStartStop.Location = new System.Drawing.Point(565, 185);
            this.btnStartStop.Name = "btnStartStop";
            this.btnStartStop.Size = new System.Drawing.Size(75, 40);
            this.btnStartStop.TabIndex = 0;
            this.btnStartStop.Text = "Start";
            this.btnStartStop.UseVisualStyleBackColor = true;
            this.btnStartStop.Click += new System.EventHandler(this.btnStartStop_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Time :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "End Time :";
            // 
            // txtTimeStep
            // 
            this.txtTimeStep.Location = new System.Drawing.Point(70, 70);
            this.txtTimeStep.Name = "txtTimeStep";
            this.txtTimeStep.Size = new System.Drawing.Size(55, 20);
            this.txtTimeStep.TabIndex = 5;
            this.txtTimeStep.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Time Step :";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(131, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "minute";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radMinDataSourceUTFD);
            this.groupBox1.Controls.Add(this.tmEndTime);
            this.groupBox1.Controls.Add(this.tmStartTime);
            this.groupBox1.Controls.Add(this.radMinDataSourceSQ);
            this.groupBox1.Controls.Add(this.radMinDataSourceEPL);
            this.groupBox1.Controls.Add(this.radMinDataSourceDSE);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtTimeStep);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 213);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Minute Data";
            // 
            // radMinDataSourceUTFD
            // 
            this.radMinDataSourceUTFD.AutoSize = true;
            this.radMinDataSourceUTFD.Location = new System.Drawing.Point(73, 183);
            this.radMinDataSourceUTFD.Name = "radMinDataSourceUTFD";
            this.radMinDataSourceUTFD.Size = new System.Drawing.Size(54, 17);
            this.radMinDataSourceUTFD.TabIndex = 14;
            this.radMinDataSourceUTFD.Text = "UTFD";
            this.radMinDataSourceUTFD.UseVisualStyleBackColor = true;
            // 
            // tmEndTime
            // 
            this.tmEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmEndTime.Location = new System.Drawing.Point(70, 44);
            this.tmEndTime.Name = "tmEndTime";
            this.tmEndTime.Size = new System.Drawing.Size(112, 20);
            this.tmEndTime.TabIndex = 13;
            this.tmEndTime.Value = new System.DateTime(2016, 4, 8, 14, 31, 0, 0);
            // 
            // tmStartTime
            // 
            this.tmStartTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmStartTime.Location = new System.Drawing.Point(70, 19);
            this.tmStartTime.Name = "tmStartTime";
            this.tmStartTime.Size = new System.Drawing.Size(112, 20);
            this.tmStartTime.TabIndex = 12;
            this.tmStartTime.Value = new System.DateTime(2016, 4, 8, 10, 29, 0, 0);
            // 
            // radMinDataSourceSQ
            // 
            this.radMinDataSourceSQ.AutoSize = true;
            this.radMinDataSourceSQ.Location = new System.Drawing.Point(73, 157);
            this.radMinDataSourceSQ.Name = "radMinDataSourceSQ";
            this.radMinDataSourceSQ.Size = new System.Drawing.Size(81, 17);
            this.radMinDataSourceSQ.TabIndex = 11;
            this.radMinDataSourceSQ.Text = "Square Sec";
            this.radMinDataSourceSQ.UseVisualStyleBackColor = true;
            // 
            // radMinDataSourceEPL
            // 
            this.radMinDataSourceEPL.AutoSize = true;
            this.radMinDataSourceEPL.Location = new System.Drawing.Point(73, 129);
            this.radMinDataSourceEPL.Name = "radMinDataSourceEPL";
            this.radMinDataSourceEPL.Size = new System.Drawing.Size(77, 17);
            this.radMinDataSourceEPL.TabIndex = 10;
            this.radMinDataSourceEPL.Text = "BRAC EPL";
            this.radMinDataSourceEPL.UseVisualStyleBackColor = true;
            // 
            // radMinDataSourceDSE
            // 
            this.radMinDataSourceDSE.AutoSize = true;
            this.radMinDataSourceDSE.Checked = true;
            this.radMinDataSourceDSE.Location = new System.Drawing.Point(73, 102);
            this.radMinDataSourceDSE.Name = "radMinDataSourceDSE";
            this.radMinDataSourceDSE.Size = new System.Drawing.Size(65, 17);
            this.radMinDataSourceDSE.TabIndex = 9;
            this.radMinDataSourceDSE.TabStop = true;
            this.radMinDataSourceDSE.Text = "DSE BD";
            this.radMinDataSourceDSE.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(19, 102);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Source :";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGetEODNow);
            this.groupBox2.Controls.Add(this.txtNumRetry);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.tmEODHitTime);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(209, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(200, 73);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "End of Day Data";
            // 
            // btnGetEODNow
            // 
            this.btnGetEODNow.Location = new System.Drawing.Point(93, 41);
            this.btnGetEODNow.Name = "btnGetEODNow";
            this.btnGetEODNow.Size = new System.Drawing.Size(86, 25);
            this.btnGetEODNow.TabIndex = 7;
            this.btnGetEODNow.Text = "Get Now";
            this.btnGetEODNow.UseVisualStyleBackColor = true;
            this.btnGetEODNow.Click += new System.EventHandler(this.btnGetEODNow_Click);
            // 
            // txtNumRetry
            // 
            this.txtNumRetry.Location = new System.Drawing.Point(61, 43);
            this.txtNumRetry.Name = "txtNumRetry";
            this.txtNumRetry.Size = new System.Drawing.Size(26, 20);
            this.txtNumRetry.TabIndex = 16;
            this.txtNumRetry.Text = "5";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 47);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "# Retry :";
            // 
            // tmEODHitTime
            // 
            this.tmEODHitTime.Format = System.Windows.Forms.DateTimePickerFormat.Time;
            this.tmEODHitTime.Location = new System.Drawing.Point(61, 19);
            this.tmEODHitTime.Name = "tmEODHitTime";
            this.tmEODHitTime.Size = new System.Drawing.Size(112, 20);
            this.tmEODHitTime.TabIndex = 14;
            this.tmEODHitTime.Value = new System.DateTime(2016, 4, 8, 18, 5, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Hit Time :";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkThursDay);
            this.groupBox3.Controls.Add(this.chkWedDay);
            this.groupBox3.Controls.Add(this.chkTuesDay);
            this.groupBox3.Controls.Add(this.chkMonDay);
            this.groupBox3.Controls.Add(this.chkSunDay);
            this.groupBox3.Location = new System.Drawing.Point(209, 94);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 82);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Open Days";
            // 
            // chkThursDay
            // 
            this.chkThursDay.AutoSize = true;
            this.chkThursDay.Checked = true;
            this.chkThursDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkThursDay.Location = new System.Drawing.Point(74, 43);
            this.chkThursDay.Name = "chkThursDay";
            this.chkThursDay.Size = new System.Drawing.Size(45, 17);
            this.chkThursDay.TabIndex = 4;
            this.chkThursDay.Text = "Thu";
            this.chkThursDay.UseVisualStyleBackColor = true;
            // 
            // chkWedDay
            // 
            this.chkWedDay.AutoSize = true;
            this.chkWedDay.Checked = true;
            this.chkWedDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWedDay.Location = new System.Drawing.Point(13, 43);
            this.chkWedDay.Name = "chkWedDay";
            this.chkWedDay.Size = new System.Drawing.Size(49, 17);
            this.chkWedDay.TabIndex = 3;
            this.chkWedDay.Text = "Wed";
            this.chkWedDay.UseVisualStyleBackColor = true;
            // 
            // chkTuesDay
            // 
            this.chkTuesDay.AutoSize = true;
            this.chkTuesDay.Checked = true;
            this.chkTuesDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTuesDay.Location = new System.Drawing.Point(134, 20);
            this.chkTuesDay.Name = "chkTuesDay";
            this.chkTuesDay.Size = new System.Drawing.Size(45, 17);
            this.chkTuesDay.TabIndex = 2;
            this.chkTuesDay.Text = "Tue";
            this.chkTuesDay.UseVisualStyleBackColor = true;
            // 
            // chkMonDay
            // 
            this.chkMonDay.AutoSize = true;
            this.chkMonDay.Checked = true;
            this.chkMonDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMonDay.Location = new System.Drawing.Point(73, 20);
            this.chkMonDay.Name = "chkMonDay";
            this.chkMonDay.Size = new System.Drawing.Size(47, 17);
            this.chkMonDay.TabIndex = 1;
            this.chkMonDay.Text = "Mon";
            this.chkMonDay.UseVisualStyleBackColor = true;
            // 
            // chkSunDay
            // 
            this.chkSunDay.AutoSize = true;
            this.chkSunDay.Checked = true;
            this.chkSunDay.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSunDay.Location = new System.Drawing.Point(14, 20);
            this.chkSunDay.Name = "chkSunDay";
            this.chkSunDay.Size = new System.Drawing.Size(45, 17);
            this.chkSunDay.TabIndex = 0;
            this.chkSunDay.Text = "Sun";
            this.chkSunDay.UseVisualStyleBackColor = true;
            // 
            // timerTradingHourDay
            // 
            this.timerTradingHourDay.Interval = 10000;
            this.timerTradingHourDay.Tick += new System.EventHandler(this.timerTradingHourDay_Tick);
            // 
            // timerMinGrabber
            // 
            this.timerMinGrabber.Interval = 60000;
            this.timerMinGrabber.Tick += new System.EventHandler(this.timerMinGrabber_Tick);
            // 
            // timerEODGrabber
            // 
            this.timerEODGrabber.Interval = 90000;
            this.timerEODGrabber.Tick += new System.EventHandler(this.timerEODGrabber_Tick);
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Gray;
            this.lblStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.Location = new System.Drawing.Point(313, 184);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(96, 40);
            this.lblStatus.TabIndex = 11;
            this.lblStatus.Text = "Status";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lstMessages
            // 
            this.lstMessages.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colMessage});
            this.lstMessages.Location = new System.Drawing.Point(16, 237);
            this.lstMessages.Name = "lstMessages";
            this.lstMessages.ShowItemToolTips = true;
            this.lstMessages.Size = new System.Drawing.Size(353, 179);
            this.lstMessages.TabIndex = 12;
            this.lstMessages.UseCompatibleStateImageBehavior = false;
            this.lstMessages.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Time";
            this.colTime.Width = 110;
            // 
            // colMessage
            // 
            this.colMessage.Text = "Message";
            this.colMessage.Width = 237;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.chkDecember);
            this.groupBox4.Controls.Add(this.chkNovember);
            this.groupBox4.Controls.Add(this.chkOctober);
            this.groupBox4.Controls.Add(this.chkSeptember);
            this.groupBox4.Controls.Add(this.chkAugust);
            this.groupBox4.Controls.Add(this.chkJuly);
            this.groupBox4.Controls.Add(this.chkJune);
            this.groupBox4.Controls.Add(this.chkMay);
            this.groupBox4.Controls.Add(this.chkApril);
            this.groupBox4.Controls.Add(this.chkMarch);
            this.groupBox4.Controls.Add(this.chkFebruary);
            this.groupBox4.Controls.Add(this.chkJanuary);
            this.groupBox4.Controls.Add(this.txtFundaDay);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Location = new System.Drawing.Point(415, 12);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(226, 163);
            this.groupBox4.TabIndex = 13;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Funda Data";
            // 
            // chkDecember
            // 
            this.chkDecember.AutoSize = true;
            this.chkDecember.Location = new System.Drawing.Point(150, 123);
            this.chkDecember.Name = "chkDecember";
            this.chkDecember.Size = new System.Drawing.Size(75, 17);
            this.chkDecember.TabIndex = 13;
            this.chkDecember.Text = "December";
            this.chkDecember.UseVisualStyleBackColor = true;
            // 
            // chkNovember
            // 
            this.chkNovember.AutoSize = true;
            this.chkNovember.Location = new System.Drawing.Point(74, 123);
            this.chkNovember.Name = "chkNovember";
            this.chkNovember.Size = new System.Drawing.Size(75, 17);
            this.chkNovember.TabIndex = 12;
            this.chkNovember.Text = "November";
            this.chkNovember.UseVisualStyleBackColor = true;
            // 
            // chkOctober
            // 
            this.chkOctober.AutoSize = true;
            this.chkOctober.Checked = true;
            this.chkOctober.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOctober.Location = new System.Drawing.Point(12, 123);
            this.chkOctober.Name = "chkOctober";
            this.chkOctober.Size = new System.Drawing.Size(64, 17);
            this.chkOctober.TabIndex = 11;
            this.chkOctober.Text = "October";
            this.chkOctober.UseVisualStyleBackColor = true;
            // 
            // chkSeptember
            // 
            this.chkSeptember.AutoSize = true;
            this.chkSeptember.Location = new System.Drawing.Point(150, 99);
            this.chkSeptember.Name = "chkSeptember";
            this.chkSeptember.Size = new System.Drawing.Size(77, 17);
            this.chkSeptember.TabIndex = 10;
            this.chkSeptember.Text = "September";
            this.chkSeptember.UseVisualStyleBackColor = true;
            // 
            // chkAugust
            // 
            this.chkAugust.AutoSize = true;
            this.chkAugust.Location = new System.Drawing.Point(74, 98);
            this.chkAugust.Name = "chkAugust";
            this.chkAugust.Size = new System.Drawing.Size(59, 17);
            this.chkAugust.TabIndex = 9;
            this.chkAugust.Text = "August";
            this.chkAugust.UseVisualStyleBackColor = true;
            // 
            // chkJuly
            // 
            this.chkJuly.AutoSize = true;
            this.chkJuly.Checked = true;
            this.chkJuly.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJuly.Location = new System.Drawing.Point(12, 99);
            this.chkJuly.Name = "chkJuly";
            this.chkJuly.Size = new System.Drawing.Size(44, 17);
            this.chkJuly.TabIndex = 8;
            this.chkJuly.Text = "July";
            this.chkJuly.UseVisualStyleBackColor = true;
            // 
            // chkJune
            // 
            this.chkJune.AutoSize = true;
            this.chkJune.Location = new System.Drawing.Point(150, 75);
            this.chkJune.Name = "chkJune";
            this.chkJune.Size = new System.Drawing.Size(49, 17);
            this.chkJune.TabIndex = 7;
            this.chkJune.Text = "June";
            this.chkJune.UseVisualStyleBackColor = true;
            // 
            // chkMay
            // 
            this.chkMay.AutoSize = true;
            this.chkMay.Location = new System.Drawing.Point(74, 75);
            this.chkMay.Name = "chkMay";
            this.chkMay.Size = new System.Drawing.Size(46, 17);
            this.chkMay.TabIndex = 6;
            this.chkMay.Text = "May";
            this.chkMay.UseVisualStyleBackColor = true;
            // 
            // chkApril
            // 
            this.chkApril.AutoSize = true;
            this.chkApril.Checked = true;
            this.chkApril.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkApril.Location = new System.Drawing.Point(12, 75);
            this.chkApril.Name = "chkApril";
            this.chkApril.Size = new System.Drawing.Size(46, 17);
            this.chkApril.TabIndex = 5;
            this.chkApril.Text = "April";
            this.chkApril.UseVisualStyleBackColor = true;
            // 
            // chkMarch
            // 
            this.chkMarch.AutoSize = true;
            this.chkMarch.Location = new System.Drawing.Point(150, 51);
            this.chkMarch.Name = "chkMarch";
            this.chkMarch.Size = new System.Drawing.Size(56, 17);
            this.chkMarch.TabIndex = 4;
            this.chkMarch.Text = "March";
            this.chkMarch.UseVisualStyleBackColor = true;
            // 
            // chkFebruary
            // 
            this.chkFebruary.AutoSize = true;
            this.chkFebruary.Location = new System.Drawing.Point(74, 51);
            this.chkFebruary.Name = "chkFebruary";
            this.chkFebruary.Size = new System.Drawing.Size(67, 17);
            this.chkFebruary.TabIndex = 3;
            this.chkFebruary.Text = "February";
            this.chkFebruary.UseVisualStyleBackColor = true;
            // 
            // chkJanuary
            // 
            this.chkJanuary.AutoSize = true;
            this.chkJanuary.Checked = true;
            this.chkJanuary.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkJanuary.Location = new System.Drawing.Point(12, 51);
            this.chkJanuary.Name = "chkJanuary";
            this.chkJanuary.Size = new System.Drawing.Size(63, 17);
            this.chkJanuary.TabIndex = 2;
            this.chkJanuary.Text = "January";
            this.chkJanuary.UseVisualStyleBackColor = true;
            // 
            // txtFundaDay
            // 
            this.txtFundaDay.Location = new System.Drawing.Point(54, 21);
            this.txtFundaDay.Name = "txtFundaDay";
            this.txtFundaDay.Size = new System.Drawing.Size(66, 20);
            this.txtFundaDay.TabIndex = 1;
            this.txtFundaDay.Text = "1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(11, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(36, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Day #";
            // 
            // lstErrors
            // 
            this.lstErrors.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.lstErrors.Location = new System.Drawing.Point(375, 237);
            this.lstErrors.Name = "lstErrors";
            this.lstErrors.ShowItemToolTips = true;
            this.lstErrors.Size = new System.Drawing.Size(522, 179);
            this.lstErrors.TabIndex = 14;
            this.lstErrors.UseCompatibleStateImageBehavior = false;
            this.lstErrors.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Time";
            this.columnHeader1.Width = 84;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Error";
            this.columnHeader2.Width = 300;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(415, 185);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(133, 40);
            this.prgBar.TabIndex = 15;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnBrowsePathMinData);
            this.groupBox5.Controls.Add(this.btnBrowsePathTemp);
            this.groupBox5.Controls.Add(this.chkSaveTempFile);
            this.groupBox5.Controls.Add(this.txtMinDataFolder);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.txtTempFolder);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Location = new System.Drawing.Point(650, 13);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(247, 129);
            this.groupBox5.TabIndex = 16;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Settings";
            // 
            // btnBrowsePathMinData
            // 
            this.btnBrowsePathMinData.Location = new System.Drawing.Point(215, 74);
            this.btnBrowsePathMinData.Name = "btnBrowsePathMinData";
            this.btnBrowsePathMinData.Size = new System.Drawing.Size(25, 25);
            this.btnBrowsePathMinData.TabIndex = 6;
            this.btnBrowsePathMinData.Text = "...";
            this.btnBrowsePathMinData.UseVisualStyleBackColor = true;
            this.btnBrowsePathMinData.Click += new System.EventHandler(this.btnBrowsePathMinData_Click);
            // 
            // btnBrowsePathTemp
            // 
            this.btnBrowsePathTemp.Location = new System.Drawing.Point(215, 31);
            this.btnBrowsePathTemp.Name = "btnBrowsePathTemp";
            this.btnBrowsePathTemp.Size = new System.Drawing.Size(25, 25);
            this.btnBrowsePathTemp.TabIndex = 5;
            this.btnBrowsePathTemp.Text = "...";
            this.btnBrowsePathTemp.UseVisualStyleBackColor = true;
            this.btnBrowsePathTemp.Click += new System.EventHandler(this.btnBrowsePathTemp_Click);
            // 
            // chkSaveTempFile
            // 
            this.chkSaveTempFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chkSaveTempFile.AutoSize = true;
            this.chkSaveTempFile.Checked = true;
            this.chkSaveTempFile.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSaveTempFile.Location = new System.Drawing.Point(19, 107);
            this.chkSaveTempFile.Name = "chkSaveTempFile";
            this.chkSaveTempFile.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkSaveTempFile.Size = new System.Drawing.Size(117, 17);
            this.chkSaveTempFile.TabIndex = 4;
            this.chkSaveTempFile.Text = "Save temp data file";
            this.chkSaveTempFile.UseVisualStyleBackColor = true;
            // 
            // txtMinDataFolder
            // 
            this.txtMinDataFolder.Location = new System.Drawing.Point(19, 78);
            this.txtMinDataFolder.Name = "txtMinDataFolder";
            this.txtMinDataFolder.Size = new System.Drawing.Size(192, 20);
            this.txtMinDataFolder.TabIndex = 3;
            this.txtMinDataFolder.Text = "C:\\www\\stockbd\\data\\";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(7, 62);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 13);
            this.label10.TabIndex = 2;
            this.label10.Text = "Data folder:";
            // 
            // txtTempFolder
            // 
            this.txtTempFolder.Location = new System.Drawing.Point(19, 35);
            this.txtTempFolder.Name = "txtTempFolder";
            this.txtTempFolder.Size = new System.Drawing.Size(192, 20);
            this.txtTempFolder.TabIndex = 1;
            this.txtTempFolder.Text = "C:\\www\\stockbd\\tmp\\";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 18);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Temp file folder:";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnPathDB);
            this.groupBox6.Controls.Add(this.btnUpdateFunda);
            this.groupBox6.Controls.Add(this.btnUpdateTicker);
            this.groupBox6.Controls.Add(this.txtDBPath);
            this.groupBox6.Controls.Add(this.label11);
            this.groupBox6.Location = new System.Drawing.Point(654, 143);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(243, 88);
            this.groupBox6.TabIndex = 17;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Manual action";
            // 
            // btnPathDB
            // 
            this.btnPathDB.Location = new System.Drawing.Point(211, 13);
            this.btnPathDB.Name = "btnPathDB";
            this.btnPathDB.Size = new System.Drawing.Size(25, 25);
            this.btnPathDB.TabIndex = 7;
            this.btnPathDB.Text = "...";
            this.btnPathDB.UseVisualStyleBackColor = true;
            this.btnPathDB.Click += new System.EventHandler(this.btnPathDB_Click);
            // 
            // btnUpdateFunda
            // 
            this.btnUpdateFunda.Location = new System.Drawing.Point(128, 45);
            this.btnUpdateFunda.Name = "btnUpdateFunda";
            this.btnUpdateFunda.Size = new System.Drawing.Size(80, 37);
            this.btnUpdateFunda.TabIndex = 3;
            this.btnUpdateFunda.Text = "Update Funda";
            this.btnUpdateFunda.UseVisualStyleBackColor = true;
            this.btnUpdateFunda.Click += new System.EventHandler(this.btnUpdateFunda_Click);
            // 
            // btnUpdateTicker
            // 
            this.btnUpdateTicker.Enabled = false;
            this.btnUpdateTicker.Location = new System.Drawing.Point(31, 45);
            this.btnUpdateTicker.Name = "btnUpdateTicker";
            this.btnUpdateTicker.Size = new System.Drawing.Size(80, 37);
            this.btnUpdateTicker.TabIndex = 2;
            this.btnUpdateTicker.Text = "Update Ticker";
            this.btnUpdateTicker.UseVisualStyleBackColor = true;
            this.btnUpdateTicker.Click += new System.EventHandler(this.btnUpdateTicker_Click);
            // 
            // txtDBPath
            // 
            this.txtDBPath.Location = new System.Drawing.Point(67, 16);
            this.txtDBPath.Name = "txtDBPath";
            this.txtDBPath.Size = new System.Drawing.Size(141, 20);
            this.txtDBPath.TabIndex = 1;
            this.txtDBPath.Text = "C:\\www\\stockbd\\db\\StockBD.s3db";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(12, 20);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(53, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "DB Path: ";
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDlg";
            this.openFileDlg.Title = "Locade Database";
            // 
            // bkgndWorker
            // 
            this.bkgndWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgndWorker_DoWork);
            this.bkgndWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bkgndWorker_ProgressChanged);
            this.bkgndWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bkgndWorker_RunWorkerCompleted);
            // 
            // lblLive
            // 
            this.lblLive.BackColor = System.Drawing.Color.Gray;
            this.lblLive.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLive.Location = new System.Drawing.Point(211, 184);
            this.lblLive.Name = "lblLive";
            this.lblLive.Size = new System.Drawing.Size(96, 40);
            this.lblLive.TabIndex = 18;
            this.lblLive.Text = "????";
            this.lblLive.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(909, 428);
            this.Controls.Add(this.lblLive);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.lstErrors);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.lstMessages);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnStartStop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "frmMainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Stock BD Admin";
            this.Load += new System.EventHandler(this.frmMainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStartStop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtTimeStep;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radMinDataSourceSQ;
        private System.Windows.Forms.RadioButton radMinDataSourceEPL;
        private System.Windows.Forms.RadioButton radMinDataSourceDSE;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker tmEndTime;
        private System.Windows.Forms.DateTimePicker tmStartTime;
        private System.Windows.Forms.TextBox txtNumRetry;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker tmEODHitTime;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkThursDay;
        private System.Windows.Forms.CheckBox chkWedDay;
        private System.Windows.Forms.CheckBox chkTuesDay;
        private System.Windows.Forms.CheckBox chkMonDay;
        private System.Windows.Forms.CheckBox chkSunDay;
        private System.Windows.Forms.Timer timerTradingHourDay;
        private System.Windows.Forms.Timer timerMinGrabber;
        private System.Windows.Forms.Timer timerEODGrabber;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ListView lstMessages;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colMessage;
        private System.Windows.Forms.RadioButton radMinDataSourceUTFD;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox chkMarch;
        private System.Windows.Forms.CheckBox chkFebruary;
        private System.Windows.Forms.CheckBox chkJanuary;
        private System.Windows.Forms.TextBox txtFundaDay;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.CheckBox chkDecember;
        private System.Windows.Forms.CheckBox chkNovember;
        private System.Windows.Forms.CheckBox chkOctober;
        private System.Windows.Forms.CheckBox chkSeptember;
        private System.Windows.Forms.CheckBox chkAugust;
        private System.Windows.Forms.CheckBox chkJuly;
        private System.Windows.Forms.CheckBox chkJune;
        private System.Windows.Forms.CheckBox chkMay;
        private System.Windows.Forms.CheckBox chkApril;
        private System.Windows.Forms.ListView lstErrors;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox chkSaveTempFile;
        private System.Windows.Forms.TextBox txtMinDataFolder;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtTempFolder;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnUpdateFunda;
        private System.Windows.Forms.Button btnUpdateTicker;
        private System.Windows.Forms.TextBox txtDBPath;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnBrowsePathMinData;
        private System.Windows.Forms.Button btnBrowsePathTemp;
        private System.Windows.Forms.Button btnPathDB;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDlg;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.ComponentModel.BackgroundWorker bkgndWorker;
        private System.Windows.Forms.Button btnGetEODNow;
        private System.Windows.Forms.Label lblLive;
    }
}

