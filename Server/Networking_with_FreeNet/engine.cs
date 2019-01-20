using FreeNet;
using System;
using System.Collections.Generic;
using System.Text;

namespace Networking_with_FreeNet
{
    public partial class CGameUser : IPeer
    {
        #region Signal
        const int signal_login = 1;
        const int signal_echo = 2;
        #endregion
    }
}
