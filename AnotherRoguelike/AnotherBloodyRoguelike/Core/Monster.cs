using AnotherRoguelike.Behaviors;
using AnotherRoguelike.Systems;
using RLNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Core
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }

        public void DrawStats(RLConsole statConsole, int position)
        {
            //Start at y=13, right under player stats
            //Multiply by 2 to leave space twixt each stat
            int yPosition = 13 + (position * 2);

            //Print the symbol and color of the creature
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            //Get width of HP Bar by dividing hp by max hp
            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            //Set background color to show how damaged the monster is
            statConsole.SetBackColor(3, yPosition, width, 1, RLColor.Red);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, RLColor.Gray);

            //Print the monster name over the health bar
            statConsole.Print(2, yPosition, $": {Name}", RLColor.White);
        }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behavior = new StandardMoveAndAttack();
            behavior.Act(this, commandSystem);
        }
    }
}
