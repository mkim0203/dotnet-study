using k8s;
using k8s.Models;
using K8sLibrary.Model;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using Xunit.Abstractions;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace K8sTest;

public class UnitTest1 : K8sCommonSetting
{
    public UnitTest1(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task �����׽�Ʈ()
    {
        // ������ Ư�� api�� ��û�ؾ� ����ó�� Ȯ�ΰ���

        using Kubernetes client = Connection(false);
        _output.WriteLine("�����׽�Ʈ ��û");
        V1APIGroupList result = await client.Apis.GetAPIVersionsAsync().ConfigureAwait(false);
        _output.WriteLine(result.ApiVersion);
    }

    [Fact]
    public async Task ListNode()
    {
        try
        {
            using Kubernetes client = Connection();
            V1NodeList nodes = await client.CoreV1.ListNodeAsync().ConfigureAwait(false);

            foreach (V1Node node in nodes.Items)
            {
                _output.WriteLine(node.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     namespace ��ȸ
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task ListNamespace()
    {
        try
        {
            using Kubernetes client = Connection();
            V1NamespaceList namespaces = await client.CoreV1.ListNamespaceAsync().ConfigureAwait(false);
            foreach (V1Namespace ns in namespaces.Items)
            {
                _output.WriteLine(ns.Metadata.Name);
                //V1PodList list = await client.CoreV1.ListNamespacedPodAsync(ns.Metadata.Name);
                //foreach (V1Pod item in list.Items)
                //{
                //    output.WriteLine(item.Metadata.Name);
                //}
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     namespace ����
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNamespace()
    {
        V1Namespace nsBody = new() { Metadata = new V1ObjectMeta { Name = DefaultNamespace } };

        try
        {
            using Kubernetes client = Connection();

            V1NamespaceList nss = await client.CoreV1.ListNamespaceAsync().ConfigureAwait(false);
            if (nss is not null && nss.Items.FirstOrDefault(x => x.Metadata.Name.Equals(DefaultNamespace, StringComparison.Ordinal)) is null)
            {
                // ������û
                _output.WriteLine("Namespace ����");
                await client.CoreV1.CreateNamespaceAsync(nsBody).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     rc ��ȸ
    /// </summary>
    [Fact]
    public async Task ListReplicationController()
    {
        // rc�� ���� ���Ҽ� ����.
        try
        {
            Kubernetes client = Connection();
            V1ReplicationControllerList rcs = await client.CoreV1.ListReplicationControllerForAllNamespacesAsync().ConfigureAwait(false);
            foreach (V1ReplicationController rc in rcs.Items)
            {
                _output.WriteLine(rc.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }


    /// <summary>
    ///     pod ��ȸ
    /// </summary>
    [Fact]
    public async Task ListPod()
    {
        try
        {
            Kubernetes client = Connection();
            V1PodList pods = await client.CoreV1.ListPodForAllNamespacesAsync().ConfigureAwait(false);
            foreach (V1Pod pod in pods.Items)
            {
                _output.WriteLine(pod.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    [Fact]
    public async Task StatusPod()
    {
        try
        {
            Kubernetes client = Connection();
            V1PodList pods = await client.CoreV1.ListNamespacedPodAsync(DefaultNamespace).ConfigureAwait(false);
            foreach (V1Pod pod in pods.Items)
            {
                _output.WriteLine($"POD : {pod.Metadata.Name}");
                //_output.WriteLine(GetYamlString(pod));

                string? deployName;
                bool keyExists = pod.Metadata.Labels.TryGetValue("app", out deployName);
                if (keyExists)
                {
                    _output.WriteLine($"Deployment Name : {deployName}");
                }

                _output.WriteLine($"Container Count : {pod.Status.ContainerStatuses.Count}, Container Status : {pod.Status.Phase}");
                foreach (V1ContainerStatus containerStatus in pod.Status.ContainerStatuses)
                {
                    //_output.WriteLine(GetYamlString(containerStatus));
                    _output.WriteLine($"Name : {containerStatus.Name}, Image : {containerStatus.Image} Ready : {containerStatus.Ready} ");
                }

                _output.WriteLine("***");
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     pod event ���� ��ȸ
    ///     pod running ���� �߻��� ���� ��ȸ�Ͽ� ����ɵ�.
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task PodEvent()
    {
        Kubernetes client = Connection();

        V1PodList pods = await client.CoreV1.ListNamespacedPodAsync(DefaultNamespace).ConfigureAwait(false);
        foreach (V1Pod pod in pods.Items)
        {
            _output.WriteLine($"POD : {pod.Metadata.Name}");
            // corev1EventList �׸� ����
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

            Corev1EventList events = await client.CoreV1.ListNamespacedEventAsync(DefaultNamespace, fieldSelector: fieldSelector).ConfigureAwait(false);
            foreach (Corev1Event ev in events.Items.OrderBy(x => x.LastTimestamp))
            {
                //_output.WriteLine(GetYamlString(ev));
            }

            _output.WriteLine("**********");
        }
    }

    /// <summary>
    ///     ���� namespace ���� pod ��ȸ
    /// </summary>
    [Fact]
    public async Task ListNamespacedPod()
    {
        try
        {
            Kubernetes client = Connection();
            V1PodList pods = await client.CoreV1.ListNamespacedPodAsync(DefaultNamespace).ConfigureAwait(false);
            foreach (V1Pod pod in pods.Items)
            {
                _output.WriteLine(pod.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     svc ��ȸ
    /// </summary>
    [Fact]
    public async Task ListService()
    {
        try
        {
            Kubernetes client = Connection();
            V1ServiceList svcs = await client.CoreV1.ListServiceForAllNamespacesAsync().ConfigureAwait(false);
            foreach (V1Service svc in svcs.Items)
            {
                _output.WriteLine(svc.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     svc ��ȸ namespace ����
    /// </summary>
    [Fact]
    public async Task ListNamespacedService()
    {
        try
        {
            using Kubernetes client = Connection();
            V1ServiceList svcs = await client.CoreV1.ListNamespacedServiceAsync(DefaultNamespace).ConfigureAwait(false);
            foreach (V1Service svc in svcs.Items)
            {
                _output.WriteLine(svc.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     svc ����
    /// </summary>
    [Fact]
    public async Task CreateNamespacedService()
    {
        string svcName = "svc-test-user2";
        // deployment ��� label �Է�
        string selectTarget = "dep-test-user2";
        V1Service svcBody = new()
        {
            ApiVersion = V1Service.KubeApiVersion,
            Kind = V1Service.KubeKind,
            Metadata = new V1ObjectMeta { Name = svcName, NamespaceProperty = DefaultNamespace },
            Spec = new V1ServiceSpec
            {
                Selector = new Dictionary<string, string> { { "app", selectTarget } },
                Type = "NodePort",
                Ports = new List<V1ServicePort>
                {
                    new() { Name = "bootcamp", NodePort = 30080, TargetPort = 8080, Port = 8080 }
                    //new V1ServicePort() { Name="aspnet", NodePort = 30081, TargetPort = 80, Port = 80 }
                    //new V1ServicePort() { Name="aspnet", TargetPort = 80, Port = 80 }
                },
                //ExternalIPs = new List<string> { "127.0.0.1" }
            }
        };

        // Service yaml ����
        /*
         * apiVersion: v1
kind: Service
metadata:
name: svc-user1
spec:
selector:
app: dev-user1
type: NodePort
ports: # �Ʒ� �� �׸� ���� �����ʿ���.
- nodePort: 30888 # �������� 30000-32767
targetPort: 8080
port: 8080
         */

        try
        {
            using Kubernetes client = Connection();

            // exists üũ�� read�� �ϸ�ȵ� ������ notfound ���� �߻���.
            V1ServiceList svcs = await client.CoreV1.ListNamespacedServiceAsync(DefaultNamespace).ConfigureAwait(false);
            if (svcs is not null && svcs.Items.FirstOrDefault(x => x.Metadata.Name.Equals(svcName, StringComparison.Ordinal)) is null)
            {
                //_output.WriteLine(GetYamlString(svcBody));
                _output.WriteLine("service ����");

                // not found
                V1Service result = await client.CreateNamespacedServiceAsync(svcBody, DefaultNamespace).ConfigureAwait(false);
                if (result is not null)
                {
                    _output.WriteLine(result.Metadata.Name);
                }
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     svc ����
    /// </summary>
    [Fact]
    public async Task DeleteNamespacedService()
    {
        string svcName = "svc-test-user2";

        try
        {
            using Kubernetes client = Connection();

            // ��ü �ִ��� Ȯ�λ춧 Read �� �ƴ� list�����. read�� ������ notfound ���� �߻���.
            V1ServiceList svcs = await client.CoreV1.ListNamespacedServiceAsync(DefaultNamespace).ConfigureAwait(false);
            if (svcs is not null && svcs.Items.FirstOrDefault(x => x.Metadata.Name.Equals(svcName, StringComparison.Ordinal)) is not null)
            {
                // svc ������ ���� ����
                V1Service result = await client.CoreV1.DeleteNamespacedServiceAsync(svcName, DefaultNamespace).ConfigureAwait(false);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     deployement ��ȸ
    /// </summary>
    [Fact]
    public async Task ListDeploymentForAllNamespaces()
    {
        try
        {
            using Kubernetes client = Connection();
            V1DeploymentList deps = await client.AppsV1.ListDeploymentForAllNamespacesAsync().ConfigureAwait(false);
            foreach (V1Deployment dep in deps.Items)
            {
                _output.WriteLine(dep.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     namespace �������� deployment ��ȸ
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task ListNamespacedDeployment()
    {
        // ListDeploymentForAllNamespaces() �� �ٸ��� Ȯ�� �ؾ��ҵ�
        try
        {
            using Kubernetes client = Connection();
            V1DeploymentList deps = await client.AppsV1.ListNamespacedDeploymentAsync(DefaultNamespace).ConfigureAwait(false);
            foreach (V1Deployment dep in deps.Items)
            {
                _output.WriteLine(dep.Metadata.Name);
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }


    /// <summary>
    ///     Deployment ����
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateNamespacedDeployment()
    {
        string deploymentName = "dep-test-user2";
        Dictionary<string, string> labels = new() { { "app", deploymentName } };
        List<V1Container> con = new()
        {
            new V1Container
            {
                Name = "bootcamp",
                Image = "gcr.io/google-samples/kubernetes-bootcamp:v1",
                ImagePullPolicy = "Always",
                Ports = new List<V1ContainerPort> { new() { ContainerPort = 8080 } }
            },
            new V1Container
            {
                Name = "aspnet",
                Image = "mcr.microsoft.com/dotnet/samples:aspnetapp",
                ImagePullPolicy = "Always",
                Ports = new List<V1ContainerPort> { new() { ContainerPort = 80 } }
            }
        };

        V1Deployment depBody = new()
        {
            ApiVersion = $"{V1Deployment.KubeGroup}/{V1Deployment.KubeApiVersion}",
            Kind = V1Deployment.KubeKind,
            Metadata = new V1ObjectMeta { Name = deploymentName, NamespaceProperty = DefaultNamespace },
            Spec = new V1DeploymentSpec
            {
                Selector = new V1LabelSelector { MatchLabels = labels },
                Template = new V1PodTemplateSpec { Metadata = new V1ObjectMeta { Labels = labels }, Spec = new V1PodSpec { Containers = con } }
            }
        };
        //.. Deployment yaml ����
        //apiVersion: apps/v1
        //kind: Deployment
        //metadata:
        //  name: dev-user1
        //spec:
        //  selector:
        //    matchLabels:
        //      app: dev-user1
        //  template:
        //    metadata:
        //      labels:
        //        app: dev-user1
        //    spec:
        //      containers:
        //      - name: bootcamp-app
        //        image: gcr.io/google-samples/kubernetes-bootcamp:v1
        //        imagePullPolicy: Always
        //        ports:
        //        - containerPort: 8080";

        try
        {
            using Kubernetes client = Connection();
            V1DeploymentList deps = await client.AppsV1.ListNamespacedDeploymentAsync(DefaultNamespace).ConfigureAwait(false);
            if (deps is not null && deps.Items.FirstOrDefault(x => x.Metadata.Name.Equals(deploymentName, StringComparison.Ordinal)) is null)
            {
                //_output.WriteLine(GetYamlString(depBody));

                _output.WriteLine("���� deployment");
                // not found
                V1Deployment result = await client.AppsV1.CreateNamespacedDeploymentAsync(depBody, DefaultNamespace).ConfigureAwait(false);
                if (result is not null)
                {
                    _output.WriteLine(result.Metadata.Name);
                }
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     Deployment ����
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task DeleteNamespacedDeployment()
    {
        string deploymentName = "dep-test-user2";

        try
        {
            using Kubernetes client = Connection();


            // ��ü �ִ��� Ȯ�λ춧 Read �� �ƴ� list�����. read�� ������ notfound ���� �߻���.
            V1DeploymentList deps = await client.AppsV1.ListNamespacedDeploymentAsync(DefaultNamespace).ConfigureAwait(false);
            if (deps is not null && deps.Items.FirstOrDefault(x => x.Metadata.Name.Equals(deploymentName, StringComparison.Ordinal)) is not null)
            {
                V1Status status = await client.AppsV1.DeleteNamespacedDeploymentAsync(deploymentName, DefaultNamespace).ConfigureAwait(false);
                if (status is not null)
                {
                    _output.WriteLine(status.Status);
                }
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    /// <summary>
    ///     pod �� container �α� ����
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task ReadLogsPod()
    {
        using Kubernetes client = Connection();

        //var podName = "dep-test-asp-7cfd5f8575-mjmlw";
        //const string podName = "dep-backendapi-07-7cc88fd969-w8vbs";
        //string namespaceName = "hc-system";

        // pod �� �����̳ʰ� ������ ������ ���� ã�Ƽ� ����� ������.
        // �Է¾��ϸ� �ϳ��� �����̳� �α׸� ��µ�.

        //V1Pod pod = await client.CoreV1.ReadNamespacedPodAsync(podName, namespaceName).ConfigureAwait(false);
        V1PodList pods = await client.CoreV1.ListNamespacedPodAsync(DefaultNamespace).ConfigureAwait(false);
        foreach (V1Pod pod in pods.Items)
        {
            _output.WriteLine($"POD : {pod.Metadata.Name}");
            foreach (V1Container? container in pod.Spec.Containers)
            {
                _output.WriteLine($"** {container.Name} **");
                //tailLines: 2
                Stream stream = await client.ReadNamespacedPodLogAsync(pod.Metadata.Name, DefaultNamespace, container.Name, timestamps: true).ConfigureAwait(false);
                await using (stream.ConfigureAwait(false))
                {
                    using (StreamReader reader = new(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            string? line = await reader.ReadLineAsync().ConfigureAwait(false);
                            _output.WriteLine(line);
                        }
                    }
                }
            }

            _output.WriteLine("**********");
        }
    }

    [Fact]
    public void TcpClientTest()
    {
        try
        {
            _output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture));
            const string serverIp = "kubernetes.docker.internal";
            int port = 32159;
            //string message = "TEST";
            using TcpClient client = new();

            bool connectionComplited = client.ConnectAsync(serverIp, port).Wait(5000);
            _output.WriteLine($"���� �Ϸ�? : {connectionComplited}");

            //// Translate the passed message into ASCII and store it as a Byte array.
            //Byte[] data = System.Text.Encoding.UTF8.GetBytes(message);

            //// Get a client stream for reading and writing.
            ////  Stream stream = client.GetStream();

            //NetworkStream stream = client.GetStream();

            //// Send the message to the connected TcpServer.
            //stream.Write(data, 0, data.Length);

            //_output.WriteLine("Sent: {0}", message);

            //// Receive the TcpServer.response.

            //stream.Close();

            client.Dispose();
        }
        catch (SocketException se)
        {
            _output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _output.WriteLine($"SocketException => {se.Message}");
        }
        catch (AggregateException ae)
        {
            _output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _output.WriteLine($"AggregateException => {ae.Message}");
        }
        catch (Exception ex)
        {
            _output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            WriteException(ex);
            throw;
        }

        _output.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
    }


    [Fact]
    public void YamlConvert()
    {
        string deploymentConfig = @"apiVersion: apps/v1
kind: Deployment
metadata:
  name: dev-user1
spec:
  selector:
    matchLabels:
      app: dev-user1
  template:
    metadata:
      labels:
        app: dev-user1
    spec:
      containers:
      - name: bootcamp-app
        image: gcr.io/google-samples/kubernetes-bootcamp:v1
        imagePullPolicy: Always
        ports:
        - containerPort: 8080";

        IDeserializer deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance) // see height_in_inches in sample yml 
            .Build();

        V1Deployment dep = deserializer.Deserialize<V1Deployment>(deploymentConfig);
        _output.WriteLine(dep.Metadata.Name);
    }


    [Fact]
    public async Task GetPodTemplatehash()
    {
        try
        {
            // pod hash ���� ��ȸ �׽�Ʈ.
            // deployment ������ ��������� replicatset, pod ������ ���� ��ȸ
            // replicatset���� owner ������ deployment ������ ����.
            // pod�� owner ������ replicatset ������ ����.

            using Kubernetes client = Connection();
            //K8sUnitWorker worker = new(client);

            V1DeploymentList deps = await client.AppsV1.ListNamespacedDeploymentAsync(DefaultNamespace).ConfigureAwait(false);
            foreach (V1Deployment dep in deps.Items)
            {
                _output.WriteLine($"Deployment name => {dep.Metadata.Name}");

                V1ReplicaSetList? replicaSetList =
                    await client.AppsV1.ListNamespacedReplicaSetAsync(DefaultNamespace, labelSelector: $"app={dep.Metadata.Name}").ConfigureAwait(false);
                V1ReplicaSet? target = replicaSetList.Items.FirstOrDefault();
                if (target is null)
                {
                    _output.WriteLine("not found replicaset");
                    return;
                }

                _output.WriteLine($"ReplicaSet name => {target.Metadata.Name}");

                // deployId ������ pod ��� ��ȸ
                V1PodList podList = await client.CoreV1.ListNamespacedPodAsync(DefaultNamespace, labelSelector: $"app={dep.Metadata.Name}").ConfigureAwait(false);
                if (podList.Items.Count == 0)
                {
                    _output.WriteLine("not found pod");
                    return;
                }


                // ���� pod �� ������ �������� ������ pod�� �������� ������ ������.
                //V1Pod pod = podList.Items.Count > 1 ? podList.Items.OrderByDescending(x => x.Metadata.CreationTimestamp).First() : podList.Items.First();

                V1Pod? targetPod = podList.Items.FirstOrDefault(x => x.Metadata.OwnerReferences.Any(x => string.Equals(x.Name, target.Metadata.Name, StringComparison.Ordinal)));
                if (targetPod is not null)
                {
                    _output.WriteLine($"Pod name => {targetPod.Metadata.Name}");
                    targetPod.Metadata.Labels.TryGetValue("pod-template-hash", out string? podHash);
                    _output.WriteLine(podHash);
                }

                //var replicaset = lists.Items.FirstOrDefault(x => x.Metadata.OwnerReferences.Any(x => x.Name == depId));

                _output.WriteLine("**********");
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    [Fact]
    public async Task GetNodeDescribe()
    {
        try
        {
            using Kubernetes client = Connection();


            V1NodeList nodes = await client.CoreV1.ListNodeAsync().ConfigureAwait(false);

            // node ���� ��ȸ�� cpu�� core �� ǥ�õ�.
            // �޸� ������ Ki ������ ǥ�õǴµ�.
            // ResourceQuantity���� ToInt64 �Ǵ� ToDouble ����Ͽ� ���ڷ� ��ȯ����
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
            foreach (V1Node node in nodes)
            {
                node.Status.Allocatable.TryGetValue("cpu", out ResourceQuantity cpuRQ);
                node.Status.Allocatable.TryGetValue("memory", out ResourceQuantity memoryRQ);

                _output.WriteLine($"{node.Metadata.Name} {cpuRQ.Value} {memoryRQ.Value} {memoryRQ.ToInt64()}");

                //_output.WriteLine(GetYamlString(node));
            }

            //var events = await client.CoreV1.ListEventForAllNamespacesAsync().ConfigureAwait(false);

            //Corev1EventList events = await client.CoreV1.ListNamespacedEventAsync(namespaceName, fieldSelector: fieldSelector).ConfigureAwait(false);

            //foreach (Corev1Event ev in events.Items.OrderBy(x => x.LastTimestamp))
            //{
            //    _output.WriteLine(GetYamlString(ev));
            //    //_output.WriteLine($"{ev.LastTimestamp.Value.ToLocalTime()}  {ev.Message}");
            //}
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }


    [Fact]
    public async Task GetTopNodes()
    {
        try
        {
            using Kubernetes client = Connection();
            //+customObject    ValueKind = Object : "{"kind":"NodeMetricsList","apiVersion":"metrics.k8s.io / v1beta1","metadata":{},"items":[{"metadata":{"name":"docker - desktop","creationTimestamp":"2023 - 06 - 09T06: 16:01Z","labels":{"beta.kubernetes.io / arch":"amd64","beta.kubernetes.io / os":"linux","kubernetes.io / arch":"amd64","kubernetes.io / hostname":"docker - desktop","kubernetes.io / os":"linux","node - role.kubernetes.io / control - plane":"","node.kubernetes.io / exclude - from - external - load - balancers":""}},"timestamp":"2023 - 06 - 09T06: 15:40Z","window":"11.069s","usage":{"cpu":"138824473n","memory":"5074568Ki"}}]}"    object { System.Text.Json.JsonElement}
            object customObject = await client.GetClusterCustomObjectAsync("metrics.k8s.io", "v1beta1", "nodes", "");

            string json = ((JsonElement)customObject).GetRawText();
            NodeMetricsList? data = JsonSerializer.Deserialize<NodeMetricsList>(json);


            // �� ���̺귯�� ���� nodes�� ��뷮 ��ȸ�� cpu�� 'n' ����� ǥ�õ�.
            // ��ɾ� ����ϸ� 'm' ������ ǥ�õ� ����ũ���ε�.
            // �޸� ������ Ki ������ ǥ�õǴµ�.
            // ResourceQuantity���� ToInt64 �Ǵ� ToDouble ����Ͽ� ���ڷ� ��ȯ����
            /*
            usage:
                cpu:
                    format: DecimalSI
                    value: 156287621n
                memory:
                    format: BinarySI
                    value: 5195528Ki
             */
            foreach (NodeMetrics? node in data.Items)
            {
                node.Usage.TryGetValue("cpu", out ResourceQuantity cpuRQ);
                node.Usage.TryGetValue("memory", out ResourceQuantity memoryRQ);

                _output.WriteLine($"{node.Metadata.Name} {cpuRQ.Value} {cpuRQ.ToDouble()} {memoryRQ.Value} {memoryRQ.ToInt64()}");

                //_output.WriteLine(GetYamlString(node));
            }

            //ResourceQuantity cpudata = data.Items.First().Usage["cpu"];
            //_output.WriteLine($"{cpudata.ToDecimal()} {cpudata.ToDouble()} ");


            //_output.WriteLine(GetYamlString(data));
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    [Fact]
    public async Task GetNode_����()
    {
        try
        {
            using Kubernetes client = Connection();

            V1NodeList nodes = await client.CoreV1.ListNodeAsync().ConfigureAwait(false);

            IList<NodeCpuMemory> nodeInfos = new List<NodeCpuMemory>();

            // node ���� ��ȸ�� cpu�� core �� ǥ�õ�.
            // �޸� ������ Ki ������ ǥ�õǴµ�.
            // ResourceQuantity���� ToInt64 �Ǵ� ToDouble ����Ͽ� ���ڷ� ��ȯ����
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
            foreach (V1Node? node in nodes)
            {
                node.Status.Allocatable.TryGetValue("cpu", out ResourceQuantity cpuRQ);
                node.Status.Allocatable.TryGetValue("memory", out ResourceQuantity memoryRQ);

                _output.WriteLine($"{node.Metadata.Name} {cpuRQ.Value} {memoryRQ.Value} {memoryRQ.ToInt64()}");

                //_output.WriteLine(GetYamlString(node));
                addItem = new NodeCpuMemory
                {
                    NodeName = node.Metadata.Name,
                    AllocatableCpu = cpuRQ.Value,
                    AllocatableMemory = memoryRQ.Value,
                    CpuCore = cpuRQ.ToInt32(),
                    TotalMemory = memoryRQ.ToInt64()
                };
                nodeInfos.Add(addItem);
            }


            //+customObject    ValueKind = Object : "{"kind":"NodeMetricsList","apiVersion":"metrics.k8s.io / v1beta1","metadata":{},"items":[{"metadata":{"name":"docker - desktop","creationTimestamp":"2023 - 06 - 09T06: 16:01Z","labels":{"beta.kubernetes.io / arch":"amd64","beta.kubernetes.io / os":"linux","kubernetes.io / arch":"amd64","kubernetes.io / hostname":"docker - desktop","kubernetes.io / os":"linux","node - role.kubernetes.io / control - plane":"","node.kubernetes.io / exclude - from - external - load - balancers":""}},"timestamp":"2023 - 06 - 09T06: 15:40Z","window":"11.069s","usage":{"cpu":"138824473n","memory":"5074568Ki"}}]}"    object { System.Text.Json.JsonElement}
            object metricsNodesResult = await client.GetClusterCustomObjectAsync("metrics.k8s.io", "v1beta1", "nodes", "");

            string json = ((JsonElement)metricsNodesResult).GetRawText();
            NodeMetricsList? metricsNodes = JsonSerializer.Deserialize<NodeMetricsList>(json);

            foreach (NodeMetrics? node in metricsNodes.Items)
            {
                node.Usage.TryGetValue("cpu", out ResourceQuantity cpuRQ);
                node.Usage.TryGetValue("memory", out ResourceQuantity memoryRQ);

                _output.WriteLine($"{node.Metadata.Name} {cpuRQ.Value} {cpuRQ.ToDouble()} {memoryRQ.Value} {memoryRQ.ToInt64()}");

                //_output.WriteLine(GetYamlString(node));

                NodeCpuMemory? targetNode = nodeInfos.FirstOrDefault(x => x.NodeName.Equals(node.Metadata.Name, StringComparison.Ordinal));
                if (targetNode is not null)
                {
                    targetNode.MetricsCpu = cpuRQ.Value;
                    targetNode.MetricsMemory = memoryRQ.Value;
                    targetNode.UsageCpu = cpuRQ.ToDouble();
                    targetNode.UsageMemory = memoryRQ.ToInt64();
                }
            }

            foreach (NodeCpuMemory info in nodeInfos)
            {
                _output.WriteLine(info.ToString());
            }
        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }

    [Fact]
    public async Task Ingress_��ȸ()
    {
        try
        {
            using Kubernetes client = Connection();


            // ��ü �ִ��� Ȯ�λ춧 Read �� �ƴ� list�����. read�� ������ notfound ���� �߻���.
            var ingressList = await client.ListIngressClassAsync().ConfigureAwait(false);

        }
        catch (Exception ex)
        {
            WriteException(ex);
            throw;
        }
    }
}