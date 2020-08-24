using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomina2018.BACK.BO
{
    public class Empleado
    {
        private int idEmpleado;
        private string nombres;
        private string apellidoP;
        private string apellidoM;
        private string direccion;
        private string telefono;
        private string email;
        private string clabe;
        private decimal salarioDiario;
        private Puesto puesto;
        private Departamento departamento;
        private PeriodoPago periodoPago;
        private TipoPago tipoPago;
        private string estatus;

        public int IdEmpleado
        {
            get { return idEmpleado; }
            set { idEmpleado = value; }
        }

        public string Nombres
        {
            get { return nombres; }
            set { nombres = value; }
        }

        public string ApellidoP
        {
            get { return apellidoP; }
            set { apellidoP = value; }
        }

        public string ApellidoM
        {
            get { return apellidoM; }
            set { apellidoM = value; }
        }

        public string NombreCompleto
        {
            get { return nombres + " " + apellidoP + " " + apellidoM; }
        }

        public string Direccion
        {
            get { return direccion; }
            set { direccion = value; }
        }

        public string Telefono
        {
            get { return telefono; }
            set { telefono = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Clabe
        {
            get { return clabe; }
            set { clabe = value; }
        }

        public decimal SalarioDiario
        {
            get { return salarioDiario; }
            set { salarioDiario = value; }
        }

        public Puesto Puesto
        {
            get { return puesto; }
            set { puesto = value; }
        }

        public Departamento Departamento
        {
            get { return departamento; }
            set { departamento = value; }
        }

        public PeriodoPago PeriodoPago
        {
            get { return periodoPago; }
            set { periodoPago = value; }
        }

        public TipoPago TipoPago
        {
            get { return tipoPago; }
            set { tipoPago = value; }
        }

        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
    }
}
