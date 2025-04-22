namespace Shop
{
    using Player;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ShopSetButton : MonoBehaviour
    {
        [SerializeField] protected PlayerContainer playerContainer = default;
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected Button currentButton = default;

        protected virtual void Awake()
            => currentButton = GetComponent<Button>();

        protected virtual void OnEnable()
            => currentButton.onClick.AddListener(SetCurrentPlayer);

        protected virtual void OnDisable()
            => currentButton.onClick.RemoveListener(SetCurrentPlayer);

        protected virtual void SetCurrentPlayer()
            => playerContainer.InitPlayer(shopDataContainer.CurrentPlayerController.gameObject, true);
    }
}
