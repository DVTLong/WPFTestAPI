using EntityFrameworkExtras.EF6;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFTestAPI
{
    [StoredProcedure("sp_LapHoaDon")]
    public class SP_LapHoaDon
    {
        [StoredProcedureParameter(SqlDbType.Date, ParameterName = "ngaylap")]
        public DateTime NgayLap { get; set; }

        [StoredProcedureParameter(SqlDbType.Time, ParameterName = "giolap")]
        public TimeSpan GioLap { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "massc")]
        public string MaSSC { get; set; }

        [StoredProcedureParameter(SqlDbType.VarChar, ParameterName = "mako")]
        public string MaKO { get; set; }

        [StoredProcedureParameter(SqlDbType.Int, ParameterName = "mahttt")]
        public int MaHTTT { get; set; }

        [StoredProcedureParameter(SqlDbType.Udt, ParameterName = "cthd")]
        public List<CTHDTableType> CTHDTableTypes { get; set; }
    }
}
