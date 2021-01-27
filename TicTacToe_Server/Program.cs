using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ModerrNetworking;

namespace TicTacToe_Server
{

    public delegate void PacketEvent(Socket source, ModerrPacket packet);

    public class Program
    {

        // Events
        public event PacketEvent onPacketReceived;
        public event PacketEvent onPacketSended;

        // Variables
        private static readonly Socket ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        private static readonly List<Socket> ClientSockets = new List<Socket>();
        private const int BufferSize = 2048;
        private const int Port = 5542;
        private static readonly byte[] Buffer = new byte[BufferSize];

        // Constructor
        static void Main(string[] args)
        {
            Console.Title = "TicTacToe - Uruchamianie...";
            SetupServer();
            Console.Title = "TicTacToe - Online";
            Console.ReadKey();
            byte[] exitMessage = Encoding.ASCII.GetBytes("Server exiting...");
            foreach(Socket s in ClientSockets)
            {
                s.Send(exitMessage, 0, exitMessage.Length, SocketFlags.None);
            }
            CloseAllSockets();
        }

        private static void SetupServer()
        {
            Console.WriteLine("Ustawianie serwera");
            ServerSocket.Bind(new IPEndPoint(IPAddress.Any, Port));
            ServerSocket.Listen(0);
            Console.WriteLine("Ustawiono serwer");
            Console.WriteLine("Włączono serwer!");
            ServerSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Włączono łączenie się z serwerem!");
        }

        private static void CloseAllSockets()
        {
            foreach (Socket socket in ClientSockets)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            ServerSocket.Close();
        }

        private static void AcceptCallback(IAsyncResult ar)
        {
            Socket socket;
            try
            {
                socket = ServerSocket.EndAccept(ar);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            ClientSockets.Add(socket);
            ServerSocket.BeginAccept(AcceptCallback, null);
            socket.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, socket);
            Console.WriteLine("Klient podłączył się do serwera");
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            Socket current = (Socket) ar.AsyncState;
            int received;
            try
            {
                received = current.EndReceive(ar);
            }
            catch (SocketException)
            {
                Console.WriteLine("Klient został odłączony na siłę");
                current.Close();
                ClientSockets.Remove(current);
                return;
            }
            byte[] recBuf = new byte[received];
            Array.Copy(Buffer, recBuf, received);
            string text = Encoding.ASCII.GetString(recBuf);
            Console.WriteLine($"[Klient] {text}");
            switch (text.ToLower())
            {
                case "get time":
                    {
                        Console.WriteLine("Klient chce pobrać czas");
                        int id = (int)PacketId.RESPONSE;
                        ModerrPacket packet = new ModerrPacket(PacketSender.SERVER, id, DateTime.Now.ToLongTimeString());
                        byte[] packetData = packet.SerializeToByte();
                        current.Send(packetData);
                        Console.WriteLine("Czas został wysłany do klienta");
                        break;
                    }
                case "exit":
                    current.Shutdown(SocketShutdown.Both);
                    current.Close();
                    ClientSockets.Remove(current);
                    Console.WriteLine("Klient odłączył się");
                    return;
                default:
                    {
                        int id = (int)PacketId.RESPONSE;
                        ModerrPacket packet = new ModerrPacket(PacketSender.SERVER, id, "Nieznana komenda.");
                        byte[] packetData = packet.SerializeToByte();
                        current.Send(packetData);
                        break;
                    }
            }
            current.BeginReceive(Buffer, 0, BufferSize, SocketFlags.None, ReceiveCallback, current);
        }
    }
}
