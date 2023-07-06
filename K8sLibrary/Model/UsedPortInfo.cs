namespace K8sLibrary.Model;

/// <summary>
///     사용중인 port 정보.
/// </summary>
public sealed record UsedPortInfo
{
    /// <summary>
    ///     deployment name
    /// </summary>
    public string DeploymentName { get; set; } = string.Empty;

    /// <summary>
    ///     service name
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;

    /// <summary>
    ///     사용중인 외부 ip
    /// </summary>
    public string? ExternalIp { get; set; }

    /// <summary>
    ///     컨테이너 정보 및 port 정보
    /// </summary>
    /// <remarks>pod에는 여러개 컨테이너가 존재 가능</remarks>
    public IList<ContainerPort> ContainerPortList { get; init; } = new List<ContainerPort>();
}
