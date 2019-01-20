using System;
using FreeNet;

namespace Networking_with_FreeNet
{
    public partial class CGameUser : IPeer
    {
        public CUserToken token;

        public void send(CPacket msg)
        {
            msg.record_size();
            this.token.send(new ArraySegment<byte>(msg.buffer, 0, msg.position));
        }

        public CGameUser(CUserToken token)
        {
            this.token = token;
            this.token.set_peer(this);
        }

        void IPeer.on_removed()
        {
            Program.remove_user(this);
        }

        public void send(ArraySegment<byte> data)
        {
            this.token.send(data);
        }

        void IPeer.disconnect()
        {
            this.token.ban();
        }

        void IPeer.on_message(CPacket read)
        {
            var msgType = read.pop_int16();

            switch (msgType)
            {
                case signal_login:
                    {
                        CPacket buffer = CPacket.create();
                        buffer.set_signal(msgType);
                        buffer_write(buffer, buffer_u8, 79);
                        send(buffer);
                        break;
                    }
            }
        }
    }
}

