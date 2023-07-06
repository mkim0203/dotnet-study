namespace K8sLibrary.Model;

/// <summary>
///     pod 로그 정보
/// </summary>
public sealed record LogsPod
{
    /// <summary>
    ///     pod 이름
    /// </summary>
    public string PodName { get; set; } = string.Empty;

    /// <summary>
    ///     연결된 deployment 이름
    ///     pod label app=DeployementName
    /// </summary>
    public string DeployementName { get; set; } = string.Empty;

    /// <summary>
    ///     container 로그
    /// </summary>
    public IList<ContainerLog> ContainerLogs { get; init; } = new List<ContainerLog>();
}
