using System.Security.Authentication;

namespace Abyssal_Events.Middlewares
{
	public class ExceptionHandlerMiddleware
	{
		private readonly ILogger<ExceptionHandlerMiddleware> _logger;
		private readonly RequestDelegate _next;

		public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
			_logger = logger;
			_next = next;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _next.Invoke(context);
			}catch (Exception ex)
			{
				switch (ex)
				{
					case AuthenticationException:
						context.Response.StatusCode = 401;
						await context.Response.WriteAsync("Not authenticated");
						break;
					case UnauthorizedAccessException:
						context.Response.StatusCode = 403;
						await context.Response.WriteAsync("Not authorized");
						break;
					case FileNotFoundException:
						context.Response.StatusCode = 404;
						await context.Response.WriteAsync("Not found");
						break;
					default:
						context.Response.StatusCode = 500;
						await context.Response.WriteAsync("An error occured");
						break;
				}
				_logger.LogError(ex, ex.Message);
			}
		}
	}
}
