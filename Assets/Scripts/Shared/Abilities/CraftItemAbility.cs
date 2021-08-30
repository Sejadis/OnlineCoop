using MLAPI.Spawning;
using Server.Ability;

namespace Shared.Abilities
{
    public class CraftItemAbility : Ability
    {
        // Start is called before the first frame update
        public override bool Start()
        {
            //CraftingManager.CraftItem(Description.IDs[0])....
            NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.TargetEntity].GetComponent<PlayerInventory>().AddItem();
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
