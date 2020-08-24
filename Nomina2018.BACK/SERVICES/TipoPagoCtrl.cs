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
    public class TipoPagoCtrl
    {
        public DataSet Retrieve(TipoPago tipoPago, Cadena cadena)
        {
            TipoPagoRetHlp dao = new TipoPagoRetHlp();
            DataSet ds = dao.Action(tipoPago, cadena);
            return ds;
        }

    }
}
