namespace Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(AbstractGameplayController))]
    public class GameplayControllerView : MonoBehaviour
    {
        [SerializeField] protected Text timerText = default;

        protected AbstractGameplayController controller = default;

        protected virtual void Awake()
            => controller = GetComponent<AbstractGameplayController>();

        protected virtual void OnEnable()
            => controller.OnTimerChanged += UpdateTimerView;

        protected virtual void OnDisable()
            => controller.OnTimerChanged -= UpdateTimerView;

        protected virtual void UpdateTimerView(TimeSpan timer)
            => timerText.text = timer.TotalSeconds.ToString();
    }
}
