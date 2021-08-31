using System.Diagnostics;
using MLAPI;
using Server.Ability;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Shared.Abilities
{
    public class ProjectileAbility : Ability
    {
        private NetworkObject projectileNetObject;
        private bool didStart;
        private Stopwatch watch = new Stopwatch();

        public override bool Start()
        {
            CanStartCooldown = false;
            if (DidCastTimePass)
            {
                FireProjectile();
            }

            return true;
        }

        public override bool Update()
        {
            if (!didStart && DidCastTimePass)
            {
                watch.Stop();
                FireProjectile();
            }

            return !didStart
                    || projectileNetObject.IsSpawned
                    && Vector3.Distance(AbilityRuntimeParams.StartPosition, projectileNetObject.transform.position) <
                    Description.range;
        }

        public override void End()
        {
            CanStartCooldown = true;
            if (projectileNetObject.IsSpawned)
            {
                projectileNetObject.Despawn(true);
            }
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
                Object.Instantiate(desc.Prefabs[0], AbilityRuntimeParams.StartPosition, Quaternion.identity);
            projectile.transform.forward = AbilityRuntimeParams.TargetDirection;
            var serverLogic = projectile.GetComponent<ServerProjectile>();
            serverLogic.Initialize(this);
            projectileNetObject = projectile.GetComponent<NetworkObject>();
            projectileNetObject.Spawn();
        }

        public ProjectileAbility(ref AbilityRuntimeParams abilityRuntimeParams) : base(ref abilityRuntimeParams)
        {
        }
    }
}