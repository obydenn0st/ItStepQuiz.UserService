using System.Diagnostics;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Project.UserService.Core.Behaviours;

public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> _logger;

    public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest, TResponse>> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    /// <inheritdoc />
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.LogInformation("----- Обработка команды {CommandHandler} ({@Command})", typeof(TRequest).Name, request);

        var sw = Stopwatch.StartNew();
        var response = await next();
        sw.Stop();

        _logger.LogInformation("----- Обработка команды {CommandHandler} завершена ({ElapsedTime} мс)", typeof(TRequest).Name, sw.ElapsedMilliseconds);

        return response;
    }
}