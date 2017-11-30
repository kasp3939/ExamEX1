using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ExamEX1
{
    public class Server
    {
        public Server(int port)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            while (true)
            {
                Console.WriteLine("Server ready");
                Socket client = listener.AcceptSocket();
                Console.WriteLine("");
                Console.WriteLine("A client has connected from " + client.LocalEndPoint);
                ClientHandler clientHandler = new ClientHandler(client);
                Thread clientthread = new Thread(clientHandler.RunClient);
                clientthread.Start();
            }
        }
    }
}
