namespace Player
{
    using UnityEngine;

    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] protected GameObject playerPref = default;
        [SerializeField] protected Transform positionToSpawn = default;
        [SerializeField] protected PlayerContainer playerContainer = default;

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
            if (playerContainer.playerToSave == null)
            {
                tempPlayer = Instantiate(playerPref, positionToSpawn.position, Quaternion.identity);
            }
            else
            {
                tempPlayer = Instantiate(playerContainer.playerToSave, positionToSpawn.position, Quaternion.identity);
            }
            playerContainer.InitPlayer(tempPlayer);
        }
    }
}