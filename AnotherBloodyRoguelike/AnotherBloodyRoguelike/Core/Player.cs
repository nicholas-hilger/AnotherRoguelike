using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Awareness = 15;
            Name = "Dalov";
            Color = Colors.Player;
            Symbol = '@';
            X = 10;
            Y = 10;
        }
    }
}
