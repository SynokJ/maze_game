namespace Collectable
{
    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    [RequireComponent(typeof(AbstractCollectable))]
    public class CollectableView : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer => collectableRenderer;

        [SerializeField] protected Light2D collectableLight = default;
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
            collectableLight.enabled = false;
            collectableCollider.enabled = false;
            collectableRenderer.enabled = false;
        }
    }
}
