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
    }
}
