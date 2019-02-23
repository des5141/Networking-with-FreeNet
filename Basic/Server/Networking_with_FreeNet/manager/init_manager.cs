using FreeNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Networking_with_FreeNet
{
    public partial class CGameUser : IPeer
    {
        #region Signal
        const int signal_heartbeat = 1;
        const int signal_login = 2;
        #endregion

        #region Buffer Type
        const int buffer_u8 = 0;
        const int buffer_s8 = 1;
        const int buffer_u16 = 2;
        const int buffer_s16 = 3;
        const int buffer_u32 = 4;
        const int buffer_s32 = 5;
        const int buffer_string = 6;
        #endregion

        #region Buffer Functions
        public static dynamic buffer_write(CPacket buffer, int type, dynamic value)
        {
            switch (type)
            {
                case buffer_u8:
                    buffer.push((Byte)value);
                    break;

                case buffer_s8:
                    buffer.push((SByte)value);
                    break;

                case buffer_u16:
                    buffer.push((UInt16)value);
                    break;

                case buffer_s16:
                    buffer.push((Int16)value);
                    break;

                case buffer_u32:
                    buffer.push((UInt32)value);
                    break;

                case buffer_s32:
                    buffer.push((Int32)value);
                    break;

                case buffer_string:
                    buffer.push((String)value);
                    break;
            }
            return null;
        }
        public static dynamic buffer_read(CPacket buffer, int type)
        {
            switch(type)
            {
                case buffer_u8:
                case buffer_s8:
                    return buffer.pop_byte();

                case buffer_u16:
                case buffer_s16:
                    return buffer.pop_int16();

                case buffer_u32:
                case buffer_s32:
                    return buffer.pop_int32();

                case buffer_string:
                    return buffer.pop_string();
            }
            return null;
        }
        #endregion
    }

    public partial class Program
    {
        #region Init
        static List<CGameUser> userlist = new List<CGameUser>();
        static CNetworkService service = new CNetworkService(false);
        static CPacket ping_buffer = CPacket.create(1);

        public static void CServer_Run(String IP, int PORT, int MAX_CLIENTS)
        {
            ping_buffer.record_size();
            service.session_created_callback += on_session_created;
            service.initialize(MAX_CLIENTS, 1024);
            service.listen(IP, PORT, 100);
        }
        #endregion

        #region Server Fuction
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
        #endregion
    }
}