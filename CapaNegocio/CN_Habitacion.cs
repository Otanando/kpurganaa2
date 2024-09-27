
using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
namespace CapaNegocio
{
    public class CN_Habitacion
    {


        private CD_Habitaciones objCapaDato = new CD_Habitaciones();

        public List<Habitacion> listar()
        {
            return objCapaDato.listar();
        }

        public int Registrar(Habitacion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {

                Mensaje = "el nombre de la  habitacion no puede estar vacio";

            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {

                Mensaje = "La descripcion del tipo de habitacion no puede estar vacio";

            }
  
            else if (obj.oTipoHabitacion.IdTipoHabitacion == 0) 
            {
                Mensaje = "Debe seleccionar un tipo";
            }
            else if (obj.Precio  == 0)
            {
                Mensaje = "Debe ingresasr el precio del producto ";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresasr el stock del producto ";
            }

            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Registrar(obj, out Mensaje);
            }
            else
            {
                return 0;
            }
        }

        public bool Editar(Habitacion obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {

                Mensaje = "el nombre de la  habitacion no puede estar vacio";

            }

            else if (string.IsNullOrEmpty(obj.Descripcion) || string.IsNullOrWhiteSpace(obj.Descripcion))
            {

                Mensaje = "La descripcion del tipo de habitacion no puede estar vacio";

            }

            else if (obj.oTipoHabitacion.IdTipoHabitacion == 0)
            {
                Mensaje = "Debe seleccionar un tipo";
            }
            else if (obj.Precio == 0)
            {
                Mensaje = "Debe ingresasr el precio del producto ";
            }
            else if (obj.Stock == 0)
            {
                Mensaje = "Debe ingresasr el stock del producto ";
            }


            if (string.IsNullOrEmpty(Mensaje))
            {
                return objCapaDato.Editar(obj, out Mensaje);
            } else
            {
                return false;
            }
        }


        public bool GuardarDatosImagen(Habitacion obj, out string Mensaje)
        {
            return objCapaDato.GuardarDatosImagen(obj, out Mensaje);


        }


        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);
        }

    }
}




