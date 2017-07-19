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
    }
}
