using Server.TargetEffects;
using Shared.StatusEffects;
using UnityEngine;

namespace Server.StatusEffects
{
    public class OverTimeStatusEffect : StatusEffect
    {
        private float elapsedTime;

        public override bool Start()
        {
            base.Start();
            RunEffects();
            return true;
        }

        public override bool Update()
        {
            elapsedTime += Time.deltaTime;
            if (Description.delay <= elapsedTime)
            {
                RunEffects();
            }

            return true;
        }

        private void RunEffects()
        {
            elapsedTime = 0;
            var effectRuntimeParams = new TargetEffectParameter(
                targets: new[] {actor},
                actor: source,
                targetDirection: Vector3.zero,
                statusEffectType: type
                // targetPosition: targetPosition,
                // startPosition: startPosition,
                // effectType: hitEffect.EffectType,
            );
            foreach (var effect in Description.HitEffects)
            {
                TargetEffect.GetEffectByType(effect.EffectType, effectRuntimeParams).Run();
            }
        }

        public override void End()
        {
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public OverTimeStatusEffect(ref StatusEffectRuntimeParams runtimeParams) : base(ref runtimeParams)
        {
        }
    }
}