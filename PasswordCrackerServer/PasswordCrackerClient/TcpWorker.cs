﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;
using System.IO;
namespace PasswordCrackerClient
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

        public TcpWorker(int port, IPAddress ip)
        {
            Port = port;
            Ip = ip;
        }


        public void Start()
        {
            List<string> wordList = new List<string>();
            TcpClient tcpClient = new TcpClient();
            tcpClient.Connect(ip, port);
            NetworkStream networkStream = tcpClient.GetStream();
            StreamWriter writer = new StreamWriter(networkStream);
            StreamReader reader = new StreamReader(networkStream);
            string str = reader.ReadLine();
            writer.AutoFlush = true;
            while (str!="END")
            {
                 str = reader.ReadLine();
                wordList.Add(str);

                Console.WriteLine(str);
               
                

            }
            
            tcpClient.Close();

            Console.ReadKey();

           



        }



        void ClientWork(TcpClient client)
        {
           

        }
    }
}
