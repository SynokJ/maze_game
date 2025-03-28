namespace UI.Utils
{
    using UnityEngine;
    using UnityEngine.UI;

    /// <summary>
    /// abstract class of action button
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class AbstractButtonView : MonoBehaviour
    {
        protected Button currentButton = default;

        protected virtual void Awake()
            => currentButton = GetComponent<Button>();

        protected virtual void OnEnable()
            => currentButton.onClick.AddListener(OnButtonClicked);
       
        protected virtual void OnDisable()
            => currentButton.onClick.RemoveListener(OnButtonClicked);

        /// <summary>
        /// abstract method of button click action 
        /// </summary>
        protected abstract void OnButtonClicked();
    }
}
