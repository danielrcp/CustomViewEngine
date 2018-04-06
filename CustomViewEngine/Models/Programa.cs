using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomViewEngine.Models
{
    public class Programa
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal UnidadesStock { get; set; }
    }
}