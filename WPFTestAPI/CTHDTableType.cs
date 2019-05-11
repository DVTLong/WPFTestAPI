using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityFrameworkExtras.EF6;

namespace WPFTestAPI
{
    [UserDefinedTableType("CTHDTableType")]
    public class CTHDTableType
    {
        [UserDefinedTableTypeColumn(1)]
        public int MaMH { get; set; }

        [UserDefinedTableTypeColumn(2)]
        public int SoLuong { get; set; }

        [UserDefinedTableTypeColumn(3)]
        public decimal DonGiaBan { get; set; }

        [UserDefinedTableTypeColumn(4)]
        public bool MangVe { get; set; }
    }
}
