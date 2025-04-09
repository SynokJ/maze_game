namespace Collectable
{
    using UnityEngine;
    using System.Collections.Generic;
    using System;

    [CreateAssetMenu(menuName = "Collectables/" + nameof(CollectablesDataSO), fileName = nameof(CollectablesDataSO))]
    public class CollectablesDataSO : ScriptableObject
    {
        public event Action OnCollectablesChanged = delegate { };
        public event Action OnCollectablesCollected = delegate { };
        public List<AbstractCollectable> AvailableCollectable => availableCollectables;

        protected List<AbstractCollectable> availableCollectables = new List<AbstractCollectable>();

        protected virtual void OnEnable()
        {
            availableCollectables.Clear();
        }

        public void AddCollectable(AbstractCollectable collectable)
        {
            availableCollectables.Add(collectable);
            OnCollectablesChanged();
        }

        public virtual void RemoveCollectable(AbstractCollectable collectable)
        {
            availableCollectables.Remove(collectable);
            OnCollectablesChanged();

            if(availableCollectables.Count <= 0)
            {
                OnCollectablesCollected();
            }
        }
    }
}
