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

        public static BodyEquipment Tunic()
        {
            return new BodyEquipment
            {
                Defense = 1,
                DefChance = 1,
                Name = "Tunic"
            };
        }

        public static BodyEquipment Leather()
        {
            return new BodyEquipment
            {
                Defense = 3,
                DefChance = 2,
                Name = "Leather Suit"
            };
        }

        public static BodyEquipment Soldier()
        {
            return new BodyEquipment
            {
                Defense = 5,
                DefChance = 5,
                Name = "Soldier Garb"
            };
        }
    }
}
