namespace Collectable
{
    using UnityEngine;
    using System.Collections.Generic;

    [RequireComponent(typeof(CollectablesController))]
    public class CollectablesControllerView : MonoBehaviour
    {
        [SerializeField] protected CollectableUI uiPref = default;
        [SerializeField] protected GameObject collectablesPanel = default;

        protected CollectableUI tempUI = default;
        protected CollectableView tempView = default;
        protected CollectablesController controller = default;

        protected virtual void Awake()
            => controller = GetComponent<CollectablesController>();

        protected virtual void OnEnable()
            => controller.OnCollectablesSpawned += UpdateCollectablesView;

        protected virtual void OnDisable()
            => controller.OnCollectablesSpawned -= UpdateCollectablesView;

        protected virtual void UpdateCollectablesView(Queue<AbstractCollectable> data)
        {
            foreach (AbstractCollectable collectable in data)
            {
                if (collectable.TryGetComponent(out tempView))
                {
                    tempUI = Instantiate(uiPref, collectablesPanel.transform);
                    tempUI.InitCollectableUI(tempView.spriteRenderer.sprite);
                }
            }
        }
    }
}