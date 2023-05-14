using Project.UserService.Infrastructure.Exceptions;

namespace Project.UserService.Infrastructure.Utils;

/// <summary>
/// Utility to retry HTTP request
/// </summary>
public static class Retry
{
    public static async Task ExecuteOn<TException>(Func<Task> operation, int attempts, int[] retryDelays)
        where TException : HttpRequestException
    {
        var originalAttemptsCount = attempts;

        do
        {
            try
            {
                attempts--;
                await operation();

                break;
            }
            catch (TException ex)
            {
                if (attempts <= 0)
                    throw new RetryLimitException(ex.Message, originalAttemptsCount);

                var delay = attempts > retryDelays.Length ? retryDelays.Last() : retryDelays[attempts];

                await Task.Delay(TimeSpan.FromSeconds(delay));
            }
        } while (true);
    }
}