namespace Player
{
    using Shop;
    using UnityEngine;

    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] protected GameObject playerPref = default;
        [SerializeField] protected Transform positionToSpawn = default;
        [SerializeField] protected PlayerContainer playerContainer = default;
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected GameObject tempPlayer = default;

        protected virtual void Start()
            => SpawnPlayer();

        protected virtual void ResetPlayer()
        {
            if (tempPlayer != null)
            {
                playerContainer.DestroyPlayer();
            }
        }

        protected virtual void SpawnPlayer()
        {
            ResetPlayer();

            tempPlayer = shopDataContainer.PlayerControllers[shopDataContainer.CurrentId].gameObject;
            tempPlayer = Instantiate(tempPlayer, positionToSpawn.position, Quaternion.identity);
            playerContainer.InitPlayer(tempPlayer);
        }
    }
}