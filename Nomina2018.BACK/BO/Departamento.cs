using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomina2018.BACK.BO
{
    public class Departamento
    {
        private int idDepartamento;
        private string nombre;
        private string estatus;

        public int IdDepartamento
        {
            get { return idDepartamento; }
            set{ idDepartamento = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
    }
}
