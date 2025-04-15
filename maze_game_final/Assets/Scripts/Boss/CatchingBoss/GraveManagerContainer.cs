namespace Boss.CatchingBoss
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Boss/CatchingBoss/" + nameof(GraveManagerContainer), fileName = nameof(GraveManagerContainer))]
    public class GraveManagerContainer : ScriptableObject
    {
        protected bool isInited = false;
        public event Action OnPlayerInited = delegate { };
        public event Action OnPlayerDestroyed = delegate { };
        public GameObject graveManager = default;

        public void InitGraveManager(GameObject graveManager)
        {
            this.graveManager = graveManager;
            isInited = graveManager != null;
            OnPlayerInited();
        }

        public void DestroyGraveManager()
        {
            Destroy(graveManager.gameObject);
            OnPlayerDestroyed();
        }
    }
}