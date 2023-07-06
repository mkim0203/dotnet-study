namespace K8sLibrary.Model;

/// <summary>
///     container 로그
/// </summary>
public sealed record ContainerLog
{
    /// <summary>
    ///     container 이름
    /// </summary>
    public string ContainerName { get; set; } = string.Empty;

    /// <summary>
    ///     로그정보
    /// </summary>
    public IList<string> LogDatas { get; init; } = new List<string>();
}
