using MLAPI.Spawning;
using Server.Character;

namespace Server.TargetEffects
{
    public class ForceMoveAbility : TargetEffect
    {
        public ForceMoveAbility(TargetEffectParameter targetEffectParameter) : base(targetEffectParameter)
        {
        }

        public override void Run()
        {
            if (NetworkSpawnManager.SpawnedObjects.TryGetValue(EffectParameter.Target, out var netObj))
            {
                netObj.GetComponent<ServerCharacter>()?.ForceMove(EffectParameter.TargetDirection, SourceDescription.force);
            }
        }
    }
}