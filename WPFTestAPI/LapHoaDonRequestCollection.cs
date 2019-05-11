using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestAPI
{
    public class LapHoaDonRequestCollection
    {
        public string Token { get; set; }
        public string MaKIOSK { get; set; }
        public string MaSSC { get; set; }
        public int MaHTTT { get; set; }
        public List<CTHDTableType> CTHDTableType { get; set; }

    }
}
