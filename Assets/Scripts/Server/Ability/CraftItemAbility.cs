using MLAPI.Spawning;
using Shared.Abilities;

namespace Server.Ability
{
    public class CraftItemAbility : Ability
    {
        // Start is called before the first frame update
        public override bool Start()
        {
            //CraftingManager.CraftItem(Description.IDs[0])....
            NetworkSpawnManager.SpawnedObjects[AbilityRuntimeParams.Targets[0]].GetComponent<PlayerInventory>().AddItem();
            return false;
        }

        // Update is called once per frame
        public override bool Update()
        {
            return false;
        }

        public CraftItemAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}
