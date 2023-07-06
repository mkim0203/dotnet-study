namespace K8sLibrary.Model;

/// <summary>
///     컨테이너 정보 및 port 정보
/// </summary>
public sealed record ContainerPort
{
    /// <summary>
    ///     컨테이너 이름
    /// </summary>
    public string ContainerName { get; set; } = string.Empty;
    /// <summary>
    ///     컨테이너 port 정보 목록.
    /// </summary>
    /// <remarks>컨테이너에 open 된 포트 목록은 여러개 가능</remarks>
    public IList<ContainerPortDetail> ContainerPortDetailList { get; init; } = new List<ContainerPortDetail>();
}
