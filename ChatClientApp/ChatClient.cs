using System.Net;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ChatClient
{
    public static void StartClient()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(ipAddress, 8080);
        Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            sender.Connect(remoteEP);
            Console.WriteLine("Connected to Server.");

            Thread receiveThread = new Thread(() => ReceiveMessages(sender));
            receiveThread.Start();

            while (true)
            {
                string message = Console.ReadLine();
                byte[] msg = Encoding.ASCII.GetBytes(message);
                sender.Send(msg);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

    }

    private static void ReceiveMessages(Socket sender)
    {

        while (true)
        {
            byte[] bytes = new byte[1024];
            int bytesRec = sender.Receive(bytes);
            string data = Encoding.ASCII.GetString(bytes, 0, bytesRec);
            Console.WriteLine("Server: " + data);
        }
    }
}