using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RogueSharp;
using RLNET;

namespace AnotherRoguelike.Core
{
    //This class extends the base RS Map class
    public class DungeonMap : Map
    {
        public List<Rectangle> Rooms;

        public DungeonMap()
        {
            Rooms = new List<Rectangle>();
        }

        //Draw will be called each time the map is updated
        //Renders all of the symbols/colors for each cell in the map
        public void Draw(RLConsole mapConsole)
        {
            mapConsole.Clear();
            foreach (Cell cell in GetAllCells())
                SetConsoleSymbolForCell(mapConsole, cell);
        }
        private void SetConsoleSymbolForCell(RLConsole console, Cell cell)
        {
            //When we haven't explored a cell yet, we don't want to draw anything
            if (!cell.IsExplored) return;
            //When a cell is in FOV, it'll be drawn lighter
            if (IsInFov(cell.X, cell.Y))
            {
                //Choose the symbol to draw based on if it's walkable or not
                if (cell.IsWalkable) console.Set(cell.X, cell.Y, Colors.FloorFov, Colors.FloorBackgroundFov, '.');
                else console.Set(cell.X, cell.Y, Colors.WallFov, Colors.FloorBackgroundFov, '#');
            }
            else
            {
                if (cell.IsWalkable) console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                else console.Set(cell.X, cell.Y, Colors.Wall, Colors.FloorBackground, '#');
            }
        }
        public void UpdatePlayerFOV()
        {
            Player player = Game.Player;
            //Compute FOV based on the player's awareness and location
            ComputeFov(player.X, player.Y, player.Awareness, true);
            //Mark all cells in FOV as explored
            foreach (Cell cell in GetAllCells())
            {
                if (IsInFov(cell.X, cell.Y))
                    SetCellProperties(cell.X, cell.Y, cell.IsTransparent, cell.IsWalkable, true);
            }
        }

        //Returns true when able to place the Actor on the cell
        public bool SetActorPosition(Actor actor, int x, int y)
        {
            if (GetCell(x, y).IsWalkable)
            {
                //The cell the actor was previously on is now walkable
                SetIsWalkable(actor.X, actor.Y, true);
                //Update actor's position
                actor.X = x;
                actor.Y = y;
                //New cell the actor is on is now not walkable
                SetIsWalkable(actor.X, actor.Y, false);
                //Update FOV
                if (actor is Player)
                {
                    UpdatePlayerFOV();
                }
                return true;
            }
            return false;
        }

        //Helper method for setting IsWalkable property on a Cell
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFOV();
        }
    }
}
