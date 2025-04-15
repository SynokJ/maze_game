namespace Boss.CatchingBoss
{
    using UnityEngine;

    public abstract class AbstractGraveManagerProvider : MonoBehaviour
    {
        [SerializeField] protected GraveManagerContainer graveManagerContainer = default;

        protected GraveManager controller = default;

        protected virtual void OnEnable()
        {
            graveManagerContainer.OnPlayerInited += InitPlayer;
            graveManagerContainer.OnPlayerDestroyed += DestroyPlayer;
        }

        protected virtual void OnDisable()
        {
            graveManagerContainer.OnPlayerInited -= InitPlayer;
            graveManagerContainer.OnPlayerDestroyed -= DestroyPlayer;
        }

        protected virtual void InitPlayer()
            => graveManagerContainer.graveManager.TryGetComponent(out controller);

        protected abstract void DestroyPlayer();
    }
}