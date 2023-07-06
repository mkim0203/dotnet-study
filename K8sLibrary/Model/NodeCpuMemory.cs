namespace K8sLibrary.Model;

/// <summary>
/// node 의 cpu, 메모리 정보
/// </summary>
public sealed record NodeCpuMemory
{
    /// <summary>
    /// node 이름
    /// </summary>
    public string NodeName { get; set; } = string.Empty;
    /// <summary>
    /// 할당된 cpu
    /// </summary>
    public string? AllocatableCpu { get; set; }
    /// <summary>
    /// cpu core
    /// </summary>
    public int? CpuCore { get; set; }
    /// <summary>
    /// 메트릭스 조회 cpu 사용량
    /// </summary>
    public string? MetricsCpu { get; set; }
    /// <summary>
    /// cpu 사용량
    /// </summary>
    public double? UsageCpu { get; set; }
    /// <summary>
    /// 할당된 memory
    /// </summary>
    public string? AllocatableMemory { get; set; }
    /// <summary>
    /// 메모리
    /// </summary>
    public long? TotalMemory { get; set; }
    /// <summary>
    /// 메트릭스에서 수집한 메모리 사용량 (단위 ki, mi 등 사용)
    /// </summary>
    public string? MetricsMemory { get; set; }
    /// <summary>
    /// 메트릭스에서 수집한 메모리 double 로 변환
    /// </summary>
    public double? UsageMemory { get; set; }

    /// <summary>
    /// cpu 사용률 (%)
    /// </summary>
    public double? UsageCpuPersent
    {
        get
        {
            // cpu 사용량 / core * 100
            if (CpuCore is not null && UsageCpu is not null)
            {
                return UsageCpu / CpuCore * 100;
            }
            return null;
        }
    }

    /// <summary>
    /// 메모리 사용률 (%)
    /// </summary>
    public double? UsageMemoryPersent
    {
        get
        {
            // 메모리 사용량 / 전체 * 100
            if (TotalMemory is not null && UsageMemory is not null)
            {
                return UsageMemory / TotalMemory * 100;
            }
            return null;
        }
    }
}
