using Shared.Abilities;
using UnityEngine;

namespace Server.Ability
{
    public class SpawnObjectAbility : Ability
    {
        private ServerPlaceableObject spawnedObject;
        private bool didStart;

        public SpawnObjectAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        private void SpawnObject()
        {
            var desc = Description;
            var zone = Object.Instantiate(desc.Prefabs[0], AbilityRuntimeParams.TargetPosition, Quaternion.identity);
            spawnedObject = zone.GetComponent<ServerPlaceableObject>();
            spawnedObject.Initialize(Description);
            spawnedObject.NetworkObject.Spawn();
        }

        public override bool Start()
        {
            if (DidCastTimePass)
            {
                SpawnObject();
            }
            return true;
        }
        
        public override bool Update()
        {
            if (DidCastTimePass)
            {
                SpawnObject();
            }
            return true;
        }

        public override void End()
        {
            spawnedObject.NetworkObject.Despawn(true);
        }
    }
}