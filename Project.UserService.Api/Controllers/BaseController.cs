using MediatR;
using Microsoft.AspNetCore.Mvc;
using Project.UserService.Core.Entities.Responce;
using System.Net;

namespace Project.UserService.Api.Controllers;

[ApiController]
[Produces("application/json")]
[ProducesResponseType(statusCode: (int)HttpStatusCode.OK, type: typeof(UserResponce))]
[ProducesResponseType(statusCode: (int)HttpStatusCode.InternalServerError, type: typeof(ProblemDetails))]
public class BaseController : ControllerBase
{
    /// <summary></summary>
    protected ISender Mediator =>
        HttpContext.RequestServices.GetRequiredService<ISender>() ?? throw new ArgumentNullException(nameof(ISender));
}
