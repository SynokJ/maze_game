namespace UI.Utils
{
    using Params;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Text))]
    public class IntegerContainerView : MonoBehaviour
    {
        [SerializeField] protected IntegerContainer targetContainer = default;

        protected Text targetText = default;

        protected virtual void Awake()
        {
            targetText = GetComponent<Text>();
            UpdateTextView(targetContainer.Value);
        }

        protected virtual void OnEnable()
            => targetContainer.ValueChanged += UpdateTextView;

        protected virtual void OnDisable()
            => targetContainer.ValueChanged -= UpdateTextView;

        protected virtual void UpdateTextView(int val)
            => targetText.text = val.ToString();
    }
}
