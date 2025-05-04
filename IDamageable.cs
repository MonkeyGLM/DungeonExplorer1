using System.Collections.Generic;
using System.Linq;

namespace DungeonExplorer
{
    public interface IDamageable            //allows damagable things to be damaged, and checks if they are alive
    {
        void TakeDamage(int amount);
        bool IsAlive();
    }
}