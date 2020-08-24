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
    class PuestoRetHlp
    {
        public DataSet Action(Puesto puesto, Cadena cadena)
        {
            SqlConnection conn = new SqlConnection(cadena.conexion);
            string sError = String.Empty;
            if (puesto == null)
                sError += ", Puesto";
            if (sError.Length > 0)
                throw new Exception("PuestoRetHlp: Los siguientes campos no pueden ser vacios: " + sError.Substring(2));
            DbCommand sqlCmd = conn.CreateCommand();
            try
            {
                conn.Open();
            }
            catch { throw new Exception("PuestoRetHlp: No se pudo conectar a la base de datos"); }
            DbParameter sqlParam;
            StringBuilder sCmd = new StringBuilder();
            sCmd.Append(" EXEC PUESTOSRET @IdPuesto, @Nombre, @IdDepartamento, @Estatus");
            
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdPuesto";
            sqlParam.Value = 0;
            if (puesto.IdPuesto > 0)
                sqlParam.Value = puesto.IdPuesto;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);
            
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Nombre";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(puesto.Nombre))
                sqlParam.Value = puesto.Nombre;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdDepartamento";
            sqlParam.Value = 0;
            if (puesto.IdDepartamento > 0)
                sqlParam.Value = puesto.IdDepartamento;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Estatus";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(puesto.Estatus))
                sqlParam.Value = puesto.Estatus;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            DataSet ds = new DataSet();
            DbDataAdapter sqlAdapter = new SqlDataAdapter();
            sqlAdapter.SelectCommand = sqlCmd;
            try
            {
                sqlCmd.CommandText = sCmd.ToString();
                sqlAdapter.Fill(ds, "PUESTOS");
            }
            catch (Exception ex)
            {
                string exmsg = ex.Message;
                try { conn.Close(); }
                catch { ; }
                throw new Exception("PuestoRetHlp: Se encontraron problemas al recuperar los datos. ");
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
