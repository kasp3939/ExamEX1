using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        RemoteFacade service;
        public Client(string serverName, int port)
        {
            service = new RemoteFacade(serverName, port);

            DoUserDialog();
            service.Close();
        }

        private void DoUserDialog()
        {
            Console.WriteLine("Type Q to Quit");

            bool active = true;
            do
            {
                string userCommand;
                Console.WriteLine("Enter command ");
                userCommand = Console.ReadLine();
                userCommand = userCommand.Trim().ToUpper();
                switch (userCommand)
                {
                    case "DIRECTORYCATALOG":
                        {
                            Console.WriteLine("Enter directory");
                            string msg = Console.ReadLine();
                            string response = service.GetAllDirectories(msg);
                            Console.WriteLine("response:" + response);
                            break;
                        }
                    case "SUBDIRECTORYCATALOG":
                        {
                            Console.WriteLine("Enter subdirectory");
                            string msg = Console.ReadLine();
                            string response = service.GetAllSubDirectories(msg);
                            Console.WriteLine("response:" + response);
                            break;
                        }
                    case "Q":
                        {
                            active = false;
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                }
            }
            while (active);
        }
    }
}
