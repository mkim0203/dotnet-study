using DockerTest.Utils;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace DockerTest;

/// <summary>
/// docker 명령어를 수행할수 있는 환경에서 테스트 가능.
/// ex) docker desktop 설치
/// </summary>
public class DockerBuildTest
{
    internal readonly ITestOutputHelper _output;

    public DockerBuildTest(ITestOutputHelper output)
    {
        _output = output;

        BuildCmd = $"docker build -t test-udpserver -f .\\Dockerfiles\\UdpServerDockerFile.txt {_solutionFolder}";
        TagChangeCmd = $"docker image tag test-udpserver {_repositoryServer}/group/test-udpserver:0.0.1";
        PushCmd = $"docker push {_repositoryServer}/group/test-udpserver:0.0.1";
        RepositoryLoginCmd = $"docker login -u {_repositoryId} -p '{_repositoryPw}' {_repositoryServer}";
    }
    private string _repositoryServer = "www.test.com:33333";
    private string _repositoryId = "teater";
    private string _repositoryPw = "my-pw";
    private string _solutionFolder = @"C:\Users\mhkim\source\repos\dotnet-study\UdpServer\";

    private string BuildCmd { get; set; } 
    private string TagChangeCmd { get; set; } 
    private string PushCmd { get; set; } 
    private string RepositoryLoginCmd { get; set; }


    [Fact]
    public async void 빌드()
    {
        // -t docker image 이름
        // -f docker build에 쓰일 docker path
        // {_solutionFolder} => 빌드 진행시 수행할 working directory
        // working 디렉토리 위치에 따라 docker file 을 구성해줘야함.
        var result = await RunCommand.RunAsync(BuildCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
        
    }

    [Fact]
    public async void 레파지토리_로그인()
    {
        // 레파지토리에 push 전에 수행해줘야함.
        // docker client를 사용할경우 인증 객체에 인증정보 넣어서 진행가능.
        var result = await RunCommand.RunAsync(RepositoryLoginCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void Tag_변경()
    {
        // tag에 push 할 레파지토리 설정필요
        // 레파지토리에  push 진행시 설정해줘야 push 진행할수 있음.
        // ex)host.test.com:30000/test-group/test-udpserver:0.0.1
        // 변경이라기 보다는 복제기능임....
        var result = await RunCommand.RunAsync(TagChangeCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void Push()
    {
        // 입력된 레포지토리 위치로 push 작업 진행. 단 login 진행 후 해야함.
        var loginResult = await RunCommand.RunAsync(RepositoryLoginCmd);
        var result = await RunCommand.RunAsync(PushCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void 빌드및배포진행()
    {
        빌드();
        Tag_변경();
        레파지토리_로그인();
        Push();
    }
}