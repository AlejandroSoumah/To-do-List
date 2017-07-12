using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ListaTareas.Models;

namespace ListaTareas.Models
{
    public class DaoAccesos
    {
        private static ContextoTareasBDDataContext ctx = new ContextoTareasBDDataContext();

        public UsuarioEnSesion Comprobar(String user, String pwd)
        {
            var usuario = (from u in ctx.Usuarios
                           where u.NombreUsuario.Equals(user) &&
                                 u.ClaveAcceso.Equals(pwd)
                           select new { USER = u.NombreUsuario,
                                        ID = u.ID_Credencial.ToString() }).FirstOrDefault();
            if (usuario!=null)
            {
                UsuarioEnSesion us = new UsuarioEnSesion(usuario.USER, usuario.ID);
                return us;
            }
            else
            {
                return null;
            }
        }


        public List<Tareas> GetTareas(String userId)
        {
            List<Tareas> tareas = new List<Tareas>();
            var results = (from t in ctx.Tareas
                           where t.Fk_Usuario.ToString().Equals(userId)
                           select new { ID_USUARIO = t.Fk_Usuario,
                               TAREA = t.NombreTarea,
                               DESCRIPCION = t.DescripcionTarea,
                               ID_TAREA = t.Id_Tarea,
                               PRIORIDAD = t.PrioridadTarea
                           }).ToList();

            foreach(var aux in results)
            {
                Tareas currentTask = new Tareas();
                currentTask.Fk_Usuario = aux.ID_USUARIO;
                currentTask.Id_Tarea = aux.ID_TAREA;
                currentTask.NombreTarea = aux.TAREA;
                currentTask.DescripcionTarea = aux.DESCRIPCION;
                currentTask.PrioridadTarea = aux.PRIORIDAD;

                tareas.Add(currentTask);
            }
            return tareas;
        }

        public int InsertarNuevoUsuario(Usuarios newUser)
        {
            try
            {
                newUser.ID_Credencial = Guid.NewGuid();
                newUser.FechaAlta = DateTime.UtcNow;
                newUser.EmailUsuario = newUser.EmailUsuario.ToLower();
                newUser.Tareas = new System.Data.Linq.EntitySet<Tareas>();
                ctx.Usuarios.InsertOnSubmit(newUser);
                ctx.SubmitChanges();
           }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

        public int InsertarNuevaTarea(Tareas newTask)
        {
            try
            {
                newTask.Id_Tarea = Guid.NewGuid();
                ctx.Tareas.InsertOnSubmit(newTask);
                ctx.SubmitChanges();
            }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

        public Tareas GetTareaById(String id)
        {
            Tareas tarea = (from t in ctx.Tareas
                           where t.Id_Tarea.ToString().Equals(id)
                           select t).FirstOrDefault<Tareas>();

            return tarea;
        }

        public int ModificarTarea(Tareas modifyTask)
        {
            try
            {
                Tareas tareaBD = this.GetTareaById(modifyTask.Id_Tarea.ToString());
                tareaBD.Fk_Usuario = modifyTask.Fk_Usuario;
                tareaBD.NombreTarea = modifyTask.NombreTarea;
                tareaBD.DescripcionTarea = modifyTask.DescripcionTarea;
                tareaBD.PrioridadTarea = modifyTask.PrioridadTarea;

                ctx.SubmitChanges();
            }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

        public int DeleteTarea(String id)
        {
            try
            {
                Tareas tareaBD = this.GetTareaById(id);
                ctx.Tareas.DeleteOnSubmit(tareaBD);
                
                ctx.SubmitChanges();
            }
            catch (Exception)
            {
                return 1;
            }
            return 0;
        }

    }
}