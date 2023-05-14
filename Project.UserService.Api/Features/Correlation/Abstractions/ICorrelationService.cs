namespace Project.UserService.Api.Features.Correlation.Abstractions;

public interface ICorrelationService
{
    string GetCorrelationHeaderName();

    string GetCorrelationId();
}
