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

            switch(msgType)
            {
                case signal_login:
                    CPacket buffer = CPacket.create();
                    buffer.set_protocol(msgType);
                    buffer.push("Hello");

                    buffer.push("sex");

                    buffer.push("오르피아 최고");

                    buffer.push("하 언조 비카이");

                    buffer.push((byte)69);

                    buffer.push((Int16)25505);
                    send(buffer);
                    break;

                case signal_echo:

                    break;
            }
        }
    }
}