using CRUD_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD_MVC.Controllers
{
    public class EstatusController : Controller
    {

        // GET: Estatus
        private DataCursos DCursos = new DataCursos();
        public ActionResult Index()
        {
            return View(DCursos.ConsultarLst());
        }
        public ActionResult Eliminar(int? id,string nombre,string clave)
        {
            ViewBag.id = id;
            ViewBag.nombre = nombre;
            ViewBag.clave = clave;
            return View();
        }
        public void CEliminar(int id)
        {
          
            DCursos.EliminarEstado(id);
            Response.Redirect("~/Estatus/Index");
        }

        public ActionResult crear()
        {
           
            return View();
        }
        public void crearEstatus(string nombre,string clave)
        {
            DCursos.AgregarEstatus(clave,nombre);
            Response.Redirect("~/Estatus/Index");
        }
        public void volver()
        {
            Response.Redirect("~/Estatus/Index");
        }
        public ActionResult Actualizar(int id, string nombre, string clave)
        {
            ViewBag.id = id;
            ViewBag.nombre = nombre;
            ViewBag.clave = clave;
            return View();
        }
        public void CActualizar(int id, string nombre, string clave)
        {
            DCursos.ModificarEstatus(id,nombre,clave);
            Response.Redirect("~/Estatus/Index");
        }
    }
}