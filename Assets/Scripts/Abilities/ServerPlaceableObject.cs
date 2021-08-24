using MLAPI;
using SejDev.Systems.Ability;

namespace Abilities
{
    public abstract class ServerPlaceableObject : NetworkBehaviour
    {
        protected AbilityDescription abilityDescription;

        public void Initialize(AbilityDescription abilityDescription)
        {
            this.abilityDescription = abilityDescription;
        }
    }
}