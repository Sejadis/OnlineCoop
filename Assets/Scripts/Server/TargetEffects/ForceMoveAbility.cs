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
            GetTarget<ServerCharacter>()?.ForceMove(EffectParameter.TargetDirection, SourceDescription.force);
        }
    }
}