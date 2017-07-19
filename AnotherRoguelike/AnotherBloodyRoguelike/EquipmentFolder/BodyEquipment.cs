using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class BodyEquipment : Equipment
    {
        public static BodyEquipment None()
        {
            return new BodyEquipment { Name = "None" };
        }
    }
}
