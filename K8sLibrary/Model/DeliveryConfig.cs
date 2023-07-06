using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K8sLibrary.Model;

public sealed record DeliveryConfig
{

    /// <summary>
    ///     public ip 설정시 입력
    /// </summary>
    [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "ExternalIp는 IP 형식만 지원합니다.")]
    public string? ExternalIp { get; set; }

    /// <summary>
    ///     k8s 배포할 namespace 위치
    /// </summary>
    [MaxLength(50)]
    [Required]
    public string NamespaceName { get; set; } = string.Empty;

    /// <summary>
    /// 배포시 host file에 등록할 목록
    /// </summary>
    public IList<IpItem> HostList { get; init; } = new List<IpItem>();

    /// <summary>
    ///     도커 이미지 목록
    /// </summary>
    [Required]
    public IList<DockerItem> DockerList { get; init; } = new List<DockerItem>();
}

public sealed record IpItem
{
    /// <summary>
    /// ip
    /// </summary>
    [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "IP 형식만 지원합니다.")]
    [Required]
    public string Ip { get; set; } = string.Empty;
    /// <summary>
    /// host 이름
    /// </summary>
    [Required] public string HostName { get; set; } = string.Empty;
}


/// <summary>
/// docker 정보
/// </summary>
public sealed record DockerItem
{
    /// <summary>
    ///     pod 컨테이너에 이름으로 설정됨
    /// </summary>
    [Required]
    [RegularExpression(@"^[a-z0-9-]+$", ErrorMessage = "Docker Name는 소문자와 '-'까지 사용할수 있습니다.")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     이미지 위치
    /// </summary>
    /// <example>114.206.189.242:30008/coding-academy/mycorewebapp:0.0.1</example>
    [Required]
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// 수행 명령어
    /// </summary>
    public IList<string>? Command { get; set; }
    /// <summary>
    /// argment
    /// </summary>
    public IList<string>? Args { get; set; }
    /// <summary>
    /// 사용할 port 정보
    /// </summary>
    public IList<PortInfo> Ports { get; init; } = new List<PortInfo>();
    /// <summary>
    /// 사용할 환경 변수 정보
    /// </summary>
    public IList<NameValueItme> Environment { get; init; } = new List<NameValueItme>();
    /// <summary>
    /// 추가할 volume 정보
    /// </summary>
    public IList<VolumeItem> Volumes { get; init; } = new List<VolumeItem>();

    /// <summary>
    ///     pod 스토리지 설정.
    ///     로컬 임시(ephemeral) 스토리지
    ///     ex) 10Gi
    /// </summary>
    public string? Storeage { get; set; }
}

public sealed record PortInfo
{
    [Range(1, ushort.MaxValue)] public int Number { get; set; }

    /// <summary>
    ///     대문자
    ///     TCP/UDP/SCTP
    /// </summary>
    [RegularExpression("^(TCP|UDP|SCTP)$", ErrorMessage = "TCP, UDP, SCTP 중 하나만 설정가능합니다.(대문자만 허용)")]
    public string Protocol { get; set; } = "TCP";

    [Range(30000, 32767)] public int? NodePort { get; set; }

    /// <summary>
    /// 외부 노출 옵션
    /// </summary>
    public bool IsExternalOpen { get; set; }

    public string? WebProtocol { get; set; }
}


public record NameValueItme
{
    [Required] public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     값이 빈값일수도 있어서 Required 제거함.
    /// </summary>
    public string Value { get; set; } = string.Empty;
}

/// <summary>
///     볼륨 설정 Item
/// </summary>
public sealed record VolumeItem : NameValueItme
{
    /// <summary>
    ///     볼륨 path 가 없으면 emptyDir로 볼륨 연결 있는경우 hostpath 볼륨으로 연결함
    /// </summary>
    public string HostDirPath { get; set; } = string.Empty;

    /// <summary>
    ///     기본값은 Directory
    /// </summary>
    public VolumeItemType VolumeType { get; set; } = VolumeItemType.Directory;
}

public enum VolumeItemType
{
    Directory,
    File,
    ETC
}