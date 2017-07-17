using AnotherRoguelike.Interfaces;
using RLNET;
using RogueSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Core
{
    public class Stairs : IDrawable
    {
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsUp { get; set; }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored) return;

            Symbol = IsUp ? '<' : '>';

            if (map.IsInFov(X, Y)) Color = Colors.Player;

            else Color = RLColor.LightGray;

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
