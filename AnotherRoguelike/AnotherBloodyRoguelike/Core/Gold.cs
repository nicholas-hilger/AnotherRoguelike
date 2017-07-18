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
    public class Gold : IDrawable
    {
        public int Amount { get; set; }

        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public Gold(int x,int y,int amt)
        {
            Amount = amt;
            Color = RLColor.Yellow;
            Symbol = '$';
            X = x;
            Y = y;
        }

        public void Draw(RLConsole console, IMap map)
        {
            if (!map.GetCell(X, Y).IsExplored) return;

            console.Set(X, Y, Color, null, Symbol);
        }
    }
}
