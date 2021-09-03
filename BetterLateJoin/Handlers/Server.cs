using System.Collections.Generic;
using Exiled.Events.EventArgs;
using MEC;
using Exiled.API.Features;
using System.Linq;
using Exiled.API.Features.Items;
using System;

namespace BetterLateJoin.Handlers
{
    class Server
    {
        internal DateTime StartTime = DateTime.Now;
        internal bool RoundStarted = false;
        public void OnWaitingForPlayers()
        {
            RoundStarted = false;
        }
        public void OnRoundStarted()
        {
            StartTime = DateTime.Now;
            RoundStarted = true;
        }
    }
}
