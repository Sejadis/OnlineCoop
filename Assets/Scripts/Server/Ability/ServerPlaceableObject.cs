using MLAPI;
using Shared.Abilities;

namespace Server.Ability
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