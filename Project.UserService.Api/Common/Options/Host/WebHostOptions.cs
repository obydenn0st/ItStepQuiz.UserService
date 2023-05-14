namespace Project.UserService.Api.Common.Options.Host;

/// <summary>
/// Contains information to configure host
/// </summary>
internal class WebHostOptions
{
    /// <summary>Appsettings section name</summary>
    internal const string SectionName = nameof(WebHostOptions);

    /// <summary>
    /// create <see cref="WebHostOptions"/> instance
    /// </summary>
    /// <param name="instanceName"></param>
    /// <param name="webAddress"></param>
    internal WebHostOptions(string instanceName, string webAddress)
    {
        InstanceName = instanceName;
        WebAddress = webAddress;
    }

    /// <summary>Gets the instance name of the service</summary>
    internal string InstanceName { get; }

    /// <summary>Gets the web address of the service.</summary>
    internal string WebAddress { get; }
}