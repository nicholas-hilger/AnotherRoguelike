using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using AnotherRoguelike.Core;
using RogueSharp.DiceNotation;
using AnotherRoguelike.Monsters;

namespace AnotherRoguelike.Systems
{
    public class MapGenerator
    {
        private readonly int width;
        private readonly int height;
        private readonly int maxRooms;
        private readonly int roomMaxSize;
        private readonly int roomMinSize;

        private readonly DungeonMap map;

        //MapGenerator construction requires the dimensions of the map it'll create
        public MapGenerator(int w, int h,int maxRm, int rmMaxSize, int rmMinSize, int floor)
        {
            width = w;
            height = h;
            maxRooms = maxRm;
            roomMaxSize = rmMaxSize;
            roomMinSize = rmMinSize;
            map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            //Init every cell in map by setting walkable, transparency, and explored to be true
            map.Initialize(width, height);

            //Try to place as many rooms as maxRooms
            for (int i = 0; i < maxRooms; i++)
            {
                int roomWidth = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomHeight = Game.Random.Next(roomMinSize, roomMaxSize);
                int roomXPosition = Game.Random.Next(0, width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, height - roomHeight - 1);

                //All rooms are represented as Rectangles
                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                bool newRoomIntersects = map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    map.Rooms.Add(newRoom);
                }
                // Iterate through each room that was generated
                // Don't do anything with the first room, so start at r = 1 instead of r = 0
                for (int r = 1; r < map.Rooms.Count; r++)
                {
                    // For all remaing rooms get the center of the room and the previous room
                    int previousRoomCenterX = map.Rooms[r - 1].Center.X;
                    int previousRoomCenterY = map.Rooms[r - 1].Center.Y;
                    int currentRoomCenterX = map.Rooms[r].Center.X;
                    int currentRoomCenterY = map.Rooms[r].Center.Y;

                    // Give a 50/50 chance of which 'L' shaped connecting hallway to tunnel out
                    if (Game.Random.Next(1, 2) == 1)
                    {
                        CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                        CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, currentRoomCenterX);
                    }
                    else
                    {
                        CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                        CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, currentRoomCenterY);
                    }
                }
            }
            //Iterate through each room, create them
            foreach (Rectangle room in map.Rooms)
            {
                CreateRoom(room);
                CreateDoors(room);
            }

            CreateStairs();

            PlacePlayer();

            PlaceMonsters();

            return map;
        }

        private void CreateRoom(Rectangle room)
        {
            for(int i = room.Left + 1; i < room.Right; i++)
            {
                for(int j = room.Top + 1; j < room.Bottom; j++)
                {
                    map.SetCellProperties(i, j, true, true);
                }
            }
        }

        private void PlacePlayer()
        {
            Player player = Game.Player;
            if (player == null) player = new Player();

            player.X = map.Rooms[0].Center.X;
            player.Y = map.Rooms[0].Center.Y;

            map.AddPlayer(player);
        }
        // Carve a tunnel out of the map parallel to the x-axis
        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                map.SetCellProperties(x, yPosition, true, true);
            }
        }

        // Carve a tunnel out of the map parallel to the y-axis
        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                map.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void PlaceMonsters()
        {
            foreach(var room in map.Rooms)
            {
                //Each room has a 70% chance of spawning monsters
                if(Dice.Roll("1D10") < 8)
                {
                    var numberOfMonsters = Dice.Roll("1D4");
                    for(int i = 0; i < numberOfMonsters; i++)
                    {
                        //Find a random walkable location in the room...
                        Point randomRoomLocation = map.GetRandomWalkableLocation(room);
                        //...unless there's no space for them
                        if(randomRoomLocation != null)
                        {
                            //Temporarily force them to be level 1
                            //TODO: Find a way to more efficiently spawn from a group of possible monsters
                            Monster monster;
                            int monSpawn = Dice.Roll("1D10");
                            if (monSpawn < 4)
                            {
                                 monster = Crowbold.Create(1);
                            }
                            else
                            {
                                 monster = Slug.Create(1);
                            }
                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;
                            map.AddMonster(monster);
                        }
                    }
                }
            }
        }

        private void CreateDoors(Rectangle room)
        {
            //Boundries of the room
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            //Put the room's border cells into a list
            List<Cell> borderCells = map.GetCellsAlongLine(xMin, yMin, xMax, yMin).ToList();
            borderCells.AddRange( map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCells.AddRange( map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCells.AddRange( map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            //Go through each of the room's border cells and look for places to place doors
            foreach(Cell cell in borderCells)
            {
                if(IsPotentialDoor(cell))
                {
                    //A door has to block FOV when it's closed
                    map.SetCellProperties(cell.X, cell.Y, false, true);
                    map.Doors.Add(new Door { X = cell.X, Y = cell.Y, IsOpen = false });
                }
            }
        }

        private bool IsPotentialDoor(Cell cell)
        {
            if (!cell.IsWalkable) return false;

            //Store references to all neighboring cells
            Cell right = map.GetCell(cell.X + 1, cell.Y);
            Cell left = map.GetCell(cell.X - 1, cell.Y);
            Cell top = map.GetCell(cell.X, cell.Y - 1);
            Cell bottom = map.GetCell(cell.X, cell.Y + 1);

            //Make sure there's not a door already there
            if (map.GetDoor(cell.X, cell.Y) != null || map.GetDoor(right.X, right.Y) != null || map.GetDoor(left.X, left.Y) != null || map.GetDoor(top.X, top.Y) != null || map.GetDoor(bottom.X, bottom.Y) != null)
                return false;

            //This is a good place on the left or right side
            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)
                return true;

            //Good place on top or bottom
            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)
                return true;

            return false;
        }

        private void CreateStairs()
        {
            map.StairsUp = new Stairs { X = map.Rooms.First().Center.X + 1, Y = map.Rooms.First().Center.Y, IsUp = true };
            map.StairsDown = new Stairs { X = map.Rooms.Last().Center.X, Y = map.Rooms.Last().Center.Y, IsUp = false };
        }
    }
}
