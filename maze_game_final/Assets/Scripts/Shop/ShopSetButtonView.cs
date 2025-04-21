namespace Shop
{
    using UnityEngine;

    [RequireComponent(typeof(ShopSetButton))]
    public class ShopSetButtonView : MonoBehaviour
    {
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected ShopSetButton shopSetButton = default;

        protected virtual void Awake()
        {
            shopSetButton = GetComponent<ShopSetButton>();
        }
    }
}