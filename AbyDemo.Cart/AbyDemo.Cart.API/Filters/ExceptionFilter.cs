using AbyDemo.Cart.Application.Products.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AbyDemo.Cart.API.Filters;

public class ExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        ProblemDetails problem = context.Exception switch
        {
            ProductNotFoundException ex => new ProblemDetails
            {
                Title = "Product not found",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message,
            },
            _ => new ProblemDetails
            {
                Title = "Unexpected error",
                Status = StatusCodes.Status400BadRequest,
                Detail = "An internal error occurred.",
            },
        };

        context.Result = new ObjectResult(problem) { StatusCode = problem.Status };
        context.ExceptionHandled = true;
        _logger.LogError(context.Exception, "Unhandled exception in CartController");
    }
}
