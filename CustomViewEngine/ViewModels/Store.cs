using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CustomViewEngine.ViewModels
{
    public class Store
    {
        public Store()
        {
            Programas = new List<Models.Programa>
            {
                new Models.Programa
                {
                    Id = 1,
                    Nombre = "Diabetes",
                    PrecioUnitario = 20,
                    UnidadesStock =1
                },
                new Models.Programa
                {
                    Id = 2,
                    Nombre = "Obesidad",
                    PrecioUnitario = 45,
                    UnidadesStock =4
                },
                new Models.Programa
                {
                    Id = 3,
                    Nombre = "Tensión",
                    PrecioUnitario = 45,
                    UnidadesStock =4
                },
                new Models.Programa
                {
                    Id = 4,
                    Nombre = "Glucosa",
                    PrecioUnitario = 45,
                    UnidadesStock =1
                },
                new Models.Programa
                {
                    Id = 5,
                    Nombre = "Embarazo",
                    PrecioUnitario = 56,
                    UnidadesStock =1
                }
            };
        }
        public List<Models.Programa> Programas { get; set; }
        public IEnumerable IDs
        {
            get
            {
                return Programas.Select(p => new { p.Id, p.Nombre }).ToArray();

            }
        }
    }
}