using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;
namespace CapaNegocio
{
    public class CN_Usuarios
    {

        private CD_Usuarios objCapaDato = new CD_Usuarios();

        public List<Usuario> listar()
        {
            return objCapaDato.listar();
        }



        public int Registrar(Usuario obj, out string Mensaje) {


             Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre)) {

                Mensaje = "El nombre del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos)) {

                Mensaje = "El apellido no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {

                Mensaje = "El Correo no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "creacion de cuenta";
                string mensaje_correo = "<h3> Su cuenta fue creada correectamente</h3> </br> <p>b Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);


                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);


                }
                else
                {
                    Mensaje = "No se puede enviar correo";
                    return 0;

                }
                 


            } else
            {

                return 0;
            }






        }public int Registrar(Usuario obj, out string Mensaje) {


             Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre)) {

                Mensaje = "El nombre del usuario no puede ser vacio";

            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos)) {

                Mensaje = "El apellido no puede ser vacio";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {

                Mensaje = "El Correo no puede ser vacio";
            }
            if (string.IsNullOrEmpty(Mensaje))
            {

                string clave = CN_Recursos.GenerarClave();

                string asunto = "creacion de cuenta";
                string mensaje_correo = "<h3> Su cuenta fue creada correectamente</h3> </br> <p>b Su contraseña para acceder es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", clave);


                bool respuesta = CN_Recursos.EnviarCorreo(obj.Correo, asunto, mensaje_correo);

                if (respuesta)
                {

                    obj.Clave = CN_Recursos.ConvertirSha256(clave);
                    return objCapaDato.Registrar(obj, out Mensaje);


                }
                else
                {
                    Mensaje = "No se puede enviar correo";
                    return 0;

                }
                 


            } else
            {

                return 0;
            }






        }

        public bool Editar(Usuario obj, out string Mensaje) {
            Mensaje = string.Empty;

            if (string.IsNullOrEmpty(obj.Nombre) || string.IsNullOrWhiteSpace(obj.Nombre))
            {
                Mensaje = "El Nombre del usuario es inválido.";
            }
            else if (string.IsNullOrEmpty(obj.Apellidos) || string.IsNullOrWhiteSpace(obj.Apellidos))
            {
                Mensaje = "El apellido no puede ser vacío.";
            }
            else if (string.IsNullOrEmpty(obj.Correo) || string.IsNullOrWhiteSpace(obj.Correo))
            {
                Mensaje = "El correo no puede ser vacío.";
            }
            else
            {
                Mensaje = null;
            }

            if (string.IsNullOrEmpty(Mensaje)) {



              

                return objCapaDato.Editar(obj, out Mensaje);

            } else {
                return false;
            }
        }

        public bool Eliminar(int id, out string Mensaje)
        {
            return objCapaDato.Eliminar(id, out Mensaje);

        }




        public bool cambiarClave(int idusuario, string nuevaclave, out string Mensaje)
        {
            return objCapaDato.cambiarClave(idusuario, nuevaclave, out Mensaje);

        }



        public bool ReestablecerClave(int idusuario,  string correo, out string Mensaje)
        {


            Mensaje = string.Empty;
            string nuevaclave = CN_Recursos.GenerarClave();
            bool resultado = objCapaDato.ReestablecerClave(idusuario, CN_Recursos.ConvertirSha256(nuevaclave),out Mensaje);

            if (resultado)
            {
                string asunto = "Contraseña reestablecida";
                string mensaje_correo = "<h3> Su cuenta fue reestablecida correectamente</h3> </br> <p>b Su contraseña para acceder ahora es: !clave!</p>";
                mensaje_correo = mensaje_correo.Replace("!clave!", nuevaclave);
                bool respuesta = CN_Recursos.EnviarCorreo(correo, asunto, mensaje_correo);


                if (respuesta)
                {

                    return true;



                }
                else
                {
                    Mensaje = "No se puedo enviar el correo ";

                    return false;

                }



            }
            else
            {

                Mensaje = "No se puedo Reestablecer la copntraseña ";
                return false;

                
            }



        }



    }



}
