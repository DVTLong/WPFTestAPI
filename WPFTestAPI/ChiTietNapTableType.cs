using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPFTestAPI
{
    [UserDefinedTableType("ChiTietNapTableType")]
    public class ChiTietNapTableType
    {
        [UserDefinedTableTypeColumn(1)]
        public string MaLT { get; set; }

        [UserDefinedTableTypeColumn(2)]
        public int SoLuongTo { get; set; }
    }
}