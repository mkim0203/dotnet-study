namespace K8sLibrary.Model;

public sealed record EventsPod
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
    ///     event 정보
    /// </summary>
    public IList<ContainerEvent> ContinerEvents { get; init; } = new List<ContainerEvent>();
}
