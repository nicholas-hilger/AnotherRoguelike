using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Interfaces
{
    public interface IActor
    {
        int Attack { get; set; }
        int AttChance { get; set; }
        int Awareness { get; set; }
        int Defense { get; set; }
        int DefChance { get; set; }
        int Gold { get; set; }
        int Health { get; set; }
        int MaxHealth { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
        int Xp { get; set; }
        int MaxXp { get; set; }
    }
}
