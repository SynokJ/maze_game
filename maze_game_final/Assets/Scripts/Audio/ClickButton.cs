namespace Audio
{

    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Button))]
    public class ClickButton : SoundControllerProvider
    {
        [SerializeField] protected AudioClip clickSound = default;
        protected Button button = default;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            button.onClick.AddListener(OnButtonClicked);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            button.onClick.RemoveListener(OnButtonClicked);
        }

        protected virtual void OnButtonClicked()
        {
            if (container.IsInited)
            {
                soundController.PlaySound(clickSound);
            }
        }
    }
}
