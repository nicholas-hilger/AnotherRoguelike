using AnotherRoguelike.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.EquipmentFolder
{
    public class Shield : Equipment
    {
        public static Shield None()
        {
            return new Shield { Name = "None" };
        }

        public static Shield Wood()
        {
            return new Shield
            {
                Defense = 1,
                DefChance = 3,
                Name = "Plywood Chunk"
            };
        }

        public static Shield Tin()
        {
            return new Shield
            {
                Defense = 2,
                DefChance = 6,
                Name = "Tin Shield"
            };
        }

        public static Shield Hardwood()
        {
            return new Shield
            {
                Defense = 3,
                DefChance = 8,
                Name = "Lacquered Shield"
            };
        }
    }
}
