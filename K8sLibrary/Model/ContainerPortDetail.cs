using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace K8sLibrary.Model;

/// <summary>
///     컨테이너 port 정보
/// </summary>
public sealed record ContainerPortDetail
{
    /// <summary>
    ///     컨테이너 내에서 쓰이는 port
    /// </summary>
    public int Port { get; set; }

    public string Protocol { get; set; } = string.Empty;

    /// <summary>
    ///     service 에서 쓰는 서비스 Port 이름.
    /// </summary>
    public string? ServicePortName { get; set; }

    /// <summary>
    ///     service 에서 노출하고 있는 Port 정보
    ///     NodePort에서 ContainerPort로 포트포워드 함.
    /// </summary>
    public int? NodePort { get; set; }
}
