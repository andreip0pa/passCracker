using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasswordCrackerServer
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Chunk> chunkList = new List<Chunk>();
            chunkList = CreateChunks();
            foreach (var item in chunkList[0].WordList)
            {
                Console.WriteLine(item);
            }
            

            TcpWorker t = new TcpWorker(9999, IPAddress.Loopback,chunkList);
            
               
                    t.Start();
                
            
           


           
        }

        static List<Chunk> CreateChunks()
        {
            List<string> fullDicionary = new List<string>();

            string filename = "webster-dictionary-reduced.txt";
            
            FileStream fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            using (StreamReader sr = new StreamReader(fs))
            {

                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    fullDicionary.Add(line);
                    

                    
                }
              
            }


            
            int maxi = fullDicionary.Count / 4;

            List<Chunk> chunkList = new List<Chunk>();

            for (int j = 0; j <= 3; j++)
            {

                int initiali = maxi - fullDicionary.Count / 4;
                List<string> list1 = new List<string>();
                for (int i = initiali; i < maxi; i++)
                {

                    list1.Add(fullDicionary[i]);
                }
                chunkList.Add(new Chunk(list1));
                
                maxi += fullDicionary.Count / 4;
            }

            return chunkList;



        }

    }
}
