using k8s.Autorest;
using k8s.Models;
using k8s;
using K8sLibrary.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K8sLibrary;

public class Worker
{
    private readonly Kubernetes _client;

    /// <summary>
    ///     Deployment 이름 접두사
    /// </summary>
    private readonly string _depPrefix = "dep-";

    /// <summary>
    ///     Service 이름 접두사
    /// </summary>
    private readonly string _svcPrefix = "svc-";

    public Worker(Kubernetes client) => _client = client;

    #region Namespace

    /// <summary>
    ///     k8s 네임스페이스 조회
    ///     kubectl get nc
    /// </summary>
    /// <returns></returns>
    public Task<V1NamespaceList> ListNamespaceAsync(CancellationToken token = default) => _client.CoreV1.ListNamespaceAsync(cancellationToken: token);

    /// <summary>
    ///     k8s 네임스페이스 있는지 확인
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <returns></returns>
    public async Task<bool> ExistsNamespaceAsync(string namespaceName, CancellationToken token = default)
    {
        V1NamespaceList ncs = await ListNamespaceAsync(token).ConfigureAwait(false);

        if (ncs.Items.FirstOrDefault(x => x.Metadata.Name.Equals(namespaceName, StringComparison.Ordinal)) is null)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    ///     k8s 네임스페이스 생성
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <returns></returns>
    public async Task<bool> CreateNamespaceAsync(string namespaceName, CancellationToken token = default)
    {
        // 이미 있는경우 생성시 오류 발생함.
        V1Namespace nsBody = new() { Metadata = new V1ObjectMeta { Name = namespaceName } };
        await _client.CoreV1.CreateNamespaceAsync(nsBody, cancellationToken: token).ConfigureAwait(false);
        return true;
    }

    #endregion

    #region Pod

    /// <summary>
    ///     pod 목록 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <returns></returns>
    public Task<V1PodList> ListPodAsync(string namespaceName, CancellationToken token = default) => _client.CoreV1.ListNamespacedPodAsync(namespaceName, cancellationToken: token);

    /// <summary>
    ///     pod 조회. deployId.Id와 일치한 pod 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<V1PodList> ListPodAsync(string namespaceName, string deployId, CancellationToken token = default) =>
        // pod의 label에 app=deployId로 생성하기 때문에 label 기준으로 조회함.
        _client.CoreV1.ListNamespacedPodAsync(namespaceName, labelSelector: $"app={_depPrefix}{deployId}", cancellationToken: token);

    
    /// <summary>
    ///     pod 상태 전체 정보 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<List<StatusPod>> StatusPodAsync(string namespaceName, CancellationToken token = default)
    {
        V1PodList pods = await ListPodAsync(namespaceName, token).ConfigureAwait(false);

        List<StatusPod> result = new();

        foreach (V1Pod pod in pods.Items)
        {
            result.Add(ConvertStatusPod(pod));
        }

        return result;
    }

    /// <summary>
    ///     pod 상태 정보 조회. Deploy id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<StatusPod?> StatusPodAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        V1Pod? pod = await NewestPodAsync(namespaceName, deployId, token).ConfigureAwait(false);
        // null로 넘어오는경우가 있어 null 체크
        if (pod is null || pod.Status is null || pod.Status.ContainerStatuses is null)
        {
            return null;
        }

        return ConvertStatusPod(pod);
    }

    /// <summary>
    ///     최근 생성 된 Pod 조회. deploy Id 기준
    /// </summary>
    /// <remarks>기존 pod에 문제 생겼을경우 재생성 되는 과정이 있음. 그것을 구분하기 위해 최근 생성된 Pod 기능 구현</remarks>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<V1Pod?> NewestPodAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        // deployId 기준의 pod 목록 조회
        V1PodList podList = await ListPodAsync(namespaceName, deployId, token).ConfigureAwait(false);
        if (podList.Items.Count == 0)
        {
            return null;
        }

        // 만일 pod 가 여러개 생겼으면 마지막 pod를 기준으로 가져와 진행함.
        V1Pod pod = podList.Items.Count > 1 ? podList.Items.OrderByDescending(x => x.Metadata.CreationTimestamp).First() : podList.Items.First();

        return pod;
    }


    /// <summary>
    /// 조회된 pod 정보 StatusPod 로 변환
    /// </summary>
    /// <param name="pod"></param>
    /// <returns></returns>
    private static StatusPod ConvertStatusPod(V1Pod pod)
    {
        StatusPod result = new() { PodName = pod.Metadata.Name };

        bool keyExists = pod.Metadata.Labels.TryGetValue("app", out string? deployName);
        if (keyExists && deployName is not null)
        {
            result.DeployementName = deployName;
        }

        result.ContainerCount = pod.Status.ContainerStatuses.Count;
        result.PodStatus = pod.Status.Phase;

        foreach (V1ContainerStatus containerStatus in pod.Status.ContainerStatuses)
        {
            result.StatusContainers.Add(new StatusContainer { Name = containerStatus.Name, Image = containerStatus.Image, Ready = containerStatus.Ready });
        }

        return result;
    }

    /// <summary>
    ///     log 조회. deploy Id 기준
    ///     kubectl logs [pod id] 명령어와 동일한 정보를 가져옵니다.
    ///     pod가 비정상일때 수집되는 정보 없음.
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="timestamps">로그에 수집된 시간 표시 여부</param>
    /// <returns></returns>
    public async Task<LogsPod?> LogsPodAsync(string namespaceName, string deployId, bool timestamps = false, CancellationToken token = default)
    {
        V1Pod? pod = await NewestPodAsync(namespaceName, deployId, token).ConfigureAwait(false);
        if (pod is null)
        {
            return null;
        }

        return await CommonLogsPodAsync(namespaceName, pod, timestamps, token).ConfigureAwait(false);
    }


    /// <summary>
    /// Pod 정보 LogsPod 로 변환
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="pod"></param>
    /// <param name="timestamps"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private async Task<LogsPod> CommonLogsPodAsync(string namespaceName, V1Pod pod, bool timestamps = false, CancellationToken token = default)
    {
        LogsPod result = new();
        result.PodName = pod.Metadata.Name;

        bool keyExists = pod.Metadata.Labels.TryGetValue("app", out string? deployName);
        if (keyExists && deployName is not null)
        {
            result.DeployementName = deployName;
        }

        // pod 에 컨테이너가 여러개 있으면 각각 찾아서 출력이 가능함.
        // 입력안하면 하나의 컨테이너 로그만 출력됨.
        foreach (V1Container? container in pod.Spec.Containers)
        {
            List<string> containerLogs = new();
            using Stream stream = await _client.ReadNamespacedPodLogAsync(pod.Metadata.Name, namespaceName, container.Name, timestamps: timestamps, cancellationToken: token)
                .ConfigureAwait(false);
            using StreamReader reader = new(stream);
            while (!reader.EndOfStream)
            {
                string? line = await reader.ReadLineAsync(token).ConfigureAwait(false);
                if (line is not null)
                {
                    containerLogs.Add(line);
                }
            }


            result.ContainerLogs.Add(new ContainerLog { ContainerName = container.Name, LogDatas = containerLogs });
        }

        return result;
    }

    /// <summary>
    ///     Pod Event 정보 조회.
    ///     kubectl describe pod [pod-name] 조회 정보의 Event 정보만 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public async Task<EventsPod?> EventsPodAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        V1Pod? pod = await NewestPodAsync(namespaceName, deployId, token).ConfigureAwait(false);
        if (pod is null)
        {
            return null;
        }

        // corev1EventList 항목 정보
        /*
        involvedObject:
          apiVersion: v1
          fieldPath: spec.containers{postgres}
          kind: Pod
          name: dep-hc-solution-789cfd556d-5tk44
          namespaceProperty: hc-work
          resourceVersion: 1293845
          uid: 8d301307-0469-47ec-9988-d115ae60f77c
         */
        string fieldSelector = $"involvedObject.kind=Pod,involvedObject.name={pod.Metadata.Name}";

        EventsPod result = new();

        result.PodName = pod.Metadata.Name;

        bool keyExists = pod.Metadata.Labels.TryGetValue("app", out string? deployName);
        if (keyExists && deployName is not null)
        {
            result.DeployementName = deployName;
        }

        Corev1EventList events = await _client.CoreV1.ListNamespacedEventAsync(namespaceName, fieldSelector: fieldSelector, cancellationToken: token).ConfigureAwait(false);
        if (events is not null && events.Items.Count > 0)
        {
            foreach (Corev1Event ev in events.Items.OrderBy(x => x.LastTimestamp))
            {
                if (ev.LastTimestamp.HasValue)
                {
                    result.ContinerEvents.Add(new ContainerEvent { EventType = ev.Type, Reason = ev.Reason, Message = ev.Message, RecordTime = ev.LastTimestamp });
                }
            }
        }

        return result;
    }

    /// <summary>
    ///     pod 의 pod-template-hash 정보조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<string?> GetPodTemplateHashAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        // pod hash 정보 조회 테스트.
        // deployment 생성후 만들어지는 replicatset, pod 순서로 정보 조회
        // replicatset에는 owner 정보가 deployment 정보가 있음.
        // pod의 owner 정보가 replicatset 정보가 있음.
        // 쿠버네티스 demployment 이름이 정해지면 pod의 pod-template-hash는 항상 같은것으로 보임
        // pod 가 문제가 발생해서 새로 만들어져도 pod-template-hash 정보는 같음

        string labelSelector = $"app={_depPrefix}{deployId}";
        V1ReplicaSetList replicaSetList =
            await _client.AppsV1.ListNamespacedReplicaSetAsync(namespaceName, labelSelector: labelSelector, cancellationToken: token).ConfigureAwait(false);
        V1ReplicaSet? targetReplicaSet = replicaSetList.Items.FirstOrDefault();
        if (targetReplicaSet is null)
        {
            return null;
        }

        // deployId 기준의 pod 목록 조회
        V1PodList podList = await _client.CoreV1.ListNamespacedPodAsync(namespaceName, labelSelector: labelSelector, cancellationToken: token).ConfigureAwait(false);
        if (podList.Items.Count == 0)
        {
            return null;
        }

        // pod 목록에서 replica set의 owner가 일치하는 pod 가져옴
        V1Pod? targetPod = podList.Items.FirstOrDefault(x => x.Metadata.OwnerReferences.Any(x => x.Name == targetReplicaSet.Metadata.Name));
        if (targetPod is not null)
        {
            targetPod.Metadata.Labels.TryGetValue("pod-template-hash", out string? podHash);
            return podHash;
        }

        return null;
    }

    #endregion

    #region Service

    /// <summary>
    ///     서비스 목록 전체 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<V1ServiceList> ListServiceAsync(string namespaceName, CancellationToken token = default) =>
        _client.CoreV1.ListNamespacedServiceAsync(namespaceName, cancellationToken: token);

    /// <summary>
    ///     서비스 있는지 확인. service 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="serviceName">k8s 서비스 이름</param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> ExistsServiceAsync(string namespaceName, string serviceName, CancellationToken token = default)
    {
        V1ServiceList svcList = await ListServiceAsync(namespaceName, token).ConfigureAwait(false);
        if (svcList.Items.FirstOrDefault(x => x.Metadata.Name.Equals(serviceName, StringComparison.Ordinal)) is not null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     서비스 있는지 확인. deploy Id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public Task<bool> ExistsServiceByIdAsync(string namespaceName, string deployId, CancellationToken token = default) =>
        ExistsServiceAsync(namespaceName, $"{_svcPrefix}{deployId}", token);

    /// <summary>
    ///     서비스 정보 조회. service 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="serviceName"></param>
    /// <returns></returns>
    public async Task<V1Service?> ReadServiceAsync(string namespaceName, string serviceName, CancellationToken token = default)
    {
        if (await ExistsServiceAsync(namespaceName, serviceName, token).ConfigureAwait(false) == false)
        {
            return null;
        }

        return await _client.CoreV1.ReadNamespacedServiceAsync(serviceName, namespaceName, cancellationToken: token).ConfigureAwait(false);
    }

    /// <summary>
    ///     서비스 정보 조회. deploy id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public Task<V1Service?> ReadServiceByIdAsync(string namespaceName, string deployId, CancellationToken token = default) =>
        ReadServiceAsync(namespaceName, $"{_svcPrefix}{deployId}", token);

    /// <summary>
    ///     서비스 제거. service 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="serviceName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> DeleteServiceAsync(string namespaceName, string serviceName, CancellationToken token = default)
    {
        await _client.CoreV1.DeleteNamespacedServiceAsync(serviceName, namespaceName, cancellationToken: token).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    ///     서비스 제거. deploy id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> DeleteServiceByIdAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        await DeleteServiceAsync(namespaceName, $"{_svcPrefix}{deployId}", token).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    ///     서비스 생성.
    /// </summary>
    /// <param name="deployId"></param>
    /// <param name="cdConfig"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<V1Service> CreateServiceAsync(string deployId, DeliveryConfig cdConfig, CancellationToken token = default)
    {
        // Deployment yaml 정보. 다음과 같은 형태로 정보 입력후 생성 진행
        // 'spec.selector.app' 값은 deployment의 matadata.name과 일치해야 서로 연결됩니다.

        //    /*
        //    apiVersion: v1
        //    kind: Service
        //    metadata:
        //      name: svc-user1
        //    spec:
        //      selector:
        //        app: dep-user1
        //      type: NodePort
        //      ports: # 아래 셋 항목 전부 설정필요함.
        //      - nodePort: 30888 # 설정범위 30000-32767
        //        targetPort: 8080
        //        port: 8080
        //    */

        // service에 pod hash 정보 메핑하기 위해 pod hash 정보 추출
        // 넣지 않아도 문제 없음.
        string? podhash;

        // pod 확인 실패시 5회 까지 진행함
        string padTemplateHash = "pod-template-hash";
        int cnt = 0;
        do
        {
            // pod 정보 조회
            podhash = await GetPodTemplateHashAsync(cdConfig.NamespaceName, deployId, token).ConfigureAwait(false);
            await Task.Delay(500, token).ConfigureAwait(false);
            cnt++;
        } while (cnt < 5 && string.IsNullOrEmpty(podhash));

        // Service 이름은  fix: svc-[delivery.Id] 붙여서 생성할 예정
        string svcName = $"{_svcPrefix}{deployId}";
        // deployment 대상 label 입력
        string selectTarget = $"{_depPrefix}{deployId}";

        List<V1ServicePort> v1ServicePorts = new();
        foreach (DockerItem dockerInfo in cdConfig.DockerList)
        {
            // docker image 내 사용하는 port index 설정
            int index = 1;
            foreach (PortInfo portConfig in dockerInfo.Ports)
            {
                if (portConfig.IsExternalOpen == false)
                {
                    continue;
                }

                // nodePort 지정 안하면 자동으로 설정됨
                V1ServicePort svcPort = new V1ServicePort
                {
                    Name = $"{dockerInfo.Name}-{index++}",
                    TargetPort = portConfig.Number,
                    Port = portConfig.Number,
                    Protocol = portConfig.Protocol
                };
                if (portConfig.NodePort is not null)
                {
                    svcPort.NodePort = portConfig.NodePort;
                }

                v1ServicePorts.Add(svcPort);
            }
        }

        // pod hash 정보 있으면 넣어줌
        Dictionary<string, string> specSelector = new(StringComparer.Ordinal) { { "app", selectTarget } };
        if (podhash is not null)
        {
            specSelector.Add(padTemplateHash, podhash);
        }

        V1Service svcBody = new()
        {
            ApiVersion = V1Service.KubeApiVersion,
            Kind = V1Service.KubeKind,
            Metadata = new V1ObjectMeta
            {
                Name = svcName,
                NamespaceProperty = cdConfig.NamespaceName,
                Labels = new Dictionary<string, string>(StringComparer.Ordinal)
            },
            Spec = new V1ServiceSpec
            {
                Selector = specSelector,
                Type = "NodePort",
                Ports = v1ServicePorts,
                ExternalIPs = cdConfig.ExternalIp is not null ? new List<string> { cdConfig.ExternalIp } : new List<string>()
            }
        };

        return await _client.CreateNamespacedServiceAsync(svcBody, cdConfig.NamespaceName, cancellationToken: token).ConfigureAwait(false);
    }

    #endregion

    #region Deployment

    /// <summary>
    ///     Deployment 목록 조회
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public Task<V1DeploymentList> ListDeploymentAsync(string namespaceName, CancellationToken token = default) =>
        _client.AppsV1.ListNamespacedDeploymentAsync(namespaceName, cancellationToken: token);

    /// <summary>
    ///     Deployment 있는지 확인. deployment 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deploymentName"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<bool> ExistsDeploymentAsync(string namespaceName, string deploymentName, CancellationToken token = default)
    {
        V1DeploymentList deploymentList = await ListDeploymentAsync(namespaceName, token).ConfigureAwait(false);
        if (deploymentList.Items.FirstOrDefault(x => x.Metadata.Name.Equals(deploymentName, StringComparison.Ordinal)) is not null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    ///     Deployment 있는지 확인. deploy id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public Task<bool> ExistsDeploymentByIdAsync(string namespaceName, string deployId, CancellationToken token = default) =>
        ExistsDeploymentAsync(namespaceName, $"{_depPrefix}{deployId}", token);

    /// <summary>
    ///     Deployment 정보 조회. deployment 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deploymentName"></param>
    /// <returns></returns>
    public async Task<V1Deployment?> ReadDeploymentAsync(string namespaceName, string deploymentName, CancellationToken token = default)
    {
        if (!await ExistsDeploymentAsync(namespaceName, deploymentName, token).ConfigureAwait(false))
        {
            return null;
        }

        return await _client.AppsV1.ReadNamespacedDeploymentAsync(deploymentName, namespaceName, cancellationToken: token).ConfigureAwait(false);
    }

    /// <summary>
    ///     Deployment 정보 조회. deployment id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public Task<V1Deployment?> ReadDeploymentByIdAsync(string namespaceName, string deployId, CancellationToken token = default) =>
        ReadDeploymentAsync(namespaceName, $"{_depPrefix}{deployId}", token);

    /// <summary>
    ///     Deployment 제거. deployment 이름 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deploymentName"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDeploymentAsync(string namespaceName, string deploymentName, CancellationToken token = default)
    {
        await _client.AppsV1.DeleteNamespacedDeploymentAsync(deploymentName, namespaceName, cancellationToken: token).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    ///     Deployment 제거. deployment id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <returns></returns>
    public async Task<bool> DeleteDeploymentByIdAsync(string namespaceName, string deployId, CancellationToken token = default)
    {
        await DeleteDeploymentAsync(namespaceName, $"{_depPrefix}{deployId}", token).ConfigureAwait(false);
        return true;
    }

    /// <summary>
    ///     Deployment 생성
    /// </summary>
    /// <param name="deployId"></param>
    /// <param name="cdConfig"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<V1Deployment> CreateDeploymentAsync(string deployId, DeliveryConfig cdConfig, CancellationToken token = default)
    {
        if (cdConfig is null)
        {
            throw new ArgumentNullException(nameof(cdConfig));
        }

        /* 
        apiVersion: apps/v1
        kind: Deployment
        metadata:
          name: dep-user1
        spec:
          selector:
            matchLabels:
              app: dep-user1
          template:
            metadata:
              labels:
                app: dep-user1
            spec:
              containers:
              - name: bootcamp-app
                image: gcr.io/google-samples/kubernetes-bootcamp:v1
                imagePullPolicy: Always
                ports:
                - containerPort: 8080";
        */

        // Deployment 이름은  fix: dep-[delivery.Id] 붙여서 생성할 예정
        string deploymentName = $"{_depPrefix}{deployId}";
        Dictionary<string, string> setLabels = new(StringComparer.Ordinal) { { "app", deploymentName } };

        // deployment에 등록할 container 
        List<V1Container> setContainers = new();


        // 컨테이너 내에 volume mount 를 할경우 V1PodTemplateSpec 에 Volumes를 설정해줘야함.
        List<V1Volume> setVolumes = new();

        // docker 정보 쿠버네티스 deploy pod container 에 설정
        foreach (DockerItem dockerInfo in cdConfig.DockerList)
        {
            List<V1ContainerPort> containerPorts = new();
            foreach (PortInfo portConfig in dockerInfo.Ports)
            {
                containerPorts.Add(new V1ContainerPort(portConfig.Number, protocol: portConfig.Protocol));
            }

            List<V1EnvVar> env = new();
            if (dockerInfo.Environment is not null)
            {
                foreach (NameValueItme item in dockerInfo.Environment)
                {
                    env.Add(new V1EnvVar { Name = item.Name, Value = item.Value });
                }
            }

            List<V1VolumeMount> volumeMounts = new();

            // 볼륨 설정
            if (dockerInfo.Volumes is not null)
            {
                foreach (VolumeItem volume in dockerInfo.Volumes)
                {
                    volumeMounts.Add(new V1VolumeMount { Name = volume.Name, MountPath = volume.Value });

                    V1Volume addVolumeItem = new() { Name = volume.Name };

                    // host dir path 가 없으면 empty dir로 설정 있으면 hostpath로 설정
                    if (string.IsNullOrEmpty(volume.HostDirPath))
                    {
                        addVolumeItem.EmptyDir = new V1EmptyDirVolumeSource();
                    }
                    else
                    {
                        addVolumeItem.HostPath = new V1HostPathVolumeSource { Path = volume.HostDirPath };

                        addVolumeItem.HostPath.Type = volume.VolumeType switch
                        {
                            VolumeItemType.Directory => "DirectoryOrCreate",
                            VolumeItemType.File => "FileOrCreate",
                            _ => ""
                        };
                    }

                    setVolumes.Add(addVolumeItem);
                }
            }
            // resources:
            //   requests:
            //     ephemeral-storage: "2Gi"
            //   limits:
            //     ephemeral-storage: "4Gi"

            // 컨테이너 스토리지 limits 설정
            Dictionary<string, ResourceQuantity> setResource = new();

            if (dockerInfo.Storeage is not null)
            {
                setResource.Add("ephemeral-storage", new ResourceQuantity(dockerInfo.Storeage));
            }


            // https://kubernetes.io/docs/tasks/inject-data-application/define-command-argument-container/#running-a-command-in-a-shell 참고
            // command는 한줄만 받는거 같음.
            // command의 args 는 list<string>으로 받아서 첫번째 command 수행하는것으로 보임.
            setContainers.Add(new V1Container
            {
                Name = dockerInfo.Name,
                Image = dockerInfo.Image,
                ImagePullPolicy = "Always",
                Ports = containerPorts,
                Env = dockerInfo.Environment is not null ? env : null,
                // command args는 한쌍으로 생각하고 command가 있는경우에 args 넣도록 처리함.
                Command = dockerInfo.Command,
                Args = dockerInfo.Command is not null ? dockerInfo.Args : null,
                VolumeMounts = dockerInfo.Volumes is not null ? volumeMounts : null,
                SecurityContext = new V1SecurityContext { Privileged = true },
                Resources = dockerInfo.Storeage is not null ? new V1ResourceRequirements { Limits = setResource } : null
            });
        }

        // hosts file에 등록할 정보 추가.
        List<V1HostAlias> setHostAlias = new();
        foreach (IpItem item in cdConfig.HostList)
        {
            setHostAlias.Add(new V1HostAlias { Hostnames = new List<string> { item.HostName }, Ip = item.Ip });
        }

        V1Deployment depBody = new()
        {
            ApiVersion = $"{V1Deployment.KubeGroup}/{V1Deployment.KubeApiVersion}",
            Kind = V1Deployment.KubeKind,
            Metadata = new V1ObjectMeta
            {
                Name = deploymentName,
                NamespaceProperty = cdConfig.NamespaceName,
                Labels =  new Dictionary<string, string>(StringComparer.Ordinal)
            },
            Spec = new V1DeploymentSpec
            {
                Selector = new V1LabelSelector { MatchLabels = setLabels },
                Template = new V1PodTemplateSpec
                {
                    Metadata = new V1ObjectMeta { Labels = setLabels },
                    Spec = new V1PodSpec { Containers = setContainers, Volumes = setVolumes, HostAliases = setHostAlias }
                }
            }
        };

        return await _client.AppsV1.CreateNamespacedDeploymentAsync(depBody, cdConfig.NamespaceName, cancellationToken: token).ConfigureAwait(false);
    }

    /// <summary>
    ///     pod 에서 사용중인 port 정보 조회. deploy id 기준
    /// </summary>
    /// <param name="namespaceName"></param>
    /// <param name="deployId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<UsedPortInfo?> ReadUsedPortInfoAsync(string namespaceName, string deployId, CancellationToken cancellationToken = default)
    {
        V1Service? resultSvc = await ReadServiceByIdAsync(namespaceName, deployId, cancellationToken).ConfigureAwait(false);
        V1Deployment? resultDep = await ReadDeploymentByIdAsync(namespaceName, deployId, cancellationToken).ConfigureAwait(false);

        // id 기준으로 dep, svc 둘다 있어야함.
        if (resultDep is null || resultSvc is null)
        {
            return null;
        }

        UsedPortInfo result = new()
        {
            DeploymentName = resultDep.Metadata.Name,
            ServiceName = resultSvc.Metadata.Name,
            // service에 설정된 external ip 정보 가져옴.
            ExternalIp = resultSvc.Spec.ExternalIPs?.FirstOrDefault()
        };

        // deployment 등록시 설정한 port 정보와 service에서 설정한 포트포워드 설정 정보를 매칭해서 결과 가져옴.
        var portList = resultDep.Spec.Template.Spec.Containers.Select(x => new { ContainrName = x.Name, x.Ports });
        foreach (var item in portList)
        {
            ContainerPort portInfo = new() { ContainerName = item.ContainrName };

            foreach (V1ContainerPort? port in item.Ports)
            {
                V1ServicePort? svcPortInfo = resultSvc.Spec.Ports.FirstOrDefault(x => x.Port == port.ContainerPort);

                ContainerPortDetail portDetailInfo = new()
                {
                    Port = port.ContainerPort,
                    Protocol = port.Protocol,
                    ServicePortName = svcPortInfo?.Name,
                    NodePort = svcPortInfo?.NodePort
                };
                portInfo.ContainerPortDetailList.Add(portDetailInfo);
            }

            result.ContainerPortList.Add(portInfo);
        }

        return result;
    }

    #endregion

    #region Node
    /// <summary>
    /// node의 자원 사용정보 조회
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<IList<NodeCpuMemory>> ListNodeCpuMemoryAsync(CancellationToken token = default)
    {

        IList<NodeCpuMemory> nodeInfos = new List<NodeCpuMemory>();

        V1NodeList nodes = await _client.CoreV1.ListNodeAsync(cancellationToken: token).ConfigureAwait(false);
        // node 정보 조회시 cpu는 core 수 표시됨.
        // 메모리 정보는 Ki 단위로 표시되는듯.
        // ResourceQuantity에서 ToInt64 또는 ToDouble 사용하여 숫자로 변환가능
        /*
            cpu:
                format: DecimalSI
                value: 12
            ephemeral-storage:
                format: DecimalSI
                value: 972991057538
            hugepages-1Gi: &o0
                format: DecimalSI
                value: 0
            hugepages-2Mi: *o0
            memory:
                format: BinarySI
                value: 7724140Ki             
         */
        NodeCpuMemory addItem;
        foreach (V1Node node in nodes)
        {
            node.Status.Allocatable.TryGetValue("cpu", out ResourceQuantity cpuRQ);
            node.Status.Allocatable.TryGetValue("memory", out ResourceQuantity memoryRQ);

            addItem = new NodeCpuMemory() { NodeName = node.Metadata.Name, AllocatableCpu = cpuRQ.Value, AllocatableMemory = memoryRQ.Value, CpuCore = cpuRQ.ToInt32(), TotalMemory = memoryRQ.ToInt64() };
            nodeInfos.Add(addItem);
        }

        NodeMetricsList? metricsNodes = await ListMetricsNodesAsync(token).ConfigureAwait(false);

        if (metricsNodes is not null)
        {
            foreach (var node in metricsNodes.Items)
            {
                node.Usage.TryGetValue("cpu", out ResourceQuantity cpuRQ);
                node.Usage.TryGetValue("memory", out ResourceQuantity memoryRQ);

                NodeCpuMemory? targetNode = nodeInfos.FirstOrDefault(x => x.NodeName.Equals(node.Metadata.Name, StringComparison.Ordinal));
                if (targetNode is not null)
                {
                    targetNode.MetricsCpu = cpuRQ.Value;
                    targetNode.MetricsMemory = memoryRQ.Value;
                    targetNode.UsageCpu = cpuRQ.ToDouble();
                    targetNode.UsageMemory = memoryRQ.ToInt64();
                }
            }
        }

        return nodeInfos;
    }

    /// <summary>
    /// metrics 조회 node 기준
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public async Task<NodeMetricsList?> ListMetricsNodesAsync(CancellationToken token = default)
    {
        try
        {
            object metricsNodesResult = await _client.GetClusterCustomObjectAsync("metrics.k8s.io", "v1beta1", "nodes", "", cancellationToken: token).ConfigureAwait(false);

            string json = ((System.Text.Json.JsonElement)metricsNodesResult).GetRawText();
            return System.Text.Json.JsonSerializer.Deserialize<NodeMetricsList>(json);

        }
        catch (HttpOperationException hoe)
        {
            // k8s host에 metrics api가 설치 안되어 있으면 notfound 오류 발생함.
            if (hoe.Response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            throw;
        }
    }
    #endregion
}
