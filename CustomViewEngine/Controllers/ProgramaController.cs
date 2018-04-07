﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CustomViewEngine.Controllers
{
    public class ProgramaController : Controller
    {
        // GET: Programa
        public ActionResult Index()
        {
            var VM = new ViewModels.Store();
            return View(VM);
        }
        public ActionResult ShowError()
        {
            return View("Error");
        }
    }
}