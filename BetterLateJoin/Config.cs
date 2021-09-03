using Exiled.API.Interfaces;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace BetterLateJoin
{
    public class Config : IConfig
    {
        public bool IsEnabled { get; set; } = true;
        [Description("How long after the round start should players be allowed to join late")]
        public int LateJoinTime { get; set; } = 60;
        [Description("List of roles players are allowed to spawn in as if they join late")]
        public List<string> AllowedRoles { get; set; } = new List<string>{ "Scp173", "Scp106", "Scp049", "Scp079", "Scp096", "Scp93953", "Scp93989", "ClassD", "Scientist", "FacilityGuard" };
        [Description("The spawn queue used to determine what class players spawn as, don't touch unless you know what you're doing")]
        public string SpawnQueue { get; set; } = "40143140314414041340";
        
    }
}
