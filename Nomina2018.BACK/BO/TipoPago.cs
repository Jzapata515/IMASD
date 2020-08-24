using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomina2018.BACK.BO
{
    public class TipoPago
    {
        private int idTipoPago;
        private string nombre;
        private string estatus;

        public int IdTipoPago
        {
            get { return idTipoPago; }
            set{ idTipoPago = value; }
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
