using Abyssal_Events.Models.Domain;
using Abyssal_Events.Models.ViewModel;
using Abyssal_Events.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Abyssal_Events.Controllers
{
    public class ManageEventsController : Controller
    {
		private readonly ICategoryRepository _categoryRepository;
		private readonly IEventRepository _eventRepository;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

        public ManageEventsController(ICategoryRepository categoryRepository, 
            IEventRepository eventRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
			_categoryRepository = categoryRepository;
			_eventRepository = eventRepository;
			_signInManager = signInManager;
			_userManager = userManager;
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Add()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var eventModel = new AddEventPostRequest { 
                Categories = categories.Select(category => new SelectListItem { 
                    Text = category.Name,
                    Value = category.Id.ToString(),
                }),
            };

            return View(eventModel);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Add(AddEventPostRequest req)
        {
            var eventModel = new EventPost {
                Title = req.Title,
                Description = req.Description,
                Content = req.Content,
                FeaturedImage = req.FeaturedImage,
                Date = req.Date,
                Place = req.Place,
                Organizer = req.Organizer,
                CategoryId = req.SelectedCategory
            };
            await _eventRepository.AddAsync(eventModel);

            return RedirectToAction("Events");
        }
        [HttpGet]
        public async Task<IActionResult> Events(int page = 1, int size = 10)
        {
			if (_signInManager.IsSignedIn(User))
			{
				var username = _userManager.GetUserName(User);
				var userEvents = await _eventRepository.FilterUserEventsAsync(username, page, size);
                int totalUserEvents = await _eventRepository.CountUserEventsAsync(username);
				int maxPageNumber = (int)Math.Ceiling((decimal)totalUserEvents / size);

				if (userEvents is not null)
				{
                    var model = new FilterUserViewModel
                    {
                        MaxPageNumber = maxPageNumber,
                        Events = userEvents
					};
					return View(model);
				}
			}
			return View(null);
        }
        [HttpGet]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(Guid id, EditEventPostRequest req)
        {
            var selectedEvent = await _eventRepository.GetByIdAsync(id);
            var categories = await _categoryRepository.GetAllAsync();

            if(selectedEvent != null)
            {
				var eventPost = new EditEventPostRequest
				{
					Id = selectedEvent.Id,
					Title = selectedEvent.Title,
					Description = selectedEvent.Description,
					Content = selectedEvent.Content,
					FeaturedImage = selectedEvent.FeaturedImage,
					Date = selectedEvent.Date,
					Place = selectedEvent.Place,
					Organizer = selectedEvent.Organizer,
					Categories = categories.Select(x => new SelectListItem
					{
						Text = x.Name,
						Value = x.Id.ToString()
					}),
					SelectedCategory = selectedEvent.Category.Name
				};
				return View(eventPost);
			}
            return View(null);
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Edit(EditEventPostRequest req)
        {
			var eventPost = new EventPost
            {
                Id = req.Id,
                Title = req.Title,
                Description = req.Description,
                Content = req.Content,
				FeaturedImage = req.FeaturedImage,
                Date = req.Date.Date,
				Place = req.Place,
				Organizer = req.Organizer,
				CategoryId = Guid.Parse(req.SelectedCategory)
			};

            var updatedEvent = await _eventRepository.UpdateAsync(eventPost);

            if(updatedEvent != null)
            {
                return RedirectToAction("Events");
            }
            return RedirectToAction("Edit", new {id = req.Id});
        }
        [HttpPost]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Delete(EditCategoryRequest req)
        {
            var deletedEvent = await _eventRepository.DeleteAsync(req.Id);
            if(deletedEvent != null)
            {
                return RedirectToAction("Events");
            }
            return RedirectToAction("Edit", new {id = req.Id});
        }
    }
}
