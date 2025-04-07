namespace Collectable
{
    using Params;
    using System.Collections;
    using UnityEngine;

    public class SpeedCollectable : AbstractCollectable
    {
        [SerializeField, Min(0.0f)] protected float speed = 0.0f;
        [SerializeField, Min(1.0f)] protected float delay = 1.0f;
        [SerializeField] protected FloatContainer playerSpeed = default;

        protected override void ActivateCollectable()
        {
            base.ActivateCollectable();
            playerSpeed.Value = speed;
            StartCoroutine(SpeedLifeTime());
        }

        protected IEnumerator SpeedLifeTime()
        {
            yield return new WaitForSeconds(delay);
            playerSpeed.ResetValue();
        }
    }
}