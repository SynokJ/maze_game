namespace Player
{
    using UnityEngine;

    public abstract class AbstractPlayerProvider : MonoBehaviour
    {
        [SerializeField] protected PlayerContainer playerContainer = default;

        protected PlayerMovement controller = default;

        protected virtual void OnEnable()
        {
            playerContainer.OnPlayerInited += InitPlayer;
            playerContainer.OnPlayerDestroyed += DestroyPlayer;
        }

        protected virtual void OnDisable()
        {
            playerContainer.OnPlayerInited -= InitPlayer;
            playerContainer.OnPlayerDestroyed -= DestroyPlayer;
        }

        protected virtual void InitPlayer()
            => playerContainer.playerToSpawn.TryGetComponent(out controller);

        protected abstract void DestroyPlayer();
    }
}