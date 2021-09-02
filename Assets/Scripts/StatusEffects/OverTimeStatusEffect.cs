using Server.Character;
using Shared.Abilities;
using UnityEngine;

namespace StatusEffects
{
    public class OverTimeStatusEffect : StatusEffect
    {
        private float nextTick;
        public override bool Start()
        {
            nextTick = Time.time + Description.delay;
            return true;
        }

        public override bool Update()
        {
            if (Time.time > nextTick)
            { 
                nextTick = Time.time + Description.delay;
                RunEffects();
            }

            return true;
        }

        private void RunEffects()
        {
            foreach (var effect in Description.HitEffects)
            {
                // var runtimeParams = new AbilityRuntimeParams(effect.)
                // target.NetworkCharacterState.CastAbilityServerRpc();
            }
        }

        public override void End()
        {
            throw new System.NotImplementedException();
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }

        public OverTimeStatusEffect(ulong source, ServerCharacter target, StatusEffectType type) : base(source, target, type)
        {
        }
    }
}