// See https://aka.ms/new-console-template for more information
using System.Net.Sockets;
using System.Net;
using System.Text;

Console.WriteLine("udp client");
// (1) UdpClient 객체 성성
UdpClient cli = new UdpClient();

// 1 server ip, 2 port, 3 message
string[] arguments = Environment.GetCommandLineArgs();
Console.WriteLine(arguments.Length);
Console.WriteLine(string.Join(Environment.NewLine, arguments));

if (arguments.Length == 4)
{
    string serverIp = arguments[1];
    int port = Convert.ToInt32(arguments[2]);
    string message = arguments[3];

    byte[] datagram = Encoding.UTF8.GetBytes(message);
    // (2) 데이타 송신
    cli.Send(datagram, datagram.Length, serverIp, port);
    Console.WriteLine($"[Send] {serverIp}:{port} 로 {datagram.Length} 바이트 전송");

    // (3) 데이타 수신
    IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
    byte[] bytes = cli.Receive(ref epRemote);
    Console.WriteLine("[Receive] {0} 로부터 {1} 바이트 수신", epRemote.ToString(), bytes.Length);

    // (4) UdpClient 객체 닫기
    cli.Close();
}
else Console.WriteLine("arg => serverip port message");

//string msg = "send message";
//byte[] datagram = Encoding.UTF8.GetBytes(msg);

//// (2) 데이타 송신
//cli.Send(datagram, datagram.Length, "127.0.0.1", 7777);
//Console.WriteLine("[Send] 127.0.0.1:7777 로 {0} 바이트 전송", datagram.Length);

//// (3) 데이타 수신
//IPEndPoint epRemote = new IPEndPoint(IPAddress.Any, 0);
//byte[] bytes = cli.Receive(ref epRemote);
//Console.WriteLine("[Receive] {0} 로부터 {1} 바이트 수신", epRemote.ToString(), bytes.Length);

//// (4) UdpClient 객체 닫기
//cli.Close();