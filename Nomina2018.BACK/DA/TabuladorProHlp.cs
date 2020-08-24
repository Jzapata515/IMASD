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

namespace Nomina2018.BACK.DA
{
    public class TabuladorProHlp
    {
        public static void Action(Tabulador tabulador, Empleado empleado,Cadena cadena)
        {
            SqlConnection conn = new SqlConnection(cadena.conexion);
            string sError = String.Empty;
            if (empleado == null)
                sError += ", Empleado";
            if (tabulador == null)
                sError += ", Tabulador";
            if (sError.Length > 0)
                throw new Exception("TabuladorProHlp: Los siguientes campos no pueden ser vacios: " + sError.Substring(2));
            DbCommand sqlCmd = conn.CreateCommand();
            try
            {
                conn.Open();
            }
            catch { throw new Exception("TabuladorProHlp: No se pudo conectar a la base de datos"); }
            DbParameter sqlParam;
            StringBuilder sCmd = new StringBuilder();
            sCmd.Append(" EXEC TABULADORESPRO @IdEmpleado, @SalarioDiario, @IdPuesto, @IdDepartamento, @IdTabulador");
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdEmpleado";
            sqlParam.Value = 0;
            if (empleado.IdEmpleado > 0)
                sqlParam.Value = empleado.IdEmpleado;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

           
            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "SalarioDiario";
            sqlParam.Value = 0;
            if (empleado.SalarioDiario > 0)
                sqlParam.Value = empleado.SalarioDiario;
            sqlParam.DbType = DbType.Decimal;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdPuesto";
            sqlParam.Value = 0;
            if (empleado.Puesto != null && empleado.Puesto.IdPuesto > 0)
                sqlParam.Value = empleado.Puesto.IdPuesto;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdDepartamento";
            sqlParam.Value = 0;
            if (empleado.Departamento != null && empleado.Departamento.IdDepartamento > 0)
                sqlParam.Value = empleado.Departamento.IdDepartamento;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);

            sqlParam = sqlCmd.CreateParameter();
            sqlParam.ParameterName = "IdTabulador";
            sqlParam.Value = 0;
            if (tabulador.IdTabulador > 0)
                sqlParam.Value = tabulador.IdTabulador;
            sqlParam.DbType = DbType.Int32;
            sqlCmd.Parameters.Add(sqlParam);
            int iRes = 0;
            try
            {
                sqlCmd.CommandText = sCmd.ToString();
                iRes = sqlCmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string exmsg = ex.Message;
                try { conn.Close(); }
                catch { ; }
                throw new Exception("TabuladorProHlp: Se encontraron problemas al actualizar los datos. ");
            }
            finally
            {
                try { conn.Close(); }
                catch { ; }
            }
            if (iRes < 1)
                throw new Exception("TabuladorProHlp: Ocurrió un error al actualizar el registro.");
        }
    }
}
