using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MP1_TCP_Servers
{
    public class JsonServer
    {
        public int port = 7;

        public void run()
        {
            Console.WriteLine("TCP Server Json");

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
                string? jsonString = reader.ReadLine();

                try
                {
                    JsonCommand commandObject = JsonSerializer.Deserialize<JsonCommand>(jsonString);

                    string command = commandObject.Method;

                    Console.WriteLine($"Method: {commandObject.Method}, Tal1: {commandObject.Value1}, Tal2: {commandObject.Value2}"); // Use while dev

                    // Is Client Input a Command?
                    if (command == "Random" || command == "Add" || command == "Subtract")
                    {
                        Console.WriteLine($"Command: {command}"); // Use while developing

                        // Parse Client Input is Int 
                        int value1 = commandObject.Value1;
                        int value2 = commandObject.Value2;

                        // For debugging in console
                        Console.WriteLine("Value 1: " + value1 + " Value 2: " + value2);

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

                        Console.WriteLine($"Command: {command} Result: {result}");

                        // Return Result
                        writer.WriteLine(result);
                        writer.Flush();
                    }

                    if (command == "stop")
                    {
                        socket.Close();
                    }

                } catch (JsonException ex) {
                    writer.WriteLine(ex.Message);
                    writer.Flush();
                }

                
            }
        }



    }
}
