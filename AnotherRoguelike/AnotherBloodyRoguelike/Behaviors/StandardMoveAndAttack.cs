using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AnotherRoguelike.Interfaces;
using AnotherRoguelike.Core;
using AnotherRoguelike.Systems;
using RogueSharp;

namespace AnotherRoguelike.Behaviors
{
    public class StandardMoveAndAttack : IBehavior
    {
        public bool Act(Monster monster, CommandSystem commandSystem)
        {
            DungeonMap dungeonMap = Game.DungeonMap;
            Player player = Game.Player;
            FieldOfView monsterFov = new FieldOfView(dungeonMap);

            //If the monster hasn't been alerted, compute an FOV
            //Use awareness in the FOV check
            //If the player is in their FOV, alert the monster
            //Add a message to the log
            if(!monster.TurnsAlerted.HasValue)
            {
                monsterFov.ComputeFov(monster.X, monster.Y, monster.Awareness, true);
                if(monsterFov.IsInFov(player.X,player.Y))
                {
                    Game.MessageLog.Add($"{monster.Name} is eager to fight {player.Name}");
                    monster.TurnsAlerted = 1;
                }
            }
            
            if(monster.TurnsAlerted.HasValue)
            {
                //Make sure to make the monster and player Cells walkable
                dungeonMap.SetIsWalkable(monster.X, monster.Y, true);
                dungeonMap.SetIsWalkable(player.X, player.Y, true);

                PathFinder pathFinder = new PathFinder(dungeonMap);
                Path path = null;

                try
                {
                    path = pathFinder.ShortestPath(dungeonMap.GetCell(monster.X, monster.Y), dungeonMap.GetCell(player.X, player.Y));
                }
                catch(PathNotFoundException)
                {
                    //If they can't find a path, make them wait
                    Game.MessageLog.Add($"{monster.Name} waits for a turn.");
                }

                //Make sure to set IsWalkable back to false
                dungeonMap.SetIsWalkable(monster.X, monster.Y, false);
                dungeonMap.SetIsWalkable(player.X, player.Y, false);

                //If there was a path, tell the CommandSystem to move the monster
                if(path != null)
                {
                    try
                    {
                        //TODO: Should be path.StepForward(), there's a bug in RogueSharp V3
                        commandSystem.MoveMonster(monster, path.Steps.First());
                    }
                    catch(NoMoreStepsException)
                    {
                        Game.MessageLog.Add($"{monster.Name} grumbles in frustration.");
                    }
                }

                monster.TurnsAlerted++;

                //Lose alerted status every 15 turns
                //They'll stay alerted as long as you're in their FOV
                if(monster.TurnsAlerted > 15)
                {
                    monster.TurnsAlerted = null;
                }
            }
            return true;
        }
    }
}
