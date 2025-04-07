namespace Collectable
{
    using Params;
    using UnityEngine;

    public class SpeedCollectable : AbstractCollectable
    {
        [SerializeField, Min(0.0f)] protected float speed = 0.0f;
        [SerializeField] protected FloatContainer playerSpeed = default;

        protected override void ActivateCollectable()
        {
            base.ActivateCollectable();
            playerSpeed.Value = speed;
        }

        protected override void ResetCollectableEffect()
            => playerSpeed.ResetValue();
    }
}