using System.Diagnostics;
using DigiMedia_Template.Models;
using Microsoft.AspNetCore.Mvc;

namespace DigiMedia_Template.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

     
       
    }
}
