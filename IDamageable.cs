using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public interface IDamageable
    {
        void TakeDamage(int amount);
        bool IsAlive();
    }
}