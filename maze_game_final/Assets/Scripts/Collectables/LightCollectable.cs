namespace Collectable
{
    using Params;
    using UnityEngine;

    public class LightCollectable : AbstractCollectable
    {
        [SerializeField, Min(0.0f)] protected float lightValue = 0.0f;
        [SerializeField] protected FloatContainer lightOuterRadius = default;

        protected override void ActivateCollectable()
        {
            base.ActivateCollectable();
            lightOuterRadius.Value = lightValue;
        }

        protected override void ResetCollectableEffect()
            => lightOuterRadius.ResetValue();
    }
}
