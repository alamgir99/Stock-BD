/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : FundaData.cs
 * Purpose: Class definition for fundamental data.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScrapperParser
{
    public enum ShareType
    {
        STOCK = 0,
        MF = 1,
        BOND = 2
    };

    public enum ShareCat
    {
        A = 1,
        B = 2,
        N = 3,
        Z = 4
    }
    //this class represents the fundamental data
    //of a ticker
    public class FundaData
    {
        public string  ticker {get;set;} // to match identity
        public string cID { get; set; }
        public float authCapital { get; set; } // authorized capital
        public float paidupCapital { get; set; } // paid up capital
        public long shareCount { get; set; } // how many shares
        public float faceValue { get; set; }   // face value
        public int lotSize { get; set; }  // lot size
        public int sID { get; set; } // sector ID
        public DateTime listingYear { get; set; } // when the stock was listed
        public ShareType shareType { get; set; } // type of share 
        public ShareCat shareCat { get; set; } // caregory A/B/N/Z
        public bool compInfoValid; // flag indicating comp info is valid and should be updated
        //share holding info
        public DateTime changeYear { get; set; } // when the Share holding info was updated
        public float sponDir { get; set; } // sponsor director
        public float gov { get; set; } // gov share
        public float inst { get; set; } // institution
        public float foreign { get; set; }
        public float pub { get; set; }
        public bool holdingInfoValid; // holding info is valid

        //audited finance
        public DateTime[] finYear { get; set; }
        public float[] EPS_Orig { get; set; }
        public float[] EPS_Restated { get; set; }
        public float[] PE_Basic { get; set; }
        public float[] PE_Diluted { get; set; }
        public float[] BonusCash { get; set; }
        public float[] BonusStock { get; set; }
        public float[] Yield { get; set; }
        public float[] NAV_Orig { get; set; }
        public float[] NAV_Restated { get; set; }
        public float LoanShort { get; set; }
        public float LoanLong { get; set; }
        public bool audFinValid; // audited financial info is valid
        //interim finance
        public DateTime intermYear;
        public float EPS_Q1 { get; set; }
        public float EPS_Q2 { get; set; }
        public float EPS_Q3 { get; set; }
        //public float EPS_Q4 { get; set; }
        public float EPS_Annual { get; set; }
        public float EPS_HalfYearly { get; set; }
        public bool intFinValid; //interim financial info is valid

        public FundaData()
        {
            authCapital = 0.0f; paidupCapital = 0.0f; shareCount = 0;
            faceValue = 10.0f; lotSize = 100; shareType = ShareType.STOCK;
            shareCat = ShareCat.A;
            compInfoValid = false;

            sponDir = 0.0f; gov = 0.0f; inst = 0.0f; foreign = 0.0f; pub = 0.0f;
            holdingInfoValid = false;
            /*
            finYear = DateTime.Now;
            EPS_Orig = 0.0f; EPS_Restated = 0.0f;
            PE_Basic = 0.0f; PE_Diluted = 0.0f;
            BonusCash = 0.0f; BonusStock = 0.0f;
            NAV_Orig = 0.0f; NAV_Restated = 0.0f;
            LoanShort= 0.0f; LoanShort = 0.0f;
            */
            audFinValid = false;

            EPS_Q1 = 0.0f; EPS_Q2 = 0.0f; EPS_Q3 = 0.0f; //EPS_Q4 = 0.0f;
            EPS_Annual = 0.0f; EPS_HalfYearly = 0.0f;
            intFinValid = false;
        }

        public static ShareCat GetCat(string str)
        {
            if (str == @"A")
                return ShareCat.A;
            if (str == @"B")
                return ShareCat.B;
            if (str == @"N")
                return ShareCat.N;
            if (str == @"Z")
                return ShareCat.Z;

            return ShareCat.A;
        }

    }

    //this class represents minimum company info
    public class MinCompInfo
    {
        public string Ticker { get; set; }
        public string Name { get; set; }
    }

    //this class represents minimum sector info
    public class SectorInfo
    {
        public int sID { get; set; }
        public string LongName { get; set; }
    }
}
