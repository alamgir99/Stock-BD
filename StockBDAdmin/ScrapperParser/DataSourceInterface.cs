/**
 * Project: StockBD Admin
 * Module : ScrapperParser 
 * 
 * File   : DataSourceInterface.cs
 * Purpose: Defines the interface for a data grabber
 *          Any data grabber should implement this interface.
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
    //define the interface that every data source grabber should implement
    public interface DataSourceInterface
    {
         void GrabAndSave(int sId, bool saveTemp);  // grabs the actual data
         int LinkTime();   // measures the link time
    }

}
