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
	class Program
	{
		static List<CGameUser> userlist = new List<CGameUser>();
        static CNetworkService service = new CNetworkService(false);

        static CPacket ping_buffer = CPacket.create(1);
        static void Main(string[] args)
        {
            ping_buffer.record_size();
			service.session_created_callback += on_session_created;
            service.initialize(10000, 1024);
			service.listen("0.0.0.0", 65535, 100);

            Console.WriteLine("welcome to the server!");
        }
		static void on_session_created(CUserToken token)
		{
            Console.WriteLine("connected");
            CGameUser user = new CGameUser(token);
            token.ping_array = new ArraySegment<byte>(ping_buffer.buffer, 0, ping_buffer.position);
            token.time.Start();
            lock (userlist)
            {
                userlist.Add(user);
            }
        }
		public static void remove_user(CGameUser user)
		{
            Console.WriteLine("User Disconnected!");
            lock (userlist)
            {
                userlist.Remove(user);
            }
        }
	}
}