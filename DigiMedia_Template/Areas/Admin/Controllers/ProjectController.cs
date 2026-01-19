using System.Threading.Tasks;
using DigiMedia_Template.Contexts;
using DigiMedia_Template.Models;
using DigiMedia_Template.ViewModels.CategoryViewModels;
using DigiMedia_Template.ViewModels.ProjectViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace DigiMedia_Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _environment;
        private readonly string _folderPath;

        public ProjectController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
            _folderPath = Path.Combine(_environment.WebRootPath, "assets", "images");
        }

        public async Task<IActionResult> Index()
        {
            var projects = await _context.Projects.Select(x => new ProjectGetVM
            {
                Id=x.Id,
                ImagePath=x.ImagePath,
                Name=x.Name,
                CategoryName=x.Category.Name
            }).ToListAsync();
            return View(projects);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _context.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
            ViewBag.Categories = categories;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var isExistCategories = await _context.Categories.AnyAsync(x => x.Id == vm.CategoryId);
            if(!isExistCategories)
            {
                ModelState.AddModelError("CategoryId", "Bele bir category id movcud deyil");
                return View(vm);
            }
            if (vm.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image must contain 2mb");
                return View(vm);
            }
            if (!vm.Image.ContentType.ToLower().Contains("image"))
            {
                ModelState.AddModelError("Image", "Image must be image format");
                return View(vm);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + vm.Image.FileName;
            string path = Path.Combine(_folderPath, uniqueFileName);
            using FileStream stream = new(path, FileMode.Create);
            await vm.Image.CopyToAsync(stream);
            Project project = new()
            {
                ImagePath = uniqueFileName,
                Name = vm.Name,
                CategoryId=vm.CategoryId
            };

            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var projects = await _context.Projects.FindAsync(id);
            if (projects is null)
            {
                return NotFound();
            }
            _context.Projects.Remove(projects);
            await _context.SaveChangesAsync();
            string deletedImagePath = Path.Combine(_folderPath, projects.ImagePath);
            if (System.IO.File.Exists(deletedImagePath))
                System.IO.File.Delete(deletedImagePath);
            return RedirectToAction(nameof(Index));
        }
        
        public async Task<IActionResult> Update(int id)
        {

            var projects = await _context.Projects.FindAsync(id);
            if (projects is null)
            {
                return NotFound();
            }
            ProjectUpdateVM vm = new()
            {
                Id = projects.Id,
                Name = projects.Name,
                CategoryId=projects.CategoryId
            };
            var categories = await _context.Categories.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToListAsync();
            ViewBag.Categories = categories;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProjectUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (vm.Image.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Image", "Image must contain 2mb");
                return View(vm);
            }
            if (!vm.Image.ContentType.ToLower().Contains("image"))
            {
                ModelState.AddModelError("Image", "Image must be image format");
                return View(vm);
            }
            var isExistProjects = await _context.Projects.FindAsync(vm.Id);
            if (isExistProjects is null)
            {
                return NotFound();
            }
            isExistProjects.Name = vm.Name;
            isExistProjects.CategoryId = vm.CategoryId;
             string uniqueFileName = Guid.NewGuid().ToString() + vm.Image.FileName;
            string path = Path.Combine(_folderPath, uniqueFileName);
            using FileStream stream = new(path, FileMode.Create);
            await vm.Image.CopyToAsync(stream);

            string oldImagePath = Path.Combine(_folderPath, isExistProjects.ImagePath);
            if (System.IO.File.Exists(oldImagePath))
                System.IO.File.Delete(oldImagePath);
            isExistProjects.ImagePath = uniqueFileName;
            _context.Projects.Update(isExistProjects);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
