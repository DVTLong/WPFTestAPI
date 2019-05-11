using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WPFTestAPI
{
    public class NapTienRequestCollection
    {
        public string Token { get; set; }
        public string MaKIOSK { get; set; }
        public string MaSSC { get; set; }
        public List<ChiTietNapTableType> ChiTietNapTableType { get; set; }
    }
}