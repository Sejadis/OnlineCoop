using MLAPI;
using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class ProjectileAbility : Ability
    {
        public override bool Start()
        {
            FireProjectile();
            return false;
        }

        private void OnHit(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public override bool Update()
        {
            return false;
        }

        public override void End()
        {
        }

        public override bool IsBlocking()
        {
            return false;
        }

        private void FireProjectile()
        {
            var desc = Description;
            var projectile = Object.Instantiate(desc.Prefabs[0], abilityRuntimeParams.StartPosition, Quaternion.identity);
            projectile.transform.forward = abilityRuntimeParams.TargetDirection;
            var serverLogic = projectile.GetComponent<ServerProjectile>();
            serverLogic.Initialize(desc.speed, desc.range, desc.HitEffects, abilityRuntimeParams.Actor);
            serverLogic.OnHit += OnHit;
            var netObject = projectile.GetComponent<NetworkObject>();
            netObject.Spawn();
        }

        public ProjectileAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}