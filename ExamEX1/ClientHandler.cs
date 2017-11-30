using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ExamEX1
{
    public class ClientHandler
    {
        Socket socket;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        public ClientHandler(Socket socket)
        {
            this.socket = socket;
        }

        public void RunClient()
        {
            stream = new NetworkStream(socket);
            writer = new StreamWriter(stream);
            reader = new StreamReader(stream);

            DoDialog();

            writer.Close();
            reader.Close();
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }

        public void DoDialog()
        {
            while (ExecuteCommand()) ;
        }

        public bool ExecuteCommand()
        {
            string command = ReceiveFromClient();

            if (command == null)
                return false;

            switch (command.Trim().ToUpper())
            {
                case "DIRECTORYCATALOG":
                    {
                        string inputMsg = ReceiveFromClient();
                        string response = Services.DirectoryCatalog(inputMsg);
                        SendToClient(response);
                    }
                    break;
                case "SUBDIRECTORYCATALOG":
                    {
                        string inputMsg = ReceiveFromClient();
                        string response = Services.SubDirectoryCatalog(inputMsg);
                        SendToClient(response);
                    }
                    break;
                case "BYE":
                    return false;
                default:
                    SendToClient("Unknown command");
                    break;
            }
            return true;
        }

        private string ReceiveFromClient()
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

        public void SendToClient(string text)
        {
            writer.WriteLine(text);
            writer.Flush();
        }
    }
}
