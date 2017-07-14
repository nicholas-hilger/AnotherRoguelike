﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;
using RogueSharp;
using AnotherRoguelike.Core;
using RogueSharp.DiceNotation;

namespace AnotherRoguelike.Monsters
{
    public class Slug : Monster
    {
        public static Slug Create(int level)
        {
            int health = Dice.Roll("2D6");
            return new Slug
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttChance = Dice.Roll("25D3"),
                Awareness = 10,
                Color = RLColor.White,
                Defense = Dice.Roll("1D3") + level / 3,
                DefChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Sluug",
                Speed = 12,
                Symbol = 's'
            };
        }
    }
}
