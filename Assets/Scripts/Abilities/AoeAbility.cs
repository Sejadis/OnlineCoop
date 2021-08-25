using System;
using MLAPI;
using MLAPI.Spawning;
using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
{
    public class AoeAbility : Ability
    {
        private Collider[] overlapResults = new Collider[10];
        protected NetworkState actor;
        
        public AoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkState>()
                .CastAbilityClientRpc(abilityRuntimeParams);
            actor = NetworkSpawnManager.SpawnedObjects[abilityRuntimeParams.Actor].GetComponent<NetworkState>();
            RunHitCheck();
            return false;
        }

        public override bool Update()
        {
            throw new NotImplementedException();
        }

        protected void RunHitCheck()
        {
            var size = Physics.OverlapSphereNonAlloc(abilityRuntimeParams.TargetPosition, Description.range,
                overlapResults);
            for (var i = 0; i < size; i++)
            {
                var result = overlapResults[i];
                var netObj = result.GetComponent<NetworkObject>();
                if (netObj != null)
                {
                    if (actor != null)
                    {
                        foreach (var effect in Description.HitEffects)
                        {
                            var runtimeParams = new AbilityRuntimeParams(effect, abilityRuntimeParams.Actor, netObj.NetworkObjectId, result.transform.position,
                                Vector3.zero, abilityRuntimeParams.TargetPosition);
                            actor.CastAbilityServerRpc(runtimeParams);
                        }
                    }
                    else
                    {
                        //TODO try run on target?
                    }
                }
            }
        }

        public override void End()
        {
            throw new System.NotImplementedException();
        }

        public override bool IsBlocking()
        {
            return false;
        }
    }
}