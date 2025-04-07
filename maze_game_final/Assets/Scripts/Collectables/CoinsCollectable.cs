namespace Collectable
{
    using Params;
    using UnityEngine;

    public class CoinsCollectable : AbstractCollectable
    {
        [SerializeField, Min(0)] protected int val = 0;
        [SerializeField] protected IntegerContainer coinsAmount = default;

        protected override void ActivateCollectable()
        {
            base.ActivateCollectable();
            coinsAmount.Value += val;
        }

        protected override void ResetCollectableEffect()
        {
            // TODO
        }
    }
}
