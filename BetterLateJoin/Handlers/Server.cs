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
