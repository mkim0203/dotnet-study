namespace K8sLibrary.Model;

/// <summary>
/// 컨테이너 event 정보. 건단위 객체
/// </summary>
public sealed record ContainerEvent
{
    /// <summary>
    /// event 타입
    /// </summary>
    public string EventType { get; set; } = string.Empty;
    /// <summary>
    /// 발생시간. k8s 기준
    /// </summary>
    public DateTime? RecordTime { get; set; }
    /// <summary>
    /// 사유
    /// </summary>
    public string Reason { get; set; } = string.Empty;
    /// <summary>
    /// event 메시지
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
