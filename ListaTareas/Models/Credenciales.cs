using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ListaTareas.Models
{
    public class Credenciales
    {
        public string Usuario {get; set;}
        public string Password { get; set; }
        public Credenciales()
        {
        }
        public Credenciales(string User,string pwd)
        {
            this.Usuario = User;
            this.Password = pwd;
        }
    }
}