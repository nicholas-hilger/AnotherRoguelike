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
        private readonly int maxRooms;
        private readonly int roomMaxSize;
        private readonly int roomMinSize;

        private readonly DungeonMap map;

        //MapGenerator construction requires the dimensions of the map it'll create
        public MapGenerator(int w, int h,int maxRm, int rmMaxSize, int rmMinSize)
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
                CreateRoom(room);

            PlacePlayer();

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
    }
}
