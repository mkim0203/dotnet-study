using k8s;

namespace K8sLibrary;

public class K8sClient
{
    /// <summary>
    /// 지정된 host 연결.
    /// 인증정보 없이 연결할경우 연결은되지만 다른 api에 대하여 권한 없음 뜨게됨.
    /// k8s service account 생성하여 token 발급하여 연결진행
    /// </summary>
    /// <param name="hostUrl"></param>
    /// <param name="connectionToken"></param>
    /// <param name="skipTlsVerify"></param>
    /// <returns></returns>
    public static Kubernetes BuildKubernetesClient(string hostUrl, string connectionToken, bool skipTlsVerify = false)
    {
        KubernetesClientConfiguration config = new()
        {
            Host = hostUrl,
            SkipTlsVerify = skipTlsVerify, // https 인증서 skip
            AccessToken = connectionToken // k8s service account 기준으로 발행한 시크릿 token
        };

        return new Kubernetes(config);
    }

    /// <summary>
    /// 로컬 연결.
    /// 로컬 연결 사용시 config 파일위치 찾아서 설정해주는것으로 보임.
    /// window => %UserProfile$.kube\config
    /// </summary>
    /// <returns></returns>
    public static Kubernetes BuildLocalKubernetesClient()
    {
        // docker desktop k8s 내부 연결시 아래 연결로 진행
        KubernetesClientConfiguration config = KubernetesClientConfiguration.BuildDefaultConfig();
        return new Kubernetes(config);
    }
}