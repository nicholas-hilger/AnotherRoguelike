using AnotherRoguelike.Core;
using AnotherRoguelike.Interfaces;
using AnotherRoguelike.Systems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Abilities
{
    public class Whirlwind : IAbility
    {
        public string Name { get; }
        public int TurnsToRefresh { get; }
        public int TurnsUntilRefresh { get; private set; }

        private readonly CommandSystem commSystem;

        public Whirlwind(CommandSystem commSystem)
        {
            Name = "Whirlwind";
            TurnsToRefresh = 20;
            TurnsUntilRefresh = 0;
            this.commSystem = commSystem;
        }

        public bool Perform()
        {
            if (TurnsUntilRefresh > 0) return false;

            Player player = Game.Player;
            Game.MessageLog.Add($"{player.Name} spins around. Fast.");

            TurnsUntilRefresh = TurnsToRefresh;

            return true;
        }

        public void Tick()
        {
            if (TurnsUntilRefresh > 0) TurnsUntilRefresh--;
        }
    }
}
