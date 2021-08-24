using System.Collections.Generic;
using MLAPI;
using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
{
    public class AoeAbility : Ability
    {
        private NetworkObject spawnedZone;

        public override bool Start()
        {
            CreateZone();
            return true;
        }

        public override bool Update()
        {
            return true;
        }

        public override void End()
        {
            spawnedZone.Despawn(true);
        }

        public override bool IsBlocking()
        {
            return false;
        }

        private void CreateZone()
        {
            var desc = Description;
            var zone = Object.Instantiate(desc.Prefabs[0],abilityRuntimeParams.TargetPosition,Quaternion.identity);
            var serverLogic = zone.GetComponent<ServerAoeZone>();
            serverLogic.Initialize(desc.duration, desc.range,Description.HitEffects,abilityRuntimeParams.Actor);
            spawnedZone = zone.GetComponent<NetworkObject>();
            spawnedZone.Spawn();
        }

        public AoeAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}