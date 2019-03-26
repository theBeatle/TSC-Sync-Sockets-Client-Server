using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] buffer = new byte[1024];

            try
            {
                // to get owner IP
                //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName()); //Server IP
                //IPAddress ip = ipHostInfo.AddressList[3]; // moge ne 0  //Server IP
                //
                //foreach (var item in ipHostInfo.AddressList)
                //{
                //    Console.WriteLine($"Ip addr - {item}");
                //}
                //connection 
                var serverIP = new IPAddress(new byte[] { 10, 7, 180, 104 });
                //IPEndPoint remotePort = new IPEndPoint(ip, 11000);
                IPEndPoint remotePort = new IPEndPoint(serverIP, 11000);

                //socket creation
                // IPv4, Stream, TCP
                Socket socket = new Socket(serverIP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    Console.WriteLine("Press any key to connect!");
                    Console.ReadKey();
                    socket.Connect(remotePort);
                    Console.WriteLine($"Connected to {socket.RemoteEndPoint}");
                

                    byte[] msg = Encoding.ASCII.GetBytes("something EOT");

                    //sent to server
                    int sendBytes = socket.Send(msg);

                    //receive answer from server
                    int receivedBytes = socket.Receive(buffer);
                    Console.WriteLine($"Received: {Encoding.ASCII.GetString(buffer,0,receivedBytes)}");

                    //free socket
                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
    }
}
