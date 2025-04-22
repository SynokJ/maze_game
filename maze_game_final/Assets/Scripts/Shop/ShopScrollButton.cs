namespace Shop
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ShopScrollButton : MonoBehaviour
    {
        [SerializeField] protected ShopDataContainer shopDataContainer = default;
        [SerializeField] protected bool isMoveNext = false;

        protected Button currentButton = default;

        protected virtual void Awake()
            => currentButton = GetComponent<Button>();

        protected virtual void OnEnable()
            => currentButton.onClick.AddListener(SwitchPlayer);

        protected virtual void OnDisable()
            => currentButton.onClick.RemoveListener(SwitchPlayer);

        protected virtual void SwitchPlayer()
        {
            if (isMoveNext)
            {
                shopDataContainer.CurrentId++;
            } else
            {
                shopDataContainer.CurrentId--;
            }
        }
    }
}
