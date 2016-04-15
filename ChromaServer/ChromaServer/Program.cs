using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ChromaServer
{
    /// <summary>
    /// The main program class, starting the server.
    /// </summary>
    class Program
    {
        /// <summary>
        /// The program's entry function.
        /// </summary>
        /// <param name="args">Commandline parameters.</param>
        static void Main(string[] args)
        {
            Server server = new Server();
            Console.WriteLine("Server starting!");
            server.Start();
        }
    }
}
