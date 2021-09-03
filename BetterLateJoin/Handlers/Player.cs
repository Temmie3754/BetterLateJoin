using System.Collections.Generic;
using Exiled.Events.EventArgs;
using MEC;
using Exiled.API.Features;
using System.Linq;
using Exiled.API.Features.Items;
using TargetPlayer = Exiled.API.Features.Player;
using System;

namespace BetterLateJoin.Handlers
{
    class Player
    {
        Random random = new Random();
        public void OnVerified(VerifiedEventArgs ev)
        {
            // Creates a new modifiable list based off of allows roles in the config
            List<string> CurAllowedRoles = new List<string>(BetterLateJoin.Instance.Config.AllowedRoles);
            // Checks if the round has started and if it is within the number of seconds specified by the config from the start of the round
            if ((DateTime.Now - BetterLateJoin.Instance.server.StartTime).TotalSeconds > BetterLateJoin.Instance.Config.LateJoinTime || !BetterLateJoin.Instance.server.RoundStarted) return;
            
            // Checks list of current scps and removes them from the allowed roles list
            foreach (TargetPlayer player in TargetPlayer.List.Where(plr => plr.IsScp))
            {
                CurAllowedRoles.Remove(player.Role.ToString());
            }
            // Gets the specified class that the new player should be according to the spawn queue
            string TeamToPick = BetterLateJoin.Instance.Config.SpawnQueue.ToList()[TargetPlayer.List.Count() % 20 - 1].ToString();

            string PickedRole;
            // If the specified class is an SCP it checks if there are available SCP slots left and if so to assign a random non-used SCP to the player
            if (TeamToPick.Equals("0") && CurAllowedRoles.Any(name => name.StartsWith("Scp")))
            {
                List<string> PickedAllowedRoles = CurAllowedRoles.Where(name => name.StartsWith("Scp")).ToList();
                PickedRole = PickedAllowedRoles[random.Next(PickedAllowedRoles.Count)];
            }
            // Sets the class to its appropriate value or Class D if all SCPs are taken
            else if (TeamToPick.Equals("1")) PickedRole = "FacilityGuard";
            else if (TeamToPick.Equals("3")) PickedRole = "Scientist";
            else PickedRole = "ClassD";
            
            // Attempts to set the role to the determined class or Class D if an error occurs (likely due to an incorrect class being put in the config)
            try
            {
                RoleType role = (RoleType)Enum.Parse(typeof(RoleType), PickedRole);
                ev.Player.SetRole(role);
            }
            catch
            {
                Log.Error("Error occurred whilst attempting to set class, did you setup the config correctly?");
                ev.Player.SetRole(RoleType.ClassD);
            }
        }
    }
}
