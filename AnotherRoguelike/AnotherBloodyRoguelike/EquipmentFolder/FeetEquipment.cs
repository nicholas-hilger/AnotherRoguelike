using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class FeetEquipment : Equipment
    {
        public static FeetEquipment None()
        {
            return new FeetEquipment { Name = "None" };
        }
    }
}
