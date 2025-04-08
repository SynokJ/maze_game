namespace Collectable
{
    using UnityEngine;
    using System.Collections.Generic;

    [RequireComponent(typeof(CollectablesController))]
    public class CollectablesControllerView : MonoBehaviour
    {
        [SerializeField] protected CollectableUI uiPref = default;
        [SerializeField] protected GameObject collectablesPanel = default;
        [SerializeField] protected CollectablesDataSO collectablesContainer = default;

        protected CollectableUI tempUI = default;
        protected CollectableView tempView = default;
        protected CollectablesController controller = default;
        protected Queue<CollectableUI> spawnedCollectables = new Queue<CollectableUI>();

        protected virtual void Awake()
            => controller = GetComponent<CollectablesController>();

        protected virtual void OnEnable()
        {
            collectablesContainer.OnCollectablesChanged += InitCollectablesView;
        }

        protected virtual void OnDisable()
        {
            collectablesContainer.OnCollectablesChanged -= InitCollectablesView;
        }

        protected virtual void InitCollectablesView()
        {
            ResetView();
            foreach (AbstractCollectable collectable in collectablesContainer.AvailableCollectable)
            {
                if (collectable.TryGetComponent(out tempView))
                {
                    tempUI = Instantiate(uiPref, collectablesPanel.transform);
                    tempUI.InitCollectableUI(tempView.spriteRenderer.sprite);
                    spawnedCollectables.Enqueue(tempUI);
                }
            }
        }

        protected virtual void ResetView()
        {
            foreach(CollectableUI tempUI in spawnedCollectables)
            {
                Destroy(tempUI.gameObject);
            }
            spawnedCollectables.Clear();
        }
    }
}