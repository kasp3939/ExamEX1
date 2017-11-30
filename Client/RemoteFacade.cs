using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class RemoteFacade
    {
        string serverName;
        int port;


        TcpClient serverSocket;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        public RemoteFacade(string serverName, int port)
        {
            this.serverName = serverName;
            this.port = port;
            Open();
        }

        public void Open()
        {
            if (serverSocket != null) return;

            serverSocket = new TcpClient(serverName, port);
            stream = serverSocket.GetStream();
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);
        }
        public void Close()
        {
            writer.Close();
            reader.Close();
            stream.Close();
            serverSocket.Close();
            serverSocket = null;
        }
        public string GetAllDirectories(string text)
        {
            SendToServer("DirectoryCatalog");
            SendToServer(text);
            return ReceiveFromServer();

        }
        public string GetAllSubDirectories(string text)
        {
            SendToServer("SubDirectoryCatalog");
            SendToServer(text);
            return ReceiveFromServer();

        }
        private void SendToServer(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }

        private string ReceiveFromServer()
        {
            try
            {
                return reader.ReadLine();
            }
            catch
            {
                return null;
            }
        }

    }
}
