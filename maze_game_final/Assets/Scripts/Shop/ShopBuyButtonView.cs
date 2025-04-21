namespace Shop
{
    using UnityEngine;

    [RequireComponent(typeof(ShopBuyButton))]
    public class ShopBuyButtonView : MonoBehaviour
    {
        [SerializeField] protected ShopDataContainer shopDataContainer = default;

        protected ShopBuyButton shopBuyButton = default;

        protected virtual void Awake()
        {
            shopBuyButton = GetComponent<ShopBuyButton>();
        }
    }
}