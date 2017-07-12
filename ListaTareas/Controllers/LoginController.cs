using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ListaTareas.Models;
namespace ListaTareas.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComprobarCredenciales(Credenciales cred)
        {
            DaoAccesos dao = new DaoAccesos();
            UsuarioEnSesion userInSession = dao.Comprobar(cred.Usuario, cred.Password);
            if(userInSession != null)
            {
                Session["NOMBRE USUARIO"] = userInSession.Nombre;
                Session["ID USUARIO"] = userInSession.Id;
                return RedirectToAction("ListaTareas");
            }
            return View("Index",cred);
        }


        public ActionResult ListaTareas()
        {
            List<Tareas> listado = new List<Tareas>();
            DaoAccesos dao = new DaoAccesos();
            if (Session["ID USUARIO"] != null)
            {
                String userId = Session["ID USUARIO"].ToString();
                listado = dao.GetTareas(userId);
            }

            return View(listado);
        }

        public ActionResult Registrarse()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrarse(Usuarios newUser)
        {
            DaoAccesos dao = new DaoAccesos();
            int resultado = dao.InsertarNuevoUsuario(newUser);
            if(resultado == 0)
            {
                return RedirectToAction("Index");
            }
            ViewBag.MensajeError = "ERROR: No se pudo completar la solicitud de registro!";
            return View("Error");
        }

        public ActionResult Create()
        {
            Tareas modelo = new Tareas();
            modelo.Fk_Usuario = new Guid(Session["ID USUARIO"].ToString());
            modelo.Id_Tarea = Guid.NewGuid();
            return View(modelo);
        }

        [HttpPost]
        public ActionResult Create(Tareas modelo)
        {
            DaoAccesos dao = new DaoAccesos();
            modelo.Fk_Usuario = new Guid(Session["ID USUARIO"].ToString());
            dao.InsertarNuevaTarea(modelo);
            return RedirectToAction("ListaTareas");
        }

        public ActionResult Edit(string id)
        {
            DaoAccesos dao = new DaoAccesos();
            Tareas myTarea = dao.GetTareaById(id);
            return View(myTarea);
        }


        [HttpPost]
        public ActionResult Edit(string id, Tareas taskToModify)
        {
            DaoAccesos dao = new DaoAccesos();
            dao.ModificarTarea(taskToModify);
            return RedirectToAction("ListaTareas");
        }

        public ActionResult Delete(string id)
        {
            DaoAccesos dao = new DaoAccesos();
            dao.DeleteTarea(id);
            return RedirectToAction("ListaTareas");
        }


    }

}