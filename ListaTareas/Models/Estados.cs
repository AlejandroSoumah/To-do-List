using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaTareas.Models
{
    public class Estados
    {
        public int IdEstado { get; set; }
        public String Estado { get; set; }

        public Estados(int id, string name)
        {
            this.IdEstado = id;
            this.Estado = name;
        }

    }
}