﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using CapaEntidad;
using CapaNegocio;
using Microsoft.Ajax.Utilities;
namespace kpurganaaAdmin.Controllers

{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Usuarios()
        {
            return View();
        }
        [HttpGet]
        public JsonResult listarUsuarios()
        {
            List<Usuario> oLista = new List<Usuario>();


            oLista = new CN_Usuarios().listar();

            return Json( new { data = oLista} , JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult GuardarUsuario(Usuario objeto) {
            object resultado;
            string mensaje = string.Empty;

            if (objeto.IdUsuario == 0) {
                resultado = new CN_Usuarios().Registrar(objeto, out mensaje); 

            } else {
                resultado = new CN_Usuarios().Editar(objeto, out mensaje);
            }

            return Json(new { resultado = resultado, mensaje = mensaje}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EliminarUsuario(int id)
        {
            bool respuesta = false;
            string mensaje = string.Empty;

            respuesta = new CN_Usuarios().Eliminar(id, out mensaje);

            return Json(new { resultado = respuesta, mensaje = mensaje }, JsonRequestBehavior.AllowGet);


        }


        [HttpGet]
        public JsonResult VistaDashBoard()
        {
            DashBoard objeto = new CN_Reporte().verDashboard();

            return Json(new { resultado = objeto }, JsonRequestBehavior.AllowGet);
        }



    }
}

