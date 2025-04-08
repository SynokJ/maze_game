namespace Collectable
{
    using UnityEngine;
    using UnityEngine.UI;

    public class CollectableUI : MonoBehaviour
    {
        [SerializeField] protected Image iconUI = default;

        public virtual void InitCollectableUI(Sprite sprite)
            => iconUI.sprite = sprite;
    }
}
