using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class SpawnObjectAbility : Ability
    {
        private ServerPlaceableObject spawnedObject;

        public SpawnObjectAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            var desc = Description;
            var zone = Object.Instantiate(desc.Prefabs[0], abilityRuntimeParams.TargetPosition, Quaternion.identity);
            spawnedObject = zone.GetComponent<ServerPlaceableObject>();
            spawnedObject.Initialize(Description);
            spawnedObject.NetworkObject.Spawn();
            return true;
        }

        public override bool Update()
        {
            return true;
        }

        public override void End()
        {
            spawnedObject.NetworkObject.Despawn(true);
        }

        public override bool IsBlocking()
        {
            return false;
        }
    }
}