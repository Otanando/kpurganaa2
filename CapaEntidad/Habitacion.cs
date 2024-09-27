using System;

namespace CapaEntidad
{
    public class Habitacion
    {
        public int IdHabitacion { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int IdTipoHabitacion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }

        public string PrecioTexto { get; set; }
        public string RutaImagen { get; set; }
        public string NombreImagen { get; set; }
        public bool Activo { get; set; }
        public bool Base64 { get; set; }
        public bool Extension { get; set; }

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public virtual Tipohabitacion oTipoHabitacion { get; set; } // Cambiado aquí
    }
}
