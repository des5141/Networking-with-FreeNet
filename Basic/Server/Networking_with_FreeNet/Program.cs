using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using FreeNet;

namespace Networking_with_FreeNet
{
	partial class Program
	{
        static void Main(string[] args)
        {
            CServer_Run("0.0.0.0", 65535, 10000);
            Console.WriteLine("welcome to the server!");
        }
	}
}