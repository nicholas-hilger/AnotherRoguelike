﻿using System;
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
        private int attack;
        private int attChance;
        private int awareness;
        private int defense;
        private int defChance;
        private int gold;
        private int health;
        private int maxHealth;
        private string name;
        private int speed;
        private int xp;
        private int maxXp;

        //IDrawable
        public RLColor Color { get; set; }
        public char Symbol { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public int Attack
        {
            get
            {
                return attack;
            }

            set
            {
                attack = value;
            }
        }

        public int AttChance
        {
            get
            {
                return attChance;
            }

            set
            {
                attChance = value;
            }
        }

        public int Defense
        {
            get
            {
                return defense;
            }

            set
            {
                defense = value;
            }
        }

        public int DefChance
        {
            get
            {
                return defChance;
            }

            set
            {
                defChance = value;
            }
        }

        public int Gold
        {
            get
            {
                return gold;
            }

            set
            {
                gold = value;
            }
        }

        public int Health
        {
            get
            {
                return health;
            }

            set
            {
                health = value;
            }
        }

        public int MaxHealth
        {
            get
            {
                return maxHealth;
            }

            set
            {
                maxHealth = value;
            }
        }

        public int Speed
        {
            get
            {
                return speed;
            }

            set
            {
                speed = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }

        public int Awareness
        {
            get
            {
                return awareness;
            }
            set
            {
                awareness = value;
            }
        }

        public int Xp
        {
            get
            {
                return xp;
            }

            set
            {
                xp = value;
            }
        }

        public int MaxXp
        {
            get
            {
                return maxXp;
            }

            set
            {
                maxXp = value;
            }
        }

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
