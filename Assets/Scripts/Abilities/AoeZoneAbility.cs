using System.Collections.Generic;
using MLAPI;
using MLAPI.Spawning;
using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
{
    public class AoeZoneAbility : AoeAbility
    {
        private float nextTickTime;

        public override bool Start()
        {
            // CreateZone();
            NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkState>()
                .CastAbilityClientRpc(abilityRuntimeParams);
            nextTickTime = Time.time + Description.delay;
            actor = NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkState>();
            return true;
        }

        public override bool Update()
        {
            if (Time.time > nextTickTime)
            {
                nextTickTime = Time.time + Description.delay;
                RunHitCheck();
            }

            return true;
        }

        public override void End()
        {
        }

        public override bool IsBlocking()
        {
            return false;
        }

        public AoeZoneAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}