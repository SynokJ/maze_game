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
        public GameObject playerToSave = default;
        public GameObject playerToSpawn = default;

        public void InitPlayer(GameObject player, bool rewriteStatus = false)
        {
            playerToSpawn = player;
            if (rewriteStatus)
            {
                playerToSave = player;
            }
            OnPlayerInited();
        }

        public void DestroyPlayer()
        {
            Destroy(playerToSpawn);
            OnPlayerDestroyed();
        }
    }
}