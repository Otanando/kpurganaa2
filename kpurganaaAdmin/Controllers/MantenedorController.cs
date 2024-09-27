using CapaEntidad;
using CapaNegocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;
using System.Globalization;
using System.Configuration;
using System.IO;
using Newtonsoft.Json.Linq;
namespace kpurganaaAdmin.Controllers
{
    public class MantenedorController : Controller
    {
        // GET: Mantenerdor
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Habitaciones()
        {
            return View();
        }

        public ActionResult TipoHabitacion()
        {
            return View();
        }

        [HttpGet]
        public JsonResult ListarTipoHabitacion()
        {
            List<Tipohabitacion> oLista = new List<Tipohabitacion>();


            oLista = new CN_TipoHabitacion().listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarTipoHabitacion(Tipohabitacion objeto)
        {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdTipoHabitacion == 0)
            {
                resultado = new CN_TipoHabitacion().Registrar(objeto, out mensaje);

            }
            else
            {
                resultado = new CN_TipoHabitacion().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult EliminarTipoHabitacion(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_TipoHabitacion().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }

       
        public JsonResult ListarHabitacion()
        {
            List<Habitacion> oLista = new List<Habitacion>();


            oLista = new CN_Habitacion().listar();
            return Json(new { data = oLista }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GuardarHabitacion(string objeto, HttpPostedFileBase archivoImagen)
        {
            
            string mensaje = string.Empty;
            bool operacion_Exitosa = true;
            bool guardar_imagen_Exito = true;

            Habitacion oHabitacion = new Habitacion();
            oHabitacion = JsonConvert.DeserializeObject<Habitacion>(objeto);

            decimal precio;
            if (decimal.TryParse(oHabitacion.PrecioTexto, NumberStyles.AllowDecimalPoint, new CultureInfo("es-CO"), out precio))
            {
                oHabitacion.Precio = precio;
            }
            else
            {

                return Json(new { operacionExitosa = false, mensaje = "El formato del precio debe ser ##.##" }, JsonRequestBehavior.AllowGet);

            }
            
            if (oHabitacion.IdHabitacion == 0)
            {
                //oHabitacion.IdTipoHabitacion = oHabitacion.TipoHabitacion.IdTipoHabitacion;
                int idHabitacionGenerado = new CN_Habitacion().Registrar(oHabitacion, out mensaje);
                if (idHabitacionGenerado != 0)
                {
                    oHabitacion.IdHabitacion = idHabitacionGenerado;
                }
                else
                {
                    operacion_Exitosa = false;
                }
            } else {
                operacion_Exitosa = new CN_Habitacion().Editar(oHabitacion, out mensaje);
            }

            if (operacion_Exitosa) {
                if (archivoImagen != null) {

                    string ruta_guardar = ConfigurationManager.AppSettings["ServidorFotos"];
                    string extension = Path.GetExtension(archivoImagen.FileName);
                    string nombre_imagen = string.Concat(oHabitacion.IdHabitacion.ToString(), extension);


                    try
                    {

                        archivoImagen.SaveAs(Path.Combine(ruta_guardar, nombre_imagen));
                    }
                    catch (Exception ex)
                    {

                       
                        guardar_imagen_Exito = false;
                        string msg = ex.Message;
                    }


                    if (guardar_imagen_Exito)
                    {
                        oHabitacion.RutaImagen = ruta_guardar;
                        oHabitacion.NombreImagen = nombre_imagen;
                        bool rspta = new CN_Habitacion().GuardarDatosImagen(oHabitacion, out mensaje);

                    }
                    else
                    {

                        mensaje = "Se guardo el producto pero hubo problemas con la imagen";
                    }
                }


                //return Json(new { operacionExitosa = operacion_Exitosa, idGenerado = oHabitacion, mensaje = mensaje }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { operacionExitosa = operacion_Exitosa, idGenerado = oHabitacion, mensaje = mensaje }, JsonRequestBehavior.AllowGet);

        }


        [HttpPost]

        public JsonResult ImagenHabitacion(int id)
        {

            bool conversion;
            Habitacion oHabitacion = new CN_Habitacion().listar().Where(H => H.IdHabitacion == id).FirstOrDefault();
            string textoBase64 = CN_Recursos.ConvertirBase64(Path.Combine(oHabitacion.RutaImagen, oHabitacion.NombreImagen), out conversion);

            return Json(new
            {
                conversion = conversion,
                textoBase64 = textoBase64,
                Extension = Path.GetExtension(oHabitacion.NombreImagen)


            },
            JsonRequestBehavior.AllowGet
            );




            
        }


        [HttpPost]

        public JsonResult EliminarHabitacion(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Habitacion().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


    }

}