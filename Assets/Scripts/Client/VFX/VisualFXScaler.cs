using UnityEngine;

namespace Client.VFX
{
    public abstract class VisualFXScaler : MonoBehaviour
    {
        public abstract void Scale(float scale);

        [ContextMenu("Test Scale 2x")]
        private void TestScale()
        {
            Scale(2f);
        }
    }
}
