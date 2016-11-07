using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StockBDWeb.Models
{
    public static class Helper
    {
     //empty directory helper
        public static void Empty(this System.IO.DirectoryInfo directory)
         {
        foreach (System.IO.FileInfo file in directory.GetFiles()) file.Delete();
        foreach (System.IO.DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
    }
}
}

