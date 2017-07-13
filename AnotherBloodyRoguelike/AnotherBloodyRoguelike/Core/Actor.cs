using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnotherRoguelike.Interfaces;
using RLNET;
using RogueSharp;

namespace AnotherRoguelike.Core
{
    public class Actor : IActor, IDrawable
    {
        //IActor
        public string Name { get; set; }
        public int Awareness { get; set; }

        //IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public void Draw(RLConsole console, IMap map)
        {
            //Don't draw actors in unexplored cells
            if (!map.GetCell(X, Y).IsExplored) return;

            //Only draw the actor with the color and symbol when they're in FOV
            if (map.IsInFov(X, Y)) console.Set(X, Y, Color, Colors.FloorBackgroundFov, Symbol);

            //When not in fov just draw a floor
            else console.Set(X, Y, Colors.Floor, Colors.FloorBackground, '.');
        }
    }
}
