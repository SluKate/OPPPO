using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NewStorageLab.Domain.Exceptions;


namespace TaskTracker.Headers;

public class ValidationExceptionFilter : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {
        ValidationException? exepsh = context.Exception as ValidationException;

        if (exepsh is not null)
        {
            context.Result = new OkObjectResult(new { error = exepsh.Message });
        }
    }
}