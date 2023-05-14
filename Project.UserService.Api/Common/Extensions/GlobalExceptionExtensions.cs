using System.Net;
using Microsoft.AspNetCore.Mvc;
using Project.UserService.Api.Common.Constants;
using Project.UserService.Core.Common.Exceptions;

namespace Project.UserService.Api.Common.Extensions;

/// <summary>
/// Extension for Exceptions
/// </summary>
public static class GlobalExceptionExtensions
{
    /// <summary>
    /// Generates <see cref="ProblemDetails"/> instance
    /// </summary>
    public static ProblemDetails GenerateProblemDetails(this Exception ex,
        HttpContext context,
        string title,
        HttpStatusCode code)
    {
        context.Response.StatusCode = (int)code;
        context.Response.ContentType = "application/problem+json";

        ProblemDetails problemDetails = new() { Status = (int)code, Title = title, Detail = ex.Message, };

        if (ex is RequestValidationException requestValidationException)
        {
            problemDetails.Extensions.Add("errors",
                requestValidationException.Errors.SelectMany(x => x.Value));
        }

        problemDetails.Extensions.Add(
            HeaderConstant.CorrelationId.ToLower(),
            context.Request.Headers[HeaderConstant.CorrelationId].ToString());

        return problemDetails;
    }
}