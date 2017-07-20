using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class HeadEquipment : Equipment
    {
        public static HeadEquipment None()
        {
            return new HeadEquipment { Name = "None" };
        }

        public static HeadEquipment Cap()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefChance = 2,
                Name = "Cap"
            };
        }

        public static HeadEquipment Copper()
        {
            return new HeadEquipment()
            {
                Defense = 3,
                DefChance = 4,
                Name = "Copper Helm"
            };
        }

        public static HeadEquipment Hat()
        {
            return new HeadEquipment()
            {
                Defense = 1,
                DefChance = 3,
                Name = "Hat"
            };
        }

        public static HeadEquipment Iron()
        {
            return new HeadEquipment()
            {
                Defense = 6,
                DefChance = 5,
                Name = "Iron Cap"
            };
        }
    }
}
