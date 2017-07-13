using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace AnotherRoguelike.Core
{
    public class Player : Actor
    {
        public Player()
        {
            Attack = 4;
            AttChance = 50;
            Defense = 4;
            DefChance = 40;
            Gold = 0;
            Health = 100;
            MaxHealth = 100;
            Awareness = 15;
            Name = "Dalov";
            Color = Colors.Player;
            Symbol = '@';
        }

        public void DrawStats(RLConsole statConsole)
        {
            statConsole.Print(1, 1, $"Name:    {Name}", Colors.Text);
            statConsole.Print(1, 3, $"Health:  {Health}/{MaxHealth}", Colors.Text);
            statConsole.Print(1, 5, $"Attack:  {Attack}  ({AttChance}%)", Colors.Text);
            statConsole.Print(1, 7, $"Defense: {Defense} ({DefChance}%)", Colors.Text);
            statConsole.Print(1, 9, $"Gold:    {Gold}", Colors.Gold);
        }
    }
}
