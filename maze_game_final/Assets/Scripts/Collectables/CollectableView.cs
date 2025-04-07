namespace Collectable
{
    using UnityEngine;

    [RequireComponent(typeof(AbstractCollectable))]
    public class CollectableView : MonoBehaviour
    {
        [SerializeField] protected Collider2D collider = default;
        [SerializeField] protected SpriteRenderer renderer = default;

        protected AbstractCollectable collectable = default;

        protected virtual void Awake()
            => collectable = GetComponent<AbstractCollectable>();

        protected virtual void OnEnable()
            => collectable.OnCollectablePicked += HideCollectable;


        protected virtual void OnDisable()
            => collectable.OnCollectablePicked -= HideCollectable;

        protected virtual void HideCollectable()
        {
            collider.enabled = false;
            renderer.enabled = false;
        }
    }
}
