using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SunmathiTech.HRMS.Models;

namespace SunmathiTech.HRMS.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Bookings()
        {
            return View();
        }

        public IActionResult Categories()
        {
            return View();
        }

        public IActionResult Classes()
        {
            return View();
        }

        public IActionResult Countries()
        {
            return View();
        }

        public IActionResult CreditcardTypes()
        {
            return View();
        }

        public IActionResult PaymentTypes()
        {
            return View();
        }

        public IActionResult ProofTypes()
        {
            return View();
        }

        public IActionResult Rates()
        {
            return View();
        }

        public IActionResult Relationships()
        {
            return View();
        }

        public IActionResult Rooms()
        {
            return View();
        }

        public IActionResult States()
        {
            return View();
        }

        public IActionResult Titles()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorView { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
