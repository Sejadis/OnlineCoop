using UnityEngine;

namespace Client.VFX
{
    public class ZoneFxScaler : VisualFXScaler
    {
        [SerializeField] private new ParticleSystem particleSystem;
        public override void Scale(float scale)
        {
            particleSystem.gameObject.transform.localScale *= scale;
            var emission = particleSystem.emission;
            var rate = emission.rateOverTime.constant;
            var radius = particleSystem.shape.radius;
            var newRadius =  radius * scale;
            var oldArea = radius * radius * Mathf.PI;
            var newArea = newRadius * newRadius * Mathf.PI;
            var emissionFactor = newArea / oldArea;
            var emissionRateOverTime = emission.rateOverTime;
            emissionRateOverTime.constant = emissionFactor * rate;
            emission.rateOverTime = emissionRateOverTime;
        }
    }
}
