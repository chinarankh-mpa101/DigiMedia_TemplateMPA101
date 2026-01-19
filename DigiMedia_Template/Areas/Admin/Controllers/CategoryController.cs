using System.Threading.Tasks;
using DigiMedia_Template.Contexts;
using DigiMedia_Template.Models;
using DigiMedia_Template.ViewModels.CategoryViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigiMedia_Template.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController: Controller
    {
        private readonly AppDbContext _context;
        

        public CategoryController(AppDbContext context)
        {
            _context = context;
            
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories.Select(x => new CategoryGetVM
            {
                Id=x.Id,
                Name=x.Name
            }).ToListAsync();
            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            Category category = new()
            {
                Name = vm.Name
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var categories = await _context.Categories.FindAsync(id);
            if(categories is null)
            {
                return NotFound();
            }
             _context.Categories.Remove(categories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {

            var categories = await _context.Categories.FindAsync(id);
            if(categories is null)
            {
                return NotFound();
            }
            CategoryUpdateVM vm = new()
            {
                Id = categories.Id,
                Name = categories.Name
            };
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var isExistCategories = await _context.Categories.FindAsync(vm.Id);
            if(isExistCategories is null)
            {
                return BadRequest();

            }

            isExistCategories.Name = vm.Name;
            _context.Categories.Update(isExistCategories);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
