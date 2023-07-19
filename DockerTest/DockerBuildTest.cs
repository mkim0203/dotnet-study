using DockerTest.Utils;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace DockerTest;

/// <summary>
/// docker ��ɾ �����Ҽ� �ִ� ȯ�濡�� �׽�Ʈ ����.
/// ex) docker desktop ��ġ
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
    public async void ����()
    {
        // -t docker image �̸�
        // -f docker build�� ���� docker path
        // {_solutionFolder} => ���� ����� ������ working directory
        // working ���丮 ��ġ�� ���� docker file �� �����������.
        var result = await RunCommand.RunAsync(BuildCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
        
    }

    [Fact]
    public async void �������丮_�α���()
    {
        // �������丮�� push ���� �����������.
        // docker client�� ����Ұ�� ���� ��ü�� �������� �־ ���డ��.
        var result = await RunCommand.RunAsync(RepositoryLoginCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void Tag_����()
    {
        // tag�� push �� �������丮 �����ʿ�
        // �������丮��  push ����� ��������� push �����Ҽ� ����.
        // ex)host.test.com:30000/test-group/test-udpserver:0.0.1
        // �����̶�� ���ٴ� ���������....
        var result = await RunCommand.RunAsync(TagChangeCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void Push()
    {
        // �Էµ� �������丮 ��ġ�� push �۾� ����. �� login ���� �� �ؾ���.
        var loginResult = await RunCommand.RunAsync(RepositoryLoginCmd);
        var result = await RunCommand.RunAsync(PushCmd);

        _output.WriteLine(result.StandardOutput);
        _output.WriteLine(result.StandardError);
    }

    [Fact]
    public async void ����׹�������()
    {
        ����();
        Tag_����();
        �������丮_�α���();
        Push();
    }
}