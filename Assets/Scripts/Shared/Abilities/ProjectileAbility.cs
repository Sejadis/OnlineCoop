using MLAPI;
using Server.Ability;
using UnityEngine;

namespace Shared.Abilities
{
    public class ProjectileAbility : Ability
    {
        private NetworkObject projectileNetObject;
        private bool didStart;

        public override bool Start()
        {
            CanStartCooldown = false;
            if (DidCastTimePass)
            {
                FireProjectile();
            }

            return true;
        }

        private void OnHit(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public override bool Update()
        {
            if (!didStart && DidCastTimePass)
            {
                FireProjectile();
            }
            return !didStart || Vector3.Distance(abilityRuntimeParams.StartPosition, projectileNetObject.transform.position) <
                   Description.range;
        }

        public override void End()
        {
            CanStartCooldown = true;
            projectileNetObject?.Despawn(true);
        }

        public override bool Reactivate()
        {
            CanStartCooldown = true;
            return false;
        }

        private void FireProjectile()
        {
            didStart = true;
            var desc = Description;
            var projectile =
                Object.Instantiate(desc.Prefabs[0], abilityRuntimeParams.StartPosition, Quaternion.identity);
            projectile.transform.forward = abilityRuntimeParams.TargetDirection;
            var serverLogic = projectile.GetComponent<ServerProjectile>();
            serverLogic.Initialize(desc.speed, desc.range, desc.HitEffects, abilityRuntimeParams.Actor);
            serverLogic.OnHit += OnHit;
            projectileNetObject = projectile.GetComponent<NetworkObject>();
            projectileNetObject.Spawn();
        }

        public ProjectileAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}