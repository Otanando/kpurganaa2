﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;


namespace CapaNegocio
{ 
    public class CN_Reporte
    {
        private CD_Reporte  objCapaDato = new CD_Reporte();

        public DashBoard verDashboard()
        {
            return objCapaDato.verDashboard();
        }

    }
}
