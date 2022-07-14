﻿using ControlOrigins.Survey.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ControlOrigins.Survey.Api.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : BaseController
    {
        public HomeController() : base()
        {
        }

        /// <summary>
        /// Employee Single Page Applications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("/home/EmpSinglePage/")]
        public ActionResult EmpSinglePage() { return View(); }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        { return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }); }

        public IActionResult Index()
        {
            return View();
        }
    }
}