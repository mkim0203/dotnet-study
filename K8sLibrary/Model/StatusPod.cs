namespace K8sLibrary.Model;

/// <summary>
///     pod 상태 정보
/// </summary>
public sealed record StatusPod
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
    ///     보유한 container 수
    /// </summary>
    public int ContainerCount { get; set; }

    /// <summary>
    ///     pod 상태정보
    /// </summary>
    public string PodStatus { get; set; } = string.Empty;

    public IList<StatusContainer> StatusContainers { get; init; } = new List<StatusContainer>();
}
