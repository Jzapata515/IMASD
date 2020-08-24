﻿using System;
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
    public class DepartamentoCtrl
    {
        public DataSet Retrieve(Departamento departamento, Cadena cadena)
        {
            DepartamentoRetHlp dao = new DepartamentoRetHlp();
            DataSet ds = dao.Action(departamento, cadena);
            return ds;
        }

    }
}
