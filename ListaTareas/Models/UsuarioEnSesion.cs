using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaTareas.Models
{
    public class UsuarioEnSesion
    {
        public String Nombre { get; }
        public String Id { get; }
        public UsuarioEnSesion(String _nombre, String _id)
        {
            this.Nombre = _nombre;
            this.Id = _id;
        }
    }
}