using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks.Dataflow;

public class ChatServer
{
    private static List<Socket> _sockets = new List<Socket>();
    private static readonly object lockObj = new object();


    private static void HandleClient(Socket clientSocket)
    {
        byte[] bytes = new byte[1024];
        string data;

        while (true)
        {
            try
            {
                int bytesRec = clientSocket.Receive(bytes);
                data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                Console.WriteLine("Received: " + data);
                Broadcast(data, clientSocket);
            }
            catch (SocketException)
            {
                lock (lockObj)
                {
                    _sockets.Remove(clientSocket);
                }

                clientSocket.Close();
                break;
            }
        }
    }

    private static void Broadcast(string message, Socket excludeSocket)
    {
        byte[] msg=Encoding.ASCII.GetBytes(message);

        lock (lockObj)
        {
            foreach (Socket socket in _sockets)
            {
                if (socket != excludeSocket)
                {
                    socket.Send(msg);
                }
            }
        }
    }

    public static void StartServer()
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 8080);
        Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("Waiting for connections...");

            while (true)
            {
                Socket clientSocket = listener.Accept();
                lock (lockObj)
                {
                    _sockets.Add(clientSocket);
                }

                Thread clientThread = new Thread(() => HandleClient(clientSocket));
                clientThread.Start();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
}

