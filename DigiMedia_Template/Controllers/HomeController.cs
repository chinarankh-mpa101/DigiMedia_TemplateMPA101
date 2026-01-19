using System.Diagnostics;
using DigiMedia_Template.Contexts;
using DigiMedia_Template.Models;
using DigiMedia_Template.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiMedia_Template.Controllers
{
    public class HomeController(AppDbContext _context) : Controller
    {

        [Authorize(Roles ="Member")]
        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.Select(x => new ProjectGetVM
            {
                Id = x.Id,
                ImagePath = x.ImagePath,
                Name = x.Name,
                CategoryName = x.Category.Name
            }).ToListAsync();
            return View(projects);
   
        }

     
       
    }
}
