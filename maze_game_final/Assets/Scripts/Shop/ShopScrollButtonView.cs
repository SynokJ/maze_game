namespace Shop
{
    using UnityEngine;

    [RequireComponent(typeof(ShopScrollButton))]
    public class ShopScrollButtonView : MonoBehaviour
    {
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected ShopScrollButton scrollButton = default;

        protected virtual void Awake()
        {
            scrollButton = GetComponent<ShopScrollButton>();
        }
    }
}
