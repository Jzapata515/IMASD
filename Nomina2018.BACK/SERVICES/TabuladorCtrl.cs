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
    public class TabuladorCtrl
    {
        public DataSet Retrieve(Tabulador tabulador, Cadena cadena)
        {
            TabuladorRetHlp dao = new TabuladorRetHlp();
            DataSet ds = dao.Action(tabulador, cadena);
            return ds;
        }

        public Tabulador LastDataRowToData(DataSet dstabuladores)
        {
            if (!dstabuladores.Tables.Contains("TABULADORES"))
                throw new Exception("TabuladorCtrl: DataSet no tiene la tabla TABULADORES");
            int index = dstabuladores.Tables["TABULADORES"].Rows.Count;
            if (index < 1)
                return null;
            return this.DataRowToData(dstabuladores.Tables["TABULADORES"].Rows[index - 1]);
        }

        public Tabulador DataRowToData(DataRow drtabulador)
        {
            Tabulador tabulador = new Tabulador();

            if (!drtabulador.IsNull("IdTabulador"))
                tabulador.IdTabulador = (Int32)Convert.ChangeType(drtabulador["IdTabulador"], typeof(Int32));

            if (!drtabulador.IsNull("Nombre"))
                tabulador.Nombre = (string)Convert.ChangeType(drtabulador["Nombre"], typeof(string));
            else
                tabulador.Nombre = null;
                     

            if (!drtabulador.IsNull("IdPuesto"))
                tabulador.IdPuesto = (Int32)Convert.ChangeType(drtabulador["IdPuesto"], typeof(Int32));
            else
                tabulador.IdPuesto = 0;

            if (!drtabulador.IsNull("LimiteInferior"))
                tabulador.LimiteInferior = (decimal)Convert.ChangeType(drtabulador["LimiteInferior"], typeof(decimal));
            else
                tabulador.LimiteInferior = 0;

            if (!drtabulador.IsNull("LimiteSuperior"))
                tabulador.LimiteSuperior = (decimal)Convert.ChangeType(drtabulador["LimiteSuperior"], typeof(decimal));
            else
                tabulador.LimiteSuperior = 0;

            if (!drtabulador.IsNull("Estatus"))
                tabulador.Estatus = (string)Convert.ChangeType(drtabulador["Estatus"], typeof(string));
            else
                tabulador.Estatus = null;

            return tabulador;
        }

    }
}
