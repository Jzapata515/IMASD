using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nomina2018.BACK.BO
{
    public class Tabulador
    {
        private int idTabulador;
        private string nombre;
        private int idPuesto;
        private decimal limiteInferior;
        private decimal limiteSuperior;
        private string estatus;

        public int IdTabulador
        {
            get { return idTabulador; }
            set { idTabulador = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public int IdPuesto
        {
            get { return idPuesto; }
            set { idPuesto = value; }
        }

        public decimal LimiteInferior
        {
            get { return limiteInferior; }
            set { limiteInferior = value; }
        }

        public decimal LimiteSuperior
        {
            get { return limiteSuperior; }
            set { limiteSuperior = value; }
        }

        public string Estatus
        {
            get { return estatus; }
            set { estatus = value; }
        }
    }
}
