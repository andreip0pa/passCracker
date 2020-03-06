using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.IO;
namespace PasswordCrackerServer
{
    class TcpWorker
    {
        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }

        private IPAddress ip;

        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }

        private List<Chunk> chunks;

        public List<Chunk> Chunks
        {
            get { return chunks; }
            set { chunks = value; }
        }


        public TcpWorker(int port, IPAddress ip, List<Chunk> chunkList)
        {
            Port = port;
            Ip = ip;
            Chunks = chunkList;

        }


        public void Start()
        {
            //TcpClient tcpClient = new TcpClient();
            //tcpClient.Connect(ip, port);

            //NetworkStream networkStream = tcpClient.GetStream();
            //string message = "abcd";
            //byte[] byteArray = System.Text.Encoding.ASCII.GetBytes(message);
            //networkStream.Write(byteArray);

            TcpListener tcpListener = new TcpListener(Ip,9999);
            tcpListener.Start();
            while (true)
            {
                Task.Run(() =>
                {

                    TcpClient cl = tcpListener.AcceptTcpClient();
                    ClientWork(cl);
                    Thread.Sleep(10000);






                });
            }
           



        }



        void ClientWork(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            StreamWriter writer = new StreamWriter(stream);
            StreamReader reader = new StreamReader(stream);
            writer.AutoFlush = true;




            foreach (var item in Chunks)
            {
                if (item.Sent == false)
                {
                    item.Sent = true;
                    foreach (var word in item.WordList)
                    {
                        
                        writer.WriteLine(word);
                    }
                    writer.WriteLine("END");
                   
                    break;
                    
                }
            }
            
            Task.Run(() => {
                while (true)
                {
                    try
                    {
                        string str = reader.ReadLine();
                        for (int i = 65; i <= 122;i++)
                        {
                            if (str.Contains(Convert.ToChar(i)))
                            {
                                Console.WriteLine(str);
                                Thread.Sleep(1000);
                            }
                        }
                       
                    }
                    catch
                    {

                    }
                }
            });

                

        }
    }
}
