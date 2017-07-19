using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Interfaces
{
    public interface IEquipment
    {
        int Attack { get; set; }
        int AttChance { get; set; }
        int Awareness { get; set; }
        int Defense { get; set; }
        int DefChance { get; set; }
        string Name { get; set; }
        int Speed { get; set; }
    }
}
