using MLAPI.Spawning;

namespace Server.TargetEffects
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