using AnotherRoguelike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Abilities
{
    class Nothing : IAbility
    {
        public string Name { get; }

        public int TurnsToRefresh { get; }

        public int TurnsUntilRefresh { get; }

        public Nothing()
        {
            Name = "None";
            TurnsToRefresh = 0;
            TurnsUntilRefresh = 0;
        }

        public bool Perform()
        {
            Game.MessageLog.Add($"{Game.Player.Name} tries to execute an ability that doesn't exist.");
            return false;
        }

        public void Tick()
        {

        }
    }
}
