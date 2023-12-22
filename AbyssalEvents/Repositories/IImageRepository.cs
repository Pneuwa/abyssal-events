namespace Abyssal_Events.Repositories
{
	public interface IImageRepository
	{
		Task<string> UploadAsync(IFormFile formFile);
	}
}
