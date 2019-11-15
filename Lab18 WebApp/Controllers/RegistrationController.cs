using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Lab18_WebApp.Models;

namespace Lab18_WebApp.Controllers
{
    public class RegistrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult UserSummary(RegisterUser newUser) 
        {
            if (ModelState.IsValid)
            {
                return View(newUser);
            }
            else
            {
                return View("Index");
            }

        }
    }
}