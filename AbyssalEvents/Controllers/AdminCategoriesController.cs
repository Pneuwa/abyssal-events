using Abyssal_Events.Models.Domain;
using Abyssal_Events.Models.ViewModel;
using Abyssal_Events.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Abyssal_Events.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public AdminCategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        private void ValidateRequest()
        {
            ModelState.AddModelError("Name", "This category is already exist");
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddCategoryRequest req)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category category = new Category
            {
                Name = req.Name,
            };
            var addedCategory = await _categoryRepository.AddAsync(category);

            if(addedCategory is null)
            {
                ValidateRequest();
                return View();
            }

            return RedirectToAction("Categories");
        }
        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            IEnumerable<Category> categories = await _categoryRepository.GetAllAsync();

            return View(categories);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var selectedCategory = await _categoryRepository.GetByIdAsync(id);
            if (selectedCategory != null)
            {
                EditCategoryRequest editCategoryRequest = new EditCategoryRequest { 
                    Id = selectedCategory.Id, 
                    Name = selectedCategory.Name,
                };
                return View(editCategoryRequest);
            }

            return View(null);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditCategoryRequest req)
        {
            Category category = new Category {
                Id = req.Id,
                Name = req.Name,
            };
            
            var updatedCategory = await _categoryRepository.UpdateAsync(category);

            if (updatedCategory is null)
            {
                ValidateRequest();
                return View();
            }

            return RedirectToAction("Categories");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditCategoryRequest req)
        {
            var deletedCategory = await _categoryRepository.DeleteAsync(req.Id);
            if (deletedCategory != null)
            {
                return RedirectToAction("Categories");
            }
            return RedirectToAction("Edit", new {id = req.Id});
        }
    }
}
