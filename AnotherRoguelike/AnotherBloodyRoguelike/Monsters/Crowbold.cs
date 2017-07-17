using System;
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
    public class Crowbold : Monster
    {
        public static Crowbold Create(int level)
        {
            int health = Dice.Roll("2D6");
            return new Crowbold
            {
                Attack = Dice.Roll("1D3") + level / 3,
                AttChance = Dice.Roll("4D6"),
                Awareness = 15,
                Color = RLColor.Brown,
                Defense = Dice.Roll("1D4") + level / 3,
                DefChance = Dice.Roll("6D3"),
                Gold = Dice.Roll("5D6"),
                Health = health,
                MaxHealth = health,
                Name = "Crowbold",
                Speed = 3,
                Xp = Dice.Roll("1D5") + level/3,
                Symbol = 'c'
            };
        }
    }
}
