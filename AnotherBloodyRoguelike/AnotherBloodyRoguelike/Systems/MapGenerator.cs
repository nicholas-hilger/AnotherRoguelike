using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using AnotherRoguelike.Core;

namespace AnotherRoguelike.Systems
{
    public class MapGenerator
    {
        private readonly int width;
        private readonly int height;

        private readonly DungeonMap map;

        //MapGenerator construction requires the dimensions of the map it'll create
        public MapGenerator(int w, int h)
        {
            width = w;
            height = h;
            map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            //Init every cell in map by setting walkable, transparency, and explored to be true
            map.Initialize(width, height);
            foreach (Cell cell in map.GetAllCells())
                map.SetCellProperties(cell.X, cell.Y, true, true, true);

            //Set the first and last rows in the map to be walls
            foreach (Cell cell in map.GetCellsInRows(0, height-1))
                map.SetCellProperties(cell.X, cell.Y, false, false, true);

            //Set the first and last column in the map to be walls
            foreach (Cell cell in map.GetCellsInColumns(0, width - 1))
                map.SetCellProperties(cell.X, cell.Y, false, false, true);

            return map;
        }
    }
}
