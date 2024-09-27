using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public int IdCliente { get; set; }
        public int IdHabitacion { get; set; }
        public int Cantidad { get; set; }

        public virtual Usuario Usuario { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Habitacion Habitacion { get; set; }
    }

}
