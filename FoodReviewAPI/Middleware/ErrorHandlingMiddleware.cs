using FoodReviewAPI.Exceptions;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace FoodReviewAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (UnauthorizedException unauthorizedException)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync(unauthorizedException.Message);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (InternalServerErrorException internalServerErrorException)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(internalServerErrorException.Message);
            }
            catch (BadRequestException badRequestException)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequestException.Message);
            }
            catch (Exception)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Something went wrong");
            }
        }
    }
}
