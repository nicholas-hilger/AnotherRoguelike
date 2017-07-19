using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnotherRoguelike.Core;
using RogueSharp.DiceNotation;
using AnotherRoguelike.Interfaces;
using RogueSharp;
using RLNET;

namespace AnotherRoguelike.Systems
{
    public class CommandSystem
    {
        public bool IsPlayerTurn { get; set; }

        //Return true if the player was able to move
        public bool MovePlayer(Direction direction)
        {
            int x = Game.Player.X;
            int y = Game.Player.Y;

            switch(direction)
            {
                case Direction.Up:
                    {
                        y = Game.Player.Y - 1;
                        break;
                    }
                case Direction.Down:
                    {
                        y = Game.Player.Y + 1;
                        break;
                    }
                case Direction.Left:
                    {
                        x = Game.Player.X - 1;
                        break;
                    }
                case Direction.Right:
                    {
                        x = Game.Player.X + 1;
                        break;
                    }
                default: return false;
            }
            if (Game.DungeonMap.SetActorPosition(Game.Player, x, y))
            {
                return true;
            }
            Monster monster = Game.DungeonMap.GetMonsterAt(x, y);

            if (monster != null)
            {
                Attack(Game.Player, monster);
                return true;
            }
            else
            {
                return false;
            }
        }

        public void EndPlayerTurn()
        {
            IsPlayerTurn = false;
        }

        public void ActivateMonsters()
        {
            ISchedulable schedulable = Game.SchedulingSystem.Get();
            if (schedulable is Player)
            {
                IsPlayerTurn = true;
                Game.SchedulingSystem.Add(Game.Player);
            }
            else
            {
                Monster monster = schedulable as Monster;

                if (monster != null && Game.Player.Health > 0)
                {
                    monster.PerformAction(this);
                    Game.SchedulingSystem.Add(monster);
                }
                ActivateMonsters();
            }
        }

        public void MoveMonster(Monster monster, Cell cell)
        {
            if (!Game.DungeonMap.SetActorPosition(monster, cell.X, cell.Y))
            {
                if (Game.Player.X == cell.X && Game.Player.Y == cell.Y && Game.Player.Health > 0)
                {
                    Attack(monster, Game.Player);
                }
            }
        }

        public void Attack(Actor attacker, Actor defender)
        {
            StringBuilder attMsg = new StringBuilder();
            StringBuilder defMsg = new StringBuilder();

            int hits = ResolveAttack(attacker, defender, attMsg);

            int blocks = ResolveDefense(defender, hits, attMsg, defMsg);

            Game.MessageLog.Add(attMsg.ToString());
            if(!string.IsNullOrWhiteSpace(defMsg.ToString()))
            {
                Game.MessageLog.Add(defMsg.ToString());
            }

            int dmg = hits - blocks;

            ResolveDamage(defender, dmg);
        }

        private static int ResolveAttack(Actor attacker, Actor defender, StringBuilder attackMsg)
        {
            int hits = 0;

            attackMsg.AppendFormat("{0} attacks {1}, ", attacker.Name, defender.Name);

            DiceExpression attDice = new DiceExpression().Dice(attacker.Attack, 100);
            DiceResult attResult = attDice.Roll();

            //Look at the face value of each die rolled
            foreach(TermResult termResult in attResult.Results)
            {
               //attackMsg.Append(termResult.Value + ", ");
                if(termResult.Value >= 100 - attacker.AttChance)
                {
                    hits++;
                }
            }
            return hits;
        }
        // The defender rolls based on his stats to see if he blocks any of the hits from the attacker
        private static int ResolveDefense(Actor defender, int hits, StringBuilder attackMessage, StringBuilder defenseMessage)
        {
            int blocks = 0;

            if (hits > 0)
            {
                attackMessage.AppendFormat("scoring {0} hits.", hits);
                defenseMessage.AppendFormat("  {0} defends, ", defender.Name);

                // Roll a number of 100-sided dice equal to the Defense value of the defendering actor
                DiceExpression defenseDice = new DiceExpression().Dice(defender.Defense, 100);
                DiceResult defenseRoll = defenseDice.Roll();

                // Look at the face value of each single die that was rolled
                foreach (TermResult termResult in defenseRoll.Results)
                {
                    //defenseMessage.Append(termResult.Value + ", ");
                    // Compare the value to 100 minus the defense chance and add a block if it's greater
                    if (termResult.Value >= 100 - defender.DefChance)
                    {
                        blocks++;
                    }
                }
                defenseMessage.AppendFormat("resulting in {0} blocks.", blocks);
            }
            else
            {
                attackMessage.Append("and misses completely.");
            }

            return blocks;
        }

        // Apply any damage that wasn't blocked to the defender
        private static void ResolveDamage(Actor defender, int damage)
        {
            if (damage > 0)
            {
                defender.Health = defender.Health - damage;

                Game.MessageLog.Add($"  {defender.Name} was hit for {damage} damage");

                if (defender.Health <= 0)
                {
                    ResolveDeath(defender);
                }
            }
            else
            {
                Game.MessageLog.Add($"  {defender.Name} blocked all damage");
            }
        }

        // Remove the defender from the map and add some messages upon death.
        private static void ResolveDeath(Actor defender)
        {
            if (defender is Player)
            {
                Game.MessageLog.Clear();
                defender.Health = 0;
                string plural = "s";
                if (Game.Player.Kills == 1) plural = "";
                Game.MessageLog.Add($"{defender.Name} died a(n) {Adjectives.AdjGen().ToLower()} death.");
                Game.MessageLog.Add("");
                Game.MessageLog.Add($"You managed to slay {Game.Player.Kills} monster{plural}.");
                Game.MessageLog.Add("");
                Game.MessageLog.Add($"You accrued {defender.Gold} gold (that you can't take with you). ");
                Game.MessageLog.Add("");
                Game.MessageLog.Add($"You made it to Floor {Game.Player.floor} before dying, having taken {Game.steps} steps.");
                Game.MessageLog.Add("");
                Game.MessageLog.Add("Press ESC to quit...");
            }
            else if (defender is Monster)
            {
                Game.DungeonMap.RemoveMonster((Monster)defender);
                Game.Player.Xp += defender.Xp;
                Game.Player.Kills++;
                Game.MessageLog.Add($"  {defender.Name} died and dropped {defender.Gold} gold");
            }
        }
    }
}
