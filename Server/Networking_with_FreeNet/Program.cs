using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using FreeNet;

namespace Networking_with_FreeNet
{
	class Program
	{
		static List<CGameUser> userlist;
        static int a, b = 0;
        static void Main(string[] args)
		{
			userlist = new List<CGameUser>();

			CNetworkService service = new CNetworkService(false);
			service.session_created_callback += on_session_created;
            service.initialize(10000, 1024);
			service.listen("0.0.0.0", 65535, 100);

            Timer aTimer = new Timer(1000 / 60);
            aTimer.Elapsed += OnStep;
            aTimer.Enabled = true;

            service.disable_heartbeat();

            Console.WriteLine("Started!");
			while (true)
			{
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
        private static void OnStep(Object source, ElapsedEventArgs e)
        {
            if(a != e.SignalTime.Second)
            {
                Console.WriteLine("----{0}----", b);
                a = e.SignalTime.Second;
                b = 0;
            }
            b++;

            //Console.WriteLine("{0} - {1}", e.SignalTime, b);
            //System.Threading.Thread.Sleep(500); // 무거운 작업 - 이 정도로 극단적인 작업는 task 이용
            System.Threading.Thread.Sleep(200); // 무거운 작업 - 이 정도는 서버 규모 커지면 가능 
        }
	}
}
