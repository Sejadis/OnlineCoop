using MLAPI;

namespace Shared.Abilities
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