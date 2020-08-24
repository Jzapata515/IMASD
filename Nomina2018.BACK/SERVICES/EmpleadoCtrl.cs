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
    public class EmpleadoCtrl
    {
        public DataSet Retrieve(Empleado empleado, Cadena cadena)
        {
            EmpleadoRetHlp dao = new EmpleadoRetHlp();
            DataSet ds = dao.Action(empleado, cadena);
            return ds;
        }

        public void Insert(Empleado empleado, Cadena cadena)
        {
            EmpleadoInsHlp dao = new EmpleadoInsHlp();
            dao.Action(empleado, cadena);
        }

        public void Update(Empleado empleado, Cadena cadena)
        {
            EmpleadoUpdHlp dao = new EmpleadoUpdHlp();
            dao.Action(empleado, cadena);
        }

        public void Delete(Empleado empleado, Cadena cadena)
        {
            EmpleadoDelHlp dao = new EmpleadoDelHlp();
            dao.Action(empleado, cadena);
        }
        
        public Empleado LastDataRowToData(DataSet dsempleados)
        {
            if (!dsempleados.Tables.Contains("EMPLEADOS"))
                throw new Exception("EmpleadosCtrl: DataSet no tiene la tabla EMPLEADOS");
            int index = dsempleados.Tables["EMPLEADOS"].Rows.Count;
            if (index < 1)
                return null;
            return this.DataRowToData(dsempleados.Tables["EMPLEADOS"].Rows[index - 1]);
        }

        public Empleado DataRowToData(DataRow drempleado)
        {
            Empleado empleado = new Empleado();

            if (!drempleado.IsNull("IdEmpleado"))
                empleado.IdEmpleado = (Int32)Convert.ChangeType(drempleado["IdEmpleado"], typeof(Int32));

            if (!drempleado.IsNull("Nombres"))
                empleado.Nombres = (string)Convert.ChangeType(drempleado["Nombres"], typeof(string));
            else
                empleado.Nombres = null;

            if (!drempleado.IsNull("ApellidoP"))
                empleado.ApellidoP = (string)Convert.ChangeType(drempleado["ApellidoP"], typeof(string));
            else
                empleado.ApellidoP = null;

            if (!drempleado.IsNull("ApellidoM"))
                empleado.ApellidoM = (string)Convert.ChangeType(drempleado["ApellidoM"], typeof(string));
            else
                empleado.ApellidoM = null;

            if (!drempleado.IsNull("Direccion"))
                empleado.Direccion = (string)Convert.ChangeType(drempleado["Direccion"], typeof(string));
            else
                empleado.Direccion = null;

            if (!drempleado.IsNull("Telefono"))
                empleado.Telefono = (string)Convert.ChangeType(drempleado["Telefono"], typeof(string));
            else
                empleado.Telefono = null;

            if (!drempleado.IsNull("Email"))
                empleado.Email = (string)Convert.ChangeType(drempleado["Email"], typeof(string));
            else
                empleado.Email = null;

            if (!drempleado.IsNull("Clabe"))
                empleado.Clabe = (string)Convert.ChangeType(drempleado["Clabe"], typeof(string));
            else
                empleado.Clabe = null;

            if (!drempleado.IsNull("SalarioDiario"))
                empleado.SalarioDiario = (decimal)Convert.ChangeType(drempleado["SalarioDiario"], typeof(decimal));
            else
                empleado.SalarioDiario = 0;

            if (!drempleado.IsNull("IdPuesto"))
            {
                empleado.Puesto = new Puesto();
                empleado.Puesto.IdPuesto = (Int32)Convert.ChangeType(drempleado["IdPuesto"], typeof(Int32));
                empleado.Puesto.Nombre = (string)Convert.ChangeType(drempleado["Puesto"], typeof(string));
            }
            else
                empleado.Puesto = null;

            if (!drempleado.IsNull("IdDepartamento"))
            {
                empleado.Departamento = new Departamento();
                empleado.Departamento.IdDepartamento = (Int32)Convert.ChangeType(drempleado["IdDepartamento"], typeof(Int32));
                empleado.Departamento.Nombre = (string)Convert.ChangeType(drempleado["Departamento"], typeof(string));
            }
            else
                empleado.Departamento = null;

            if (!drempleado.IsNull("IdPeriodoPago"))
            {
                empleado.PeriodoPago = new PeriodoPago();
                empleado.PeriodoPago.IdPeriodoPago = (Int32)Convert.ChangeType(drempleado["IdPeriodoPago"], typeof(Int32));
                empleado.PeriodoPago.Nombre = (string)Convert.ChangeType(drempleado["PeriodoPago"], typeof(string));
            }
            else
                empleado.PeriodoPago = null;

            if (!drempleado.IsNull("IdTipoPago"))
            {
                empleado.TipoPago = new TipoPago();
                empleado.TipoPago.IdTipoPago = (Int32)Convert.ChangeType(drempleado["IdTipoPago"], typeof(Int32));
                empleado.TipoPago.Nombre = (string)Convert.ChangeType(drempleado["TipoPago"], typeof(string));
            }
            else
                empleado.TipoPago = null;

            if (!drempleado.IsNull("Estatus"))
                empleado.Estatus = (string)Convert.ChangeType(drempleado["Estatus"], typeof(string));
            else
                empleado.Estatus = null;

            return empleado;
        }
    }
}
