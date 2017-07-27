using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnotherRoguelike.Interfaces
{
    public interface IAbility
    {
        string Name { get; }
        int TurnsToRefresh { get; }
        int TurnsUntilRefresh { get; }

        bool Perform();
        void Tick();
    }
}
