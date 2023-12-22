using Abyssal_Events.Models;
using Abyssal_Events.Models.Domain;
using Abyssal_Events.Models.ViewModel;
using Abyssal_Events.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Abyssal_Events.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ILogger<HomeController> logger, IEventRepository eventRepository, ICategoryRepository categoryRepository)
        {
            _logger = logger;
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<IActionResult> Index(int page = 1, int size = 2)
        {
            var events = await _eventRepository.GetAllAsync(page, size);
            var categories = await _categoryRepository.GetAllAsync();

            int totalEvents = await _eventRepository.CountEventsAsync();
            int maxPageNumber = (int)Math.Ceiling((decimal)totalEvents / size);

            var model = new HomeViewModel { 
                Events = events,
                MaxPageNumber = maxPageNumber,
                Categories = categories,
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Categories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            if (categories is not null)
            {
                return View(categories);
            }
            return View(null);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
