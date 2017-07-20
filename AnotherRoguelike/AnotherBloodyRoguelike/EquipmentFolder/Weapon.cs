using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class Weapon : Equipment
    {
        public static Weapon None()
        {
            return new Weapon { Name = "None" };
        }

        public static Weapon Wood()
        {
            return new Weapon
            {
                Attack = 1,
                AttChance = 1,
                Name = "Wooden Sword"
            };
        }

        public static Weapon Shiv()
        {
            return new Weapon
            {
                Attack = 2,
                AttChance = 3,
                Name = "Shiv"
            };
        }

        public static Weapon Bronze()
        {
            return new Weapon
            {
                Attack = 1,
                AttChance = 1,
                Name = "Bronze Blade"
            };
        }
    }
}
