using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nomina2018.BACK.BO;
using Nomina2018.BACK.CONEXION;

namespace Nomina2018.BACK.DAO
{
    class TipoPagoRetHlp
    {
        public DataSet Action(TipoPago tipoPago, Cadena cadena)
        {
            SqlConnection conn = new SqlConnection(cadena.conexion);
            string sError = String.Empty;
            if (tipoPago == null)
                sError += ", TipoPago";
            if (sError.Length > 0)
                throw new Exception("TipoPagoRetHlp: Los siguientes campos no pueden ser vacios: " + sError.Substring(2));
            DbCommand sqlCmd = conn.CreateCommand();
            try
            {
                conn.Open();
            }
            catch { throw new Exception("TipoPagoRetHlp: No se pudo conectar a la base de datos"); }
            DbParameter sqlParam;
            StringBuilder sCmd = new StringBuilder();
            sCmd.Append(" EXEC TIPOSPAGORET @IdTipoPago, @Nombre, @Estatus");
            
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdTipoPago";
            sqlParam.Value = 0;
            if (tipoPago.IdTipoPago > 0)
                sqlParam.Value = tipoPago.IdTipoPago;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);
            
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Nombre";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(tipoPago.Nombre))
                sqlParam.Value = tipoPago.Nombre;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);
      
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Estatus";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(tipoPago.Estatus))
                sqlParam.Value = tipoPago.Estatus;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            DataSet ds = new DataSet();
            DbDataAdapter sqlAdapter = new SqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            try
            {
                sqlCmd.CommandText = sCmd.ToString();
                sqlAdapter.Fill(ds, "TIPOSPAGO");
            }
            catch (Exception ex)
            {
                string exmsg = ex.Message;
                try { conn.Close(); }
                catch { ; }
                throw new Exception("TipoPagoRetHlp: Se encontraron problemas al recuperar los datos. ");
            }
            finally
            {
                try { conn.Close(); }
                catch { ; }
            }
            return ds;
        }
    }
}
