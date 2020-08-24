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
    class EmpleadoInsHlp
    {
        public void Action(Empleado empleado, Cadena cadena)
        {
            SqlConnection conn = new SqlConnection(cadena.conexion);
            string sError = String.Empty;
            if (empleado == null)
                sError += ", Empleado";
            if (empleado.PeriodoPago == null)
                sError += ", PeriodoPago";
            if (empleado.TipoPago == null)
                sError += ", TipoPago";
            if (sError.Length > 0)
                throw new Exception("EmpleadoInsHlp: Los siguientes campos no pueden ser vacios: " + sError.Substring(2));
            DbCommand sqlCmd = conn.CreateCommand();
            try
            {
                conn.Open();
            }
            catch { throw new Exception("EmpleadoInsHlp: No se pudo conectar a la base de datos"); }
            DbParameter sqlParam;
            StringBuilder sCmd = new StringBuilder();
            sCmd.Append(" EXEC EMPLEADOSINS @Nombres, @ApellidoP, @ApellidoM, @Direccion, @Telefono, @Email, @Clabe, @IdPeriodoPago, @IdTipoPago");
            
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Nombres";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.Nombres))
                sqlParam.Value = empleado.Nombres;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "ApellidoP";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.ApellidoP))
                sqlParam.Value = empleado.ApellidoP;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "ApellidoM";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.ApellidoM))
                sqlParam.Value = empleado.ApellidoM;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Direccion";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.Direccion))
                sqlParam.Value = empleado.Direccion;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Telefono";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.Telefono))
                sqlParam.Value = empleado.Telefono;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Email";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.Email))
                sqlParam.Value = empleado.Email;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "Clabe";
            sqlParam.Value = "";
            if (!string.IsNullOrEmpty(empleado.Clabe))
                sqlParam.Value = empleado.Clabe;
            sqlParam.DbType = DbType.String;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdPeriodoPago";
            sqlParam.Value = 0;
            if (empleado.PeriodoPago != null && empleado.PeriodoPago.IdPeriodoPago > 0)
                sqlParam.Value = empleado.PeriodoPago.IdPeriodoPago;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdTipoPago";
            sqlParam.Value = 0;
            if (empleado.TipoPago != null && empleado.TipoPago.IdTipoPago > 0)
                sqlParam.Value = empleado.TipoPago.IdTipoPago;
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
                throw new Exception("EmpleadoInsHlp: Se encontraron problemas al insertar los datos. ");
            }
            finally
            {
                try { conn.Close(); }
                catch { ; }
            }
        }
    }
}
