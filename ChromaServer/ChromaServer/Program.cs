using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromaServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            Console.WriteLine("Server starting!");
            server.Start();
        }
    }
}
