using System;
using FreeNet;

namespace Networking_with_FreeNet
{
    public partial class CGameUser : IPeer
    {
        public CUserToken token;

        public CGameUser(CUserToken token)
        {
            this.token = token;
            this.token.set_peer(this);
        }

        void IPeer.on_removed()
        {
            Program.remove_user(this);
        }

        public void send(CPacket msg)
        {
            msg.record_size();
            this.token.send(new ArraySegment<byte>(msg.buffer, 0, msg.position));
        }

        public void send(ArraySegment<byte> data)
        {
            this.token.send(data);
        }

        void IPeer.disconnect()
        {
            this.token.ban();
        }

        void IPeer.on_message(CPacket msg)
        {
            var msgType = msg.pop_int16();

            switch (msgType)
            {
                case signal_login:
                    {
                        Console.WriteLine(buffer_read(msg, buffer_string));
                        Console.WriteLine(buffer_read(msg, buffer_s32));
                        CPacket buffer = CPacket.create();
                        buffer.set_signal(msgType);
                        buffer_write(buffer, buffer_s8, 254);
                        buffer_write(buffer, buffer_s32, -1);
                        buffer_write(buffer, buffer_string, "sex");
                        buffer_write(buffer, buffer_string, "asd");
                        buffer_write(buffer, buffer_string, "qweqweqwe");
                        buffer_write(buffer, buffer_s8, -50);
                        buffer_write(buffer, buffer_string, "loli ZOA!!!!!");
                        send(buffer);
                        break;
                    }

                case signal_echo:
                    {
                        buffer_read(msg, buffer_u32);
                        CPacket buffer = CPacket.create();
                        buffer.set_signal(msgType);
                        buffer_write(buffer, buffer_u32, 2);
                        send(buffer);
                        break;
                    }
            }
        }
    }
}