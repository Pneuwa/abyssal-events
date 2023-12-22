namespace Abyssal_Events.Models.Domain
{
	public class EventPostLike
	{
		public Guid Id { get; set; }
		public Guid EventPostId { get; set; }
		public Guid UserId { get; set; }

	}
}
