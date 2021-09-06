using System.Collections.Generic;
using MLAPI.Spawning;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.StatusEffects
{
    public class IgnoreCollisionStatusEffect : StatusEffect
    {
        private Collider targetCollider;
        private List<Collider> otherColliders = new List<Collider>();

        public IgnoreCollisionStatusEffect(ref StatusEffectRuntimeParams runtimeParams) : base(ref runtimeParams)
        {
        }

        public override bool Start()
        {
            var core = base.Start();
            targetCollider = target.GetComponent<Collider>();
            for (int i = 1; i < runtimeParams.targets.Length; i++)
            {
                var collider = NetworkSpawnManager.SpawnedObjects[runtimeParams.targets[i]]
                    .GetComponent<Collider>();
                otherColliders.Add(collider);
                Physics.IgnoreCollision(targetCollider, otherColliders[i - 1], true);
            }

            return core;
        }

        public override bool Update()
        {
            return true;
        }

        public override void End()
        {
            foreach (var collider in otherColliders)
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