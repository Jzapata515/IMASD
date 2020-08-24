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
    public class PuestoCtrl
    {
        public DataSet Retrieve(Puesto puesto, Cadena cadena)
        {
            PuestoRetHlp dao = new PuestoRetHlp();
            DataSet ds = dao.Action(puesto, cadena);
            return ds;
        }

    }
}
