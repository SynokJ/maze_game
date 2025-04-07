namespace Player
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Player/" + nameof(PlayerContainer), fileName = nameof(PlayerContainer))]
    public class PlayerContainer : ScriptableObject
    {
        public bool isPlayerInited = false;
        public event Action OnPlayerInited = delegate { };
        public event Action OnPlayerDestroyed = delegate { };
        public GameObject player = default;

        public void InitPlayer(GameObject player)
        {
            this.player = player;
            isPlayerInited = player != null;
            OnPlayerInited();
        }

        public void DestroyPlayer()
        {
            Destroy(player);
            OnPlayerDestroyed();
        }
    }
}