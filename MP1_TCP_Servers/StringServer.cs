using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MP1_TCP_Servers
{
    public class StringServer
    {

        public int port = 21;

        public void run()
        {
            Console.WriteLine("TCP Server String");

            TcpListener listener = new TcpListener(IPAddress.Any, port);

            listener.Start();

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                IPEndPoint clientEndPoint = socket.Client.RemoteEndPoint as IPEndPoint;
                Console.WriteLine("Client connected: " + clientEndPoint.Address);

                Task.Run(() => HandleClient(socket));
            }

            listener.Stop();
        }

        void HandleClient(TcpClient socket)
        {
            NetworkStream ns = socket.GetStream();
            StreamReader reader = new StreamReader(ns);
            StreamWriter writer = new StreamWriter(ns);

            while (socket.Connected)
            {
                // Get Client Input
                string? command = reader.ReadLine();

                if (command == null)
                {
                    writer.Flush();
                }

                // Is Client Input a Command?
                if (command == "Random" || command == "Add" || command == "Subtract")
                {
                    Console.WriteLine($"Command: {command}"); // Use while developing

                    writer.WriteLine("Input numbers");
                    writer.Flush();

                    // Expected Client Input <value1> <seperator> <value2>  
                    string? commandResponse = reader.ReadLine();
                    string[]? commandParts = commandResponse.Split(' ');

                    // Parse Client Input is Int 
                    int value1 = int.Parse(commandParts[0]);
                    int value2 = int.Parse(commandParts[1]);

                    // Could be a good idea to validate values

                    // For debugging in console
                    Console.WriteLine("Value 1: " + value1 + " Value 2: " + value2); // Use while developing

                    string result = "";

                    switch (command)
                    {
                        case "Random":

                            Random rand = new Random();
                            result = rand.Next(value1, value2).ToString();

                            break;

                        case "Add":

                            result = (value1 + value2).ToString();

                            break;

                        case "Subtract":

                            result = (value1 - value2).ToString();

                            break;

                        default:
                            break;
                    }

                    Console.WriteLine($"Command: {command} Result: {result}"); // Use while developing

                    // Return Result
                    writer.WriteLine(result);
                    writer.Flush();
                }

                writer.Flush();

                if (command == "stop")
                {
                    socket.Close();
                }
            }
        }

    }
}
