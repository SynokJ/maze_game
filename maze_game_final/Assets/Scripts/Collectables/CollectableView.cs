namespace Collectable
{
    using UnityEngine;

    [RequireComponent(typeof(AbstractCollectable))]
    public class CollectableView : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer => collectableRenderer;

        [SerializeField] protected Collider2D collectableCollider = default;
        [SerializeField] protected SpriteRenderer collectableRenderer = default;

        protected AbstractCollectable collectable = default;

        protected virtual void Awake()
            => collectable = GetComponent<AbstractCollectable>();

        protected virtual void OnEnable()
            => collectable.OnCollectablePicked += HideCollectable;


        protected virtual void OnDisable()
            => collectable.OnCollectablePicked -= HideCollectable;

        protected virtual void HideCollectable()
        {
            collectableCollider.enabled = false;
            collectableRenderer.enabled = false;
        }
    }
}
