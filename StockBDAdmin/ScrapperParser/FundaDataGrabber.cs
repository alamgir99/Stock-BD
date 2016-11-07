/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : FundaData.cs
 * Purpose: Grabs fundamental data from DSE website and stores in Database.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using System.ComponentModel;


namespace ScrapperParser
{
    //grabs fundamental data from DSE site
    public class FundaDataGrabber
    {
        private static string srcLink = @"http://dsebd.org/displayCompany.php?name=";
        private static SQLiteConnection m_dbConnection;
        private static List<SectorInfo> m_Sectors;
        private static List<string> m_Tickers;
        private static List<string> m_cID;

        BackgroundWorker m_Sender;

        public FundaDataGrabber(string dbConString, object sender)
        {
            m_dbConnection = new SQLiteConnection(dbConString);
            m_dbConnection.Open();

            m_Sectors = new List<SectorInfo>();

            //read in the sector list
            string sql = @"SELECT sID, LongName FROM Sector ORDER BY sID ASC";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while(reader.Read())
            {
                int sid = int.Parse(reader["sID"].ToString());
                string sname = reader["LongName"].ToString();
                m_Sectors.Add(new SectorInfo(){sID =sid , LongName=sname});
            }

            m_Tickers = new List<string>();
            m_cID = new List<string>();
            sql = @"SELECT cID, Ticker FROM Company WHERE ActiveListing=1 ORDER BY Ticker ASC";
            command = new SQLiteCommand(sql, m_dbConnection);
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                m_cID.Add(reader["cID"].ToString());
                m_Tickers.Add(reader["Ticker"].ToString());
            }

            //close the connection
            m_dbConnection.Close();

            m_Sender = sender as BackgroundWorker;
        }
 

        public void GrabAndSave()
        {
            int i = 0;
            int tot = m_Tickers.Count;
            float progress = 0.0f;

            foreach(string ticker in m_Tickers)
                {
                    //Console.WriteLine("Processing {0}", ticker);
                    WebRequest webReq = WebRequest.Create(srcLink + ticker);

                    using (WebResponse response = webReq.GetResponse())
                    {
                        // Get the data stream that is associated with the specified URL.
                        using (Stream responseStream = response.GetResponseStream())
                        {
                            // Read the bytes in responseStream and copy them to content.  
                            //get the downloaded string
                            var strmReader = new StreamReader(responseStream);
                            string rxBuffer = strmReader.ReadToEnd();

                            FundaData fData = ProcessFunda(rxBuffer, ticker, m_cID[i]);
                            StoreFunda(fData);
                        }
                    }
                    i++;
                    //update progress
                  progress = 100*(i+1.0f)/tot;
                  if (m_Sender.CancellationPending == true)
                       break;
                  else
                    m_Sender.ReportProgress((int)progress);
                } // foreach
       } // GrabAndSave

        //store a block of funda data to the dB
        private void StoreFunda(FundaData fData)
        {
            m_dbConnection.Open();
            using (SQLiteCommand command = m_dbConnection.CreateCommand())
            {
                //basic info
                if (fData.compInfoValid) // valid company info
                {

                    string query = @"UPDATE Company SET sID="+fData.sID + ", AuthCapital="+ fData.authCapital + ", PaidupCapital="+fData.paidupCapital + ", ShareCount="+fData.shareCount +
                        ", FaceValue="+ fData.faceValue + ", LotSize="+ fData.lotSize + ", ListYear='"+fData.listingYear +  "', ShareType=" + (int)fData.shareType +
                        " WHERE Ticker='"+fData.ticker+"'";

                    command.CommandText = query;
                    command.ExecuteNonQuery();
                } // comp info
                
                if (fData.holdingInfoValid)
                {
                    string query = @"UPDATE Holding SET ChangeYear='" + fData.changeYear + "', SponDir=" + fData.sponDir + ", Gov=" + fData.gov + ", Inst=" + fData.inst + ", Forgn=" + fData.foreign +
                                    ", Public="+fData.pub+", ShareCount=" +  fData.shareCount + 
                                    @" WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker+"')";
    
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }//holdingInfo

                if (fData.audFinValid) // audited financials
                {
                    string query = "UPDATE AudFinance SET LoanShortTerm=" + fData.LoanShort + ", LoanLongTerm=" + fData.LoanLong + 
                                    @" WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker + "')";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    for (int i = 0; i < fData.finYear.Count(); i++ )
                    {
                        query = @"INSERT OR REPLACE INTO AudFinance(cID, Year, EPS_Orig, EPS_Restated, NAV_Orig, NAV_Restated) VALUES(" +
                                fData.cID + ",'" + fData.finYear[i] + "'," + fData.EPS_Orig[i] + "," + fData.EPS_Restated[i] + "," + fData.NAV_Orig[i] + "," + fData.NAV_Restated[i] + ")";
                                //@"WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker+"'";
                        command.CommandText = query;
                        command.ExecuteNonQuery();

                    }
                }// aud financials

                if (fData.intFinValid) //interim financials
                {
                    string query = "";
                    if (fData.shareType == ShareType.STOCK)
                    {
                        query = @"INSERT OR REPLACE INTO EPS_Perf(cID, Year, Q1, Q2, Half_Yearly, Q3, Annual) VALUES(" +
                                        fData.cID + ",'" + fData.intermYear + "'," + fData.EPS_Q1 + "," + fData.EPS_Q2 + "," + fData.EPS_HalfYearly + "," + fData.EPS_Q3 + "," + fData.EPS_Annual + ")";                                     
                                       // @" WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker + "'";
                    }
                    else 
                    {
                        
                        //query = @"INSERT OR REPLACE INTO MF_Perf(Year, Q1, Q2, Half_Yearly, Q3, Annual) VALUES(" +
                        //                fData.intermYear + "," + fData.EPS_Q1 + "," + fData.EPS_Q2 + "," + fData.EPS_HalfYearly + "," + fData.EPS_Q3 + "," + fData.EPS_Annual + ")" +
                        //                @" WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker + "'";

                        
                        //TODO
                        query = @"INSERT OR REPLACE INTO MF_Perf(cID, Year, Quarter, NAV_Market, NAV_Cost) VALUES(" +
                                fData.cID + ",'" + fData.intermYear + "'," + 1 + "," + 0 + "," + 0 + ")";
                                //@" WHERE cID=(SELECT cID FROM Company WHERE Ticker='" + fData.ticker + "'";
                         
                    }
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
            }
            m_dbConnection.Close();
        }
        //remove a tag
        private static string StripStartTags(string item)
        {
            // Determine whether a tag begins the string.
            if (item.Trim().StartsWith("<"))
            {
                // Find the closing tag.
                int lastLocation = item.IndexOf(">");
                // Remove the tag.
                if (lastLocation >= 0)
                {
                    item = item.Substring(lastLocation + 1);

                    // Remove any additional starting tags.
                    item = StripStartTags(item);
                }
            }

            return item;
        }

        // convert to float
        private static float FloatParse(string str)
        {
            if (str == @"-" || str =="n/a")
                return 0.0f;
            else
                return float.Parse(str);

        }
        //convert a string to security type
        private ShareType StringToShareType(string sT)
        {
            if (sT.Equals ("Equity", StringComparison.OrdinalIgnoreCase))
                return ShareType.STOCK;
            if (sT.Equals ("Mutual Fund", StringComparison.OrdinalIgnoreCase))
                return ShareType.MF;
            if (sT.Equals ("Corporate Bond", StringComparison.OrdinalIgnoreCase))
                return ShareType.BOND;

            return ShareType.STOCK; // default
        }

        //convert sector string to code
        private static int StringToSectorId(string sector)
        {
            foreach(var sec in m_Sectors) {
                if (sec.LongName.Equals(sector, StringComparison.OrdinalIgnoreCase))
                    return sec.sID;
            }
            return 0; //default sector
        }

        //given a buffer full of text with company info
        //parse and fill the Funda structure
        private FundaData ProcessFunda(string strComp, string ticker, string cid)
        {
            FundaData aData = new FundaData();
            aData.ticker = ticker;
            aData.cID = cid;

            int startAt, endAt;
            // ------------------------------------------------------------------------------- //
            // basic information
            startAt = strComp.IndexOf(@"Basic Information", System.StringComparison.CurrentCultureIgnoreCase);
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string basicInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length+1);
       
            // Creates an HtmlDocument object from an URL
            HtmlDocument document = new HtmlDocument();
            //load the given html string
            document.LoadHtml(basicInfo);

            //get all the rows
            IEnumerable<HtmlNode> allRows = document.DocumentNode.SelectNodes("//tr");
            int rowIndex = 0;
            HtmlNodeCollection allTds;
            foreach (HtmlNode row in allRows)
            {
                // Extracts all links within that node
                allTds = row.SelectNodes("td");
                switch(rowIndex) 
                {
                    case 0:
                        aData.authCapital = FloatParse(allTds[1].InnerText.Trim().Replace(",", string.Empty));
                        break;
                    case 1:
                        aData.paidupCapital = FloatParse(allTds[1].InnerText.Trim().Replace(",", string.Empty));
                        break;
                    case 2:
                        aData.faceValue = FloatParse(allTds[1].InnerText.Trim().Replace(",", string.Empty));
                        break;
                    case 3:
                        aData.shareCount = int.Parse(allTds[1].InnerText.Trim().Replace(",", string.Empty));
                        break;
                    default: 
                        break;
                }
                rowIndex++;
                if (rowIndex == 4) break;
            }

            //second/right hand side table
            startAt = strComp.IndexOf(@"<table", startAt + @"<table".Length, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            basicInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length + 1);
            //load the given html string
            document.LoadHtml(basicInfo);

            //get all the rows
             allRows = document.DocumentNode.SelectNodes("//tr");
             rowIndex = 0;
  
            foreach (HtmlNode row in allRows)
            {
                // Extracts all links within that node
                allTds = row.SelectNodes("td");
                switch (rowIndex)
                {
                    case 0:
                        break;
                    case 1: // type of security
                        aData.shareType = StringToShareType(allTds[1].InnerText.Trim());
                        break;
                    case 2: // lot
                        aData.lotSize = int.Parse(allTds[1].InnerText.Trim().Replace(",", string.Empty));
                        break;
                    case 3: // sector
                        aData.sID = StringToSectorId(allTds[1].InnerText.Trim());
                        break;
                    default:
                        break;
                }
                rowIndex++;
                if (rowIndex == 4) break;
            }

            aData.compInfoValid = true;
            //----------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------
            // interim financials
            startAt = strComp.IndexOf(@"Interim Financial Performance:", System.StringComparison.CurrentCultureIgnoreCase);
            if(startAt == -1)
                return aData;
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string interimInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length+1);
       
            //load the given html string
            document.LoadHtml(interimInfo);

            //get all the rows
            //allRows = document.DocumentNode.SelectNodes("//tr";
            HtmlNode selRow = document.DocumentNode.SelectSingleNode("//tr[10]");

            // Extracts all links within that node
            allTds = selRow.SelectNodes("td");

            aData.EPS_Q1 = FloatParse(allTds[1].InnerText.Trim());
            aData.EPS_Q2 = FloatParse(allTds[2].InnerText.Trim()); 

            string[] data = allTds[3].InnerHtml.Trim().Split(new string[] {"<span>"}, StringSplitOptions.RemoveEmptyEntries);
            aData.EPS_HalfYearly = FloatParse(StripStartTags(data[0]).Trim());

            aData.EPS_Q3 = FloatParse(allTds[4].InnerText.Trim());

            data = allTds[6].InnerText.Trim().Split(new string[] {"<span>"}, StringSplitOptions.RemoveEmptyEntries);
            aData.EPS_Annual = FloatParse(StripStartTags(data[0]).Trim()); //float.Parse(data[0].Trim()); 

            aData.intermYear = DateTime.Now; //.Year.ToString();
            aData.intFinValid = true;
            //---------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------
            // Financial Performance as per Audited
            startAt = strComp.IndexOf(@"Financial Performance as per Audited", System.StringComparison.CurrentCultureIgnoreCase);
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string auditedInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length + 1);

            //load the given html string
            document.LoadHtml(auditedInfo);

            //get all the rows
            allRows = document.DocumentNode.SelectNodes("//tr");
            rowIndex = 0;
            int rowCount = allRows.Count() - 3;
            aData.finYear = new DateTime[rowCount];
            aData.EPS_Orig = new float[rowCount];
            aData.EPS_Restated = new float[rowCount];
            aData.NAV_Orig = new float[rowCount];
            aData.NAV_Restated = new float[rowCount];

            foreach( var row in allRows) 
            {   
                rowIndex ++;
                if(rowIndex == 1 || rowIndex == 2 || rowIndex == 3) // skip first 3 rows
                   continue;

                // Extracts all links within that node
                allTds = row.SelectNodes("td");

                aData.finYear[rowIndex-4] = Convert.ToDateTime(allTds[0].InnerText.Trim() + "-01-01");
                aData.EPS_Orig[rowIndex - 4] = FloatParse(allTds[4].InnerText.Trim());
                aData.EPS_Restated[rowIndex - 4] = FloatParse(allTds[5].InnerText.Trim());
                aData.NAV_Orig[rowIndex - 4] = FloatParse(allTds[7].InnerText.Trim());
                aData.NAV_Restated[rowIndex - 4] = FloatParse(allTds[8].InnerText.Trim());             
                 }

            aData.audFinValid = true;
            //-------------------------------------------------------------------------------------

            //--------------------------------------------------------------------------------------
            //other information
            startAt = strComp.IndexOf(@"Other Information of the Company", System.StringComparison.CurrentCultureIgnoreCase);
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string otherInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length + 1);

    
            //load the given html string
            document.LoadHtml(otherInfo);

            //get all the rows
             allRows = document.DocumentNode.SelectNodes("//tr");
             rowIndex = 0;
            foreach (HtmlNode row in allRows)
            {
                // Extracts all links within that node
                 allTds = row.SelectNodes("td");
                switch (rowIndex)
                {
                    case 0:
                        aData.listingYear = Convert.ToDateTime(allTds[1].InnerText.Trim()+"-01-01");
                        break;
                    case 1:
                        aData.shareCat = FundaData.GetCat(allTds[1].InnerText.Trim());
                        break;
                    default:
                        break;
                }
                rowIndex++;
                if (rowIndex == 2) break;
            }
            //----------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------
            //holding info
            int found = strComp.IndexOf(@"Share Holding", System.StringComparison.CurrentCultureIgnoreCase);
            while(found > -1) 
            {
                startAt = found;
                found = strComp.IndexOf(@"Share Holding", startAt + 10, System.StringComparison.CurrentCultureIgnoreCase);
            }
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string holdingInfo = strComp.Substring(startAt, endAt - startAt + @"</table".Length + 1);


            //load the given html string
            document.LoadHtml(holdingInfo);

            //get all the rows
            allRows = document.DocumentNode.SelectNodes("//tr");
            rowIndex = 0;
            
            foreach (HtmlNode row in allRows)
            {
                // Extracts all links within that node
                 allTds = row.SelectNodes("td");
                for (int col = 0; col < 5; col++)
                {
                    string tempHolding = allTds[col].InnerText.Trim(); //InnerHtml.Trim();
                    string[] holdings = tempHolding.Split(new string[] { ":" }, StringSplitOptions.RemoveEmptyEntries); //"\r\n" 

                    switch (col)
                    {
                        case 0:
                            aData.sponDir = FloatParse(holdings[1].Trim());
                            break;
                        case 1:
                            aData.gov = FloatParse(holdings[1].Trim());
                            break;
                        case 2:
                            aData.inst = FloatParse(holdings[1].Trim());
                            break;
                        case 3:
                            aData.foreign = FloatParse(holdings[1].Trim());
                            break;
                        case 4:
                            aData.pub = FloatParse(holdings[1].Trim());
                            break;
                        default:
                            break;
                    }
                }
                break; // only one row
            } // for row
            aData.holdingInfoValid = true;
            //---------------------------------------------------------------------------------------

            //----------------------------------------------------------------------------------------
            //corporate performance
            startAt = strComp.IndexOf(@"Corporate Performance", System.StringComparison.CurrentCultureIgnoreCase);
            startAt = strComp.IndexOf(@"<table", startAt, System.StringComparison.CurrentCultureIgnoreCase);
            endAt = strComp.IndexOf(@"</table", startAt, System.StringComparison.CurrentCultureIgnoreCase);

            string corpPerf = strComp.Substring(startAt, endAt - startAt + @"</table".Length + 1);

    
            //load the given html string
            document.LoadHtml(corpPerf);

            //get all the rows
             allRows = document.DocumentNode.SelectNodes("//tr");
             rowIndex = 0;
            foreach (HtmlNode row in allRows)
            {
                if (rowIndex == 0 || rowIndex == 1)
                {
                    rowIndex++;
                    continue;
                }
                // Extracts all links within that node
                 allTds = row.SelectNodes("td");
                switch (rowIndex)
                {
                    case 2:
                        aData.LoanShort = FloatParse(allTds[2].InnerText.Trim());
                        break;
                    case 3:
                        aData.LoanLong = FloatParse(allTds[1].InnerText.Trim());
                        break;
                    default:
                        break;
                }
                rowIndex++;
                if (rowIndex == 4) break;
            }

            return aData;
        }
    }
}
