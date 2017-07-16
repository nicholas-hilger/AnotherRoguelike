using AnotherRoguelike.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Systems
{
    public class SchedulingSystem
    {
        //The word 'schedule' and its variants have lost all meaning to me.
        private int time;
        private readonly SortedDictionary<int, List<ISchedulable>> schedulables;

        public SchedulingSystem()
        {
            time = 0;
            schedulables = new SortedDictionary<int, List<ISchedulable>>();
        }

        //Add a new object to the schedule
        //Place it at the current time plus their Time property
        public void Add(ISchedulable schedulable)
        {
            int key = time + schedulable.Time;
            if(!schedulables.ContainsKey(key))
            {
                schedulables.Add(key, new List<ISchedulable>());
            }
            schedulables[key].Add(schedulable);
        }

        public void Remove(ISchedulable schedulable)
        {
            KeyValuePair<int, List<ISchedulable>> schedulableListFound = new KeyValuePair<int, List<ISchedulable>>(-1, null);

            foreach(var schedulablesList in schedulables)
            {
                if(schedulablesList.Value.Contains(schedulable))
                {
                    schedulableListFound = schedulablesList;
                    break;
                }
            }
            if(schedulableListFound.Value != null)
            {
                schedulableListFound.Value.Remove(schedulable);
                if(schedulableListFound .Value.Count <= 0)
                {
                    schedulables.Remove(schedulableListFound.Key);
                }
            }
        }

        //Get the next object whose turn it is in the schedule. Advance time if needed
        public ISchedulable Get()
        {
            var firstSchedulableGroup = schedulables.First();
            var firstSchedulable = firstSchedulableGroup.Value.First();
            Remove(firstSchedulable);
            time = firstSchedulableGroup.Key;
            return firstSchedulable;
        }

        public int GetTime()
        {
            return time;
        }

        public void Clear()
        {
            time = 0;
            schedulables.Clear();
        }
    }
}
