/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : DataTable.cs
 * Purpose: Helper class to easily parse a data table.
 * Author : Alagmir Mohammed
 * Copyright: All rights reserverd. Private property of the copyrights holder.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace ScrapperParser
{
    //this is a helper class that aids in processing
    //HTML table in a given string
    public class DataTable
    {
        //strTable -> a HTML table full of data
        //selCols  -> an array of 6 numbers indicating the columns to pick on
        public static List<MinuteData> ProcessHTMLTable(string strTable, int[] selCols)
        {
            List<MinuteData> data = new List<MinuteData>();
            int rowIndex;
            MinuteData aData;

            // Creates an HtmlDocument object from an URL
            HtmlDocument document = new HtmlDocument();
            //load the given html string
            document.LoadHtml(strTable);

            //get all the rows
            IEnumerable<HtmlNode> allRows = document.DocumentNode.SelectNodes("//tr");
            rowIndex = 0;
            foreach (HtmlNode row in allRows)
            {
                rowIndex++;
                if (rowIndex == 1) continue;

                // Extracts all links within that node
                HtmlNodeCollection allTds = row.SelectNodes("td");

                aData.Ticker = allTds[selCols[0]].InnerText.Trim();
                aData.Open = allTds[selCols[1]].InnerText.Trim().Replace(",", string.Empty);
                aData.High = allTds[selCols[2]].InnerText.Trim().Replace(",", string.Empty);
                aData.Low = allTds[selCols[3]].InnerText.Trim().Replace(",", string.Empty);
                aData.Close = allTds[selCols[4]].InnerText.Trim().Replace(",", string.Empty);
                aData.Volume = allTds[selCols[5]].InnerText.Trim().Replace(",", string.Empty); // remove thousand separator comma
                
                data.Add(aData);
            } // end of foreach tr
            //return the data collected
            return data;
        }
    }
}
