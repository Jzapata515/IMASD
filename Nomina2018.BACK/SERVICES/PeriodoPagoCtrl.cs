using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nomina2018.BACK.BO;
using Nomina2018.BACK.CONEXION;
using Nomina2018.BACK.DAO;

namespace Nomina2018.BACK.SERVICES
{
    public class PeriodoPagoCtrl
    {
        public DataSet Retrieve(PeriodoPago periodoPago, Cadena cadena)
        {
            PeriodoPagoRetHlp dao = new PeriodoPagoRetHlp();
            DataSet ds = dao.Action(periodoPago, cadena);
            return ds;
        }

    }
}
