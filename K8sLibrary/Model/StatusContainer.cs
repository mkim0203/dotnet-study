namespace K8sLibrary.Model;

/// <summary>
///     pod에 있는 container 상태정보
/// </summary>
public sealed record StatusContainer
{
    /// <summary>
    ///     container 이름
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     docker image 정보
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    ///     ready 상태 정보
    /// </summary>
    public bool Ready { get; set; }
}
