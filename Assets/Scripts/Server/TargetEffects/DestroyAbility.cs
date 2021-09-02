using MLAPI.Spawning;
using Shared.Abilities;

namespace Server.Ability.TargetEffects
{
    public class DestroyAbility : TargetEffect
    {
        public DestroyAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if(NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Target, out var obj))
            {
               obj.Despawn(true); 
            }
        }
    }
}