namespace Collectable
{
    using Player;
    using System;
    using UnityEngine;

    public abstract class AbstractCollectable : MonoBehaviour
    {
        public event Action OnCollectablePicked = delegate { };

        protected PlayerInteraction playerInteraction = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out playerInteraction))
            {
                ActivateCollectable();
            }
        }

        protected virtual void ActivateCollectable()
        {
            OnCollectablePicked();
        }
    }
}