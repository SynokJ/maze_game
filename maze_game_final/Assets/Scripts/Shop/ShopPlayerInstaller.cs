namespace Shop
{
    using Player;
    using UnityEngine;

    public class ShopPlayerInstaller : MonoBehaviour
    {
        [SerializeField] protected Transform positionToSpawn = default;
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected PlayerMovement currentPlauerController = default;

        protected virtual void OnEnable()
            => shopDataContainer.OnPlayerChanged += UpdatePlayer;

        protected virtual void OnDisable()
            => shopDataContainer.OnPlayerChanged -= UpdatePlayer;

        protected virtual void Start()
            => shopDataContainer.CurrentId = 0;

        protected virtual void UpdatePlayer(PlayerMovement playerController)
        {
            ResetCurrentController();
            currentPlauerController = Instantiate(playerController, positionToSpawn.position, Quaternion.identity);
        }

        protected virtual void ResetCurrentController()
        {
            if (currentPlauerController != null)
            {
                Destroy(currentPlauerController.gameObject);
            }
        }
    }
}
