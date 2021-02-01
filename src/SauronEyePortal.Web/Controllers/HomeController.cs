using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SauronEye.Common.Dto;
using SauronEye.Portal.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SauronEyePortal.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController()
        {
            
        }

        public IActionResult FlowEditor() => View();

        public IActionResult Privacy() => View();

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {
            List<Flows> fluxo = new List<Flows>(new List<Flows> 
            { 
                new Flows
                {
                    Name = "Abertura de Teste",
                    LastExecution = DateTime.Now,
                    Status = true
                    
                },
                new Flows
                {
                    Name = "Abertura de Notificação",
                    LastExecution = DateTime.Now,
                    Status = false
                },
                new Flows
                {
                    Name = "Abertura de Teste 3",
                    LastExecution = DateTime.Now,
                    Status = true
                },
            });
           

            HomeViewModel homeViewModel = new HomeViewModel();
            homeViewModel.FlowInProgress = fluxo;
            homeViewModel.FlowNews = fluxo;

            return View(homeViewModel);
        }
    }
}
