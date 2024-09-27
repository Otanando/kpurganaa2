﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DetalleVenta
    {
        public int IdDetalleVenta { get; set; }
        public int IdVenta { get; set; }
        public int IdHabitacion { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }

        public virtual Venta Venta { get; set; }
        public virtual Habitacion Habitacion { get; set; }
    }


}
