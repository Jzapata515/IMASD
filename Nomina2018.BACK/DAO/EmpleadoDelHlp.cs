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
    class EmpleadoDelHlp
    {
        public void Action(Empleado empleado, Cadena cadena)
        {
            SqlConnection conn = new SqlConnection(cadena.conexion);
            string sError = String.Empty;
            if (empleado == null)
                sError += ", Empleado";
            if (sError.Length > 0)
                throw new Exception("EmpleadoDelHlp: Los siguientes campos no pueden ser vacios: " + sError.Substring(2));
            DbCommand sqlCmd = conn.CreateCommand();
            try
            {
                conn.Open();
            }
            catch { throw new Exception("EmpleadoDelHlp: No se pudo conectar a la base de datos"); }
            DbParameter sqlParam;
            StringBuilder sCmd = new StringBuilder();
            sCmd.Append(" EXEC EMPLEADOSDEL @IdEmpleado ");
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdEmpleado";
            sqlParam.Value = 0;
            if (empleado.IdEmpleado > 0)
                sqlParam.Value = empleado.IdEmpleado;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

            try
            {
                sqlCmd.CommandText = sCmd.ToString();
                sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string exmsg = ex.Message;
                try { conn.Close(); }
                catch { ; }
                throw new Exception("EmpleadoDelHlp: Se encontraron problemas al actualizar los datos. ");
            }
            finally
            {
                try { conn.Close(); }
                catch { ; }
            }            
        }
    }
}
