using DockerTest.Utils;
using Xunit.Abstractions;
using static System.Net.WebRequestMethods;

namespace DockerTest;

/// <summary>
/// docker buildx 를 사용한 빌드 테스트.
/// buildx -> 멀티 플랫폼 또는 다중 빌드를 위한 tool
/// </summary>
public class DockerBuildxTest
{
    internal readonly ITestOutputHelper _output;

    public DockerBuildxTest(ITestOutputHelper output)
    {
        _output = output;

        BuildxBuildCommand =
       $"docker buildx build -t {_repositoryServer}/group/test-udpserver:0.0.1 -f .\\Dockerfiles\\UdpServerDockerFile.txt --cache-to type=local,dest={_dockerCacheDirPath} --cache-from type=local,src={_dockerCacheDirPath} {_solutionFolder} --push --provenance=false";
        BuildxInspectCommand = $"docker buildx inspect {_buildxInspectName}";
        BuildxCreateCommand = $"docker buildx create --use --driver=docker-container --name={_buildxInspectName} --config /https/buildkit-build.toml";

        // docker client 사용하지 않고 command 로 로그인진행 > build 및 push 진행하기 위해 command 사용함.
        RepositoryLoginCommand = $"docker login -u {_repositoryId} -p {_repositoryPw} {_repositoryServer}";
    }

    private string _repositoryServer = "www.test.com:33333";
    private string _repositoryId = "teater";
    private string _repositoryPw = "my-pw";
    private string _solutionFolder = @"C:\Users\mhkim\source\repos\dotnet-study\UdpServer\";

    private string _dockerCacheDirPath = "/data/docker-cache";
    private string _buildxInspectName = "mkim-build";

    private string BuildxBuildCommand { get; set; }
    private string BuildxInspectCommand { get; set; }
    private string BuildxCreateCommand { get; set; }
    private string RepositoryLoginCommand { get; set; }

    [Fact]
    public async void Buildx_inspect_체크()
    {
        // 레파지토리에 push 전에 수행해줘야함.
        // docker client를 사용할경우 인증 객체에 인증정보 넣어서 진행가능.
        // 없으면 ERROR: 메세지 발생
        var result = await RunCommand.RunAsync(BuildxInspectCommand);
        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void Buildx_inspect_생성()
    {
        CreateBuildxConfig();

        var result = await RunCommand.RunAsync(BuildxCreateCommand);
        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    private void CreateBuildxConfig()
    {
        // "[registry.\"[[DockerRegistryHost]]\"]\n  ca=[\"/https/host-ssl.crt\"]\n" > /https/buildkit-hc-build.toml
        // 인증서가 있는경우 인증서 위치 설정해줘야함. 해당인증서는 buildx buildkit 구성중 사용됨
        string configText = $@"[registry.{_repositoryServer}]
ca=[/https/host-ssl.crt]";

        if (Directory.Exists("/https") == false) Directory.CreateDirectory("/https");

        System.IO.File.WriteAllText("/https/buildkit-build.toml", configText);
    }

    [Fact]
    public async void 레파지토리_로그인()
    {
        // 레파지토리에 push 전에 수행해줘야함.
        // docker client를 사용할경우 인증 객체에 인증정보 넣어서 진행가능.
        var result = await RunCommand.RunAsync(RepositoryLoginCommand);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void 빌드및배포()
    {
        // 빌드 및 push 까지 할려면 tag에 레파지토리 위치 및 tag 까지 붙여줘야함.
        // ex) www.test.com:33333/group/test-udpserver:0.0.1

        Buildx_inspect_생성();
        레파지토리_로그인();

        var result = await RunCommand.RunAsync(BuildxBuildCommand);
        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public void Test()
    {
        _output.WriteLine(BuildxBuildCommand);
    }
    
}
