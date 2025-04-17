namespace Collectable
{
    using Player;
    using System;
    using UnityEngine;
    using System.Collections;

    public abstract class AbstractCollectable : MonoBehaviour
    {
        public event Action OnCollectablePicked = delegate { };
        public bool IsCollected => isCollected;

        [SerializeField] protected CollectablesDataSO collectablesContainer = default;
        [SerializeField, Min(1.0f)] protected float delay = 1.0f;

        protected PlayerInteraction playerInteraction = default;
        protected bool isCollected = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.TryGetComponent(out playerInteraction))
            {
                ActivateCollectable();
            }
        }

        protected virtual void ActivateCollectable()
        {
            isCollected = true;
            OnCollectablePicked();
            collectablesContainer.RemoveCollectable(this);
            StartCoroutine(ActivateByLifeTime());
        }

        protected virtual IEnumerator ActivateByLifeTime()
        {
            yield return new WaitForSeconds(delay);
            ResetCollectableEffect();
        }

        protected abstract void ResetCollectableEffect();
    }
}