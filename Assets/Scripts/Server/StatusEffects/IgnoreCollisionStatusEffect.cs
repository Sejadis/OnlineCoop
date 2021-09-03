using System.Collections.Generic;
using MLAPI.Spawning;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.StatusEffects
{
    public class IgnoreCollisionStatusEffect : StatusEffect
    {
        private Collider targetCollider;
        private List<Collider> otherCollider = new List<Collider>();

        public IgnoreCollisionStatusEffect(ref StatusEffectRuntimeParams runtimeParams) : base(ref runtimeParams)
        {
        }

        public override bool Start()
        {
            var core = base.Start();
            targetCollider = target.GetComponent<Collider>();
            for (int i = 1; i < runtimeParams.targets.Length; i++)
            {
                otherCollider.Add(NetworkSpawnManager.SpawnedObjects[runtimeParams.targets[i]]
                    .GetComponent<Collider>());
                Physics.IgnoreCollision(targetCollider, otherCollider[i], true);
            }

            return core;
        }

        public override bool Update()
        {
            return true;
        }

        public override void End()
        {
            foreach (var collider in otherCollider)
            {
                Physics.IgnoreCollision(targetCollider, collider, false);

            }
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}