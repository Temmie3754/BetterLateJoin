using Exiled.API.Features;
using Player = Exiled.Events.Handlers.Player;
using Server = Exiled.Events.Handlers.Server;
using System;

namespace BetterLateJoin
{
    public class BetterLateJoin : Plugin<Config>
    {
        private static BetterLateJoin Singleton;
        public static BetterLateJoin Instance => Singleton;
        public override string Author => "TemmieGamerGuy";
        public override string Name => "BetterLateJoin";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(3, 0, 0);

        private Handlers.Player player;
        internal Handlers.Server server;

        public void RegisterEvents()
        {
            player = new Handlers.Player();
            server = new Handlers.Server();
            Player.Verified += player.OnVerified;
            Server.RoundStarted += server.OnRoundStarted;
            Server.WaitingForPlayers += server.OnWaitingForPlayers;
        }

        public void UnregisterEvents()
        {
            Player.Verified -= player.OnVerified;
            Server.RoundStarted -= server.OnRoundStarted;
            Server.WaitingForPlayers -= server.OnWaitingForPlayers;
            server = null;
            player = null;
        }

        public override void OnEnabled()
        {
            Singleton = this;
            RegisterEvents();
            base.OnEnabled();
        }

        public override void OnDisabled()
        {
            UnregisterEvents();
            base.OnDisabled();
        }
    }
}
