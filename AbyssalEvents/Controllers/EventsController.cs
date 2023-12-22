using Abyssal_Events.Models.Domain;
using Abyssal_Events.Models.ViewModel;
using Abyssal_Events.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Abyssal_Events.Controllers
{
    public class EventsController : Controller
    {
        private readonly IEventRepository _eventRepository;
        private readonly ICategoryRepository _categoryRepository;
		private readonly ILikeRepository _likeRepository;
		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;

		public EventsController(IEventRepository eventRepository, 
            ICategoryRepository categoryRepository, 
            ILikeRepository likeRepository,
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
			_likeRepository = likeRepository;
			_signInManager = signInManager;
			_userManager = userManager;
		}
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            var eventPost = await _eventRepository.GetByIdAsync(id);
            var eventPostViewModel = new EventDetailsViewModel();
            bool isLiked = false;

			if (eventPost is not null)
            {
                var totalLikes = await _likeRepository.GetTotalLikesAsync(id);

                if (_signInManager.IsSignedIn(User))
                {
                    var userId = _userManager.GetUserId(User);
                    if(userId is not null)
                    {
                        var totalLikesForEvent = await _likeRepository.GetTotalLikesForEventAsync(id);
                        var likeFromUser = totalLikesForEvent.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
                        isLiked = likeFromUser == null ? false : true;
                    }
                }

				eventPostViewModel = new EventDetailsViewModel
				{
					Id = eventPost.Id,
					Title = eventPost.Title,
					Description = eventPost.Description,
					Content = eventPost.Content,
					FeaturedImage = eventPost.FeaturedImage,
					Date = eventPost.Date,
					Place = eventPost.Place,
					Organizer = eventPost.Organizer,
                    Category = eventPost.Category,
					TotalLikes = totalLikes,
                    IsLiked = isLiked,
				};
			}
            return View(eventPostViewModel);
        }
        [HttpGet]
        public async Task<IActionResult> Filter(Guid categoryId, int page = 1, int size = 2)
        {
            var filteredEvents = await _eventRepository.FilterAsync(categoryId, page, size);

            var category = await _categoryRepository.GetByIdAsync(categoryId);
            int totalFilteredEvents = await _eventRepository.CountFilteredEventsAsync(categoryId);
            int maxPageNumber = (int)Math.Ceiling((decimal)totalFilteredEvents / size);

            if(filteredEvents is not null)
            {
                var model = new FilterViewModel
                {
                    CategoryId = category.Id,
                    CategoryName = category.Name,
                    MaxPageNumber = maxPageNumber,
                    Events = filteredEvents,
                };
                return View(model);
            }
            return View(null);
        }
        [HttpGet]
        public async Task<IActionResult> Favorites(int page = 1, int size = 2)
        {
            if (_signInManager.IsSignedIn(User))
            {
                Guid userId = Guid.Parse(_userManager.GetUserId(User));
                var favorites = await _likeRepository.GetUserFavoritesAsync(userId, page, size);
                int totalfavoritedEvents = await _likeRepository.CountFavoriteEventsAsync(userId);
                int maxPageNumber = (int)Math.Ceiling((decimal)totalfavoritedEvents / size);

                if (favorites is not null)
				{
					var model = new FavoritesViewModel
					{
						Username = _userManager.GetUserName(User),
                        MaxPageNumber = maxPageNumber,
						FavoriteEvents = favorites.ToList(),
					};
					return View(model);
				}
			}
			return View(null);
		}
    }
}
