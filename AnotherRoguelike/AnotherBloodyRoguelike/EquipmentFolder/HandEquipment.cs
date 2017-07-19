using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class HandEquipment : Equipment
    {
        public static HandEquipment None()
        {
            return new HandEquipment { Name = "None" };
        }
    }
}
