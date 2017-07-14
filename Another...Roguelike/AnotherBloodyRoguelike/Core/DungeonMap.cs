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
        private readonly List<Monster> monsters;
        public List<Rectangle> Rooms;

        public DungeonMap()
        {
            //Initialize needed lists
            Rooms = new List<Rectangle>();
            monsters = new List<Monster>();
        }

        //Draw will be called each time the map is updated
        //Renders all of the symbols/colors for each cell in the map
        public void Draw(RLConsole mapConsole, RLConsole statConsole)
        {
            foreach (Cell cell in GetAllCells())
                SetConsoleSymbolForCell(mapConsole, cell);
            /*foreach (Monster monster in monsters)
                monster.Draw(mapConsole, this);*/

            //Monster stat bars
            //Index for the position to draw the stats at
            int i = 0;
            foreach (Monster monster in monsters)
            {
                //Draw their stats if they're in your FOV
                if(IsInFov(monster.X,monster.Y))
                {
                    monster.Draw(mapConsole, this);
                    monster.DrawStats(statConsole, i);
                    i++;
                }
            }
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
                if (cell.IsWalkable || MonsterAt(cell.X,cell.Y)) console.Set(cell.X, cell.Y, Colors.Floor, Colors.FloorBackground, '.');
                else if(!MonsterAt(cell.X,cell.Y)) console.Set(cell.X, cell.Y, Colors.Wall, Colors.WallBackground, '#');
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

        public void AddPlayer(Player player)
        {
            Game.Player = player;
            SetIsWalkable(player.X, player.Y, false);
            UpdatePlayerFOV();
        }

        public void AddMonster(Monster monster)
        {
            monsters.Add(monster);
            //After adding a monster, make thir cell unwalkable
            SetIsWalkable(monster.X, monster.Y, false);
        }

        public void RemoveMonster(Monster monster)
        {
            monsters.Remove(monster);
            SetIsWalkable(monster.X, monster.Y, true);
            Game.Player.Xp += monster.Xp;
        }

        public Monster GetMonsterAt(int x, int y)
        {
            return monsters.FirstOrDefault(m => m.X == x && m.Y == y);
        }

        private bool MonsterAt(int x, int y)
        {
            return monsters.Exists(m => m.X == x && m.Y == y);
        }

        //Helper method for setting IsWalkable property on a Cell
        public void SetIsWalkable(int x, int y, bool isWalkable)
        {
            Cell cell = GetCell(x, y);
            SetCellProperties(cell.X, cell.Y, cell.IsTransparent, isWalkable, cell.IsExplored);
        }

        //Look for a random location in the room that's walkable
        public Point GetRandomWalkableLocation(Rectangle room)
        {
            if(DoesRoomHaveWalkableSpace(room))
            {
                for(int i = 0; i < 100; i++)
                {
                    int x = Game.Random.Next(1, room.Width - 2) + room.X;
                    int y = Game.Random.Next(1, room.Height - 2) + room.Y;
                    if(IsWalkable(x, y))
                    {
                        return new Point(x, y);
                    }
                }
            }
            //If we didn't find a walkable space, return null
            return null;
        }

        public bool DoesRoomHaveWalkableSpace(Rectangle room)
        {
            for(int x = 1; x <= room.Width - 2; x++)
            {
                for(int y = 1; y <= room.Height - 2; y++)
                {
                    if (IsWalkable(x + room.X, y + room.Y)) return true;
                }
            }
            return false;
        }
    }
}
