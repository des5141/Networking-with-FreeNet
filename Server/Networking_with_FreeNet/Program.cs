using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreeNet;

namespace Networking_with_FreeNet
{
	class Program
	{
		static List<CGameUser> userlist;

		static void Main(string[] args)
		{
			userlist = new List<CGameUser>();

			CNetworkService service = new CNetworkService(false);
			service.session_created_callback += on_session_created;
            service.initialize(10000, 1024);
			service.listen("0.0.0.0", 7979, 100);
            
            service.disable_heartbeat();


			Console.WriteLine("Started!");
			while (true)
			{
                //Console.Write(".");
                string input = Console.ReadLine();
                if (input.Equals("users"))
                {
                    Console.WriteLine(service.usermanager.get_total_count());
                }
				System.Threading.Thread.Sleep(1000);
			}
		}
		static void on_session_created(CUserToken token)
		{
            CGameUser user = new CGameUser(token);
            lock (userlist)
            {
                userlist.Add(user);
            }
        }

		public static void remove_user(CGameUser user)
		{
            lock (userlist)
            {
                userlist.Remove(user);
            }
        }
	}
}
