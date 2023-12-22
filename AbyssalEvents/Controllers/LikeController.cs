using Abyssal_Events.Models.Domain;
using Abyssal_Events.Models.ViewModel;
using Abyssal_Events.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Abyssal_Events.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class LikeController : ControllerBase
	{
		private readonly ILikeRepository _likeRepository;

		public LikeController(ILikeRepository likeRepository)
        {
			_likeRepository = likeRepository;
		}
        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] LikeRequest req)
		{
			var model = new EventPostLike { 
				EventPostId = req.EventPostId,
				UserId = req.UserId,
			};
			await _likeRepository.AddLikeAsync(model);

			return Ok();
		}
		[HttpPost]
		[Route("Remove")]
		public async Task<IActionResult> RemoveLike([FromBody] LikeRequest req)
		{
			var model = new EventPostLike
			{
				EventPostId = req.EventPostId,
				UserId = req.UserId,
			};
			await _likeRepository.RemoveLikeAsync(model, req.UserId);

			return Ok();
		}
		[HttpGet]
		[Route("{eventPostId}/totalLikes")]
		public async Task<IActionResult> GetTotalLikesById([FromRoute] Guid eventPostId)
		{
			var totalLikes = await _likeRepository.GetTotalLikesAsync(eventPostId);
			return Ok(totalLikes);
		}
	}
}
