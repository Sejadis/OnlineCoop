using Shared.Abilities;

namespace Client.VFX
{
    public class AoeZoneVfx : AbilityVfx
    {
        public AoeZoneVfx(ref AbilityRuntimeParams abilityRuntimeParams) : base(
            ref abilityRuntimeParams)
        {
        }

        public override bool Start()
        {
            TrySpawnPrefab();
            return true;
        }

        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }

        protected override bool TrySpawnPrefab(int prefabIndex = 0)
        {
            if (base.TrySpawnPrefab(prefabIndex))
            {
                var scaler = EffectTransform.GetComponent<VisualFXScaler>();
                if (scaler != null)
                {
                    scaler.Scale(Description.size);
                }
            }

            return true;
        }
    }
}