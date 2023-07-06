using k8s;
using k8s.Autorest;
using K8sLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace K8sTest;

public class K8sCommonSetting
{
    internal readonly ITestOutputHelper _output;
    internal readonly string DefaultNamespace = "test-nc";
    public K8sCommonSetting(ITestOutputHelper output)
    {
        _output = output;
    }

    internal Kubernetes Connection(bool isLocal = true)
    {
        if (isLocal)
        {
            // docker desktop k8s 내부 연결시 아래 연결로 진행
            return K8sClient.BuildLocalKubernetesClient();
        }

        var connInfo = new
        {
            HostUrl = "https://kubernetes.docker.internal:6443/",
            Token =                "eyJhbGciOiJSUzI1NiIsImtpZCI6InBQdmY0a0ZBV1Z5VVNyZFpEN0RMSy0tZENBdUo4MFozbVM3VHQ2N19NUk0ifQ.eyJpc3MiOiJrdWJlcm5ldGVzL3NlcnZpY2VhY2NvdW50Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9uYW1lc3BhY2UiOiJkZWZhdWx0Iiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZWNyZXQubmFtZSI6InNlY3JldC1ta2ltIiwia3ViZXJuZXRlcy5pby9zZXJ2aWNlYWNjb3VudC9zZXJ2aWNlLWFjY291bnQubmFtZSI6Im1raW0iLCJrdWJlcm5ldGVzLmlvL3NlcnZpY2VhY2NvdW50L3NlcnZpY2UtYWNjb3VudC51aWQiOiI1NTg2YmMzNi00ZGRkLTQ5YWYtOGQ4MC1hMzRlNTNjYjMxZTkiLCJzdWIiOiJzeXN0ZW06c2VydmljZWFjY291bnQ6ZGVmYXVsdDpta2ltIn0.rgP2KJ5OTw_HSuLXPIBfosgW7UhnHItVe-_peI517oLrWtRCMU5yV4AwxN8fynhucmLKR0zJdIRLh1TSLrwfOuZxVEhDMkDmbRBgHyl6cmGZHpelc6p9eYsSlaYiY4xY-nHBub-EK2hA5Gv0hAfvKfz9ugokYbA9BIlnIONi0lyarFRQvpzthWiIlSheLngKva79TtLqKV-4xtfXIK_5tXBCfYR6nBz8dLmQ00gM9Kg69C4Celdj-iQDokZXqv2tV2nmTWd4qLT5bebViul0LaX_eqfEzpnLKCSlLyDe-amwzxBRhNcDeHsPAzCtSLmJwmeZCpIfXrugJSD4VQSSbg"
        };


        return K8sClient.BuildKubernetesClient(connInfo.HostUrl, connInfo.Token, true);
    }

    internal void WriteException(Exception ex)
    {
        if (ex.InnerException is not null)
        {
            WriteException(ex.InnerException);
        }

        if (ex is HttpOperationException hoe)
        {
            // exception 메시지
            _output.WriteLine(ex.Message);
            // k8s api에 요청한 정보 restapi url, method, content
            _output.WriteLine($"Request => {hoe.Request.Method}\r\n{hoe.Request.RequestUri}\r\n{hoe.Request.Content}");
            // k8s api에 응답내용. 상세 오류 메시지 content에서 확인
            _output.WriteLine($"Response => {hoe.Response.StatusCode}\r\n{hoe.Response.Content}");
        }
        else
        {
            _output.WriteLine(ex.Message);
        }
    }
}
