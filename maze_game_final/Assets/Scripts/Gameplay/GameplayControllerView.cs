namespace Gameplay
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(GameplayController))]
    public class GameplayControllerView : MonoBehaviour
    {
        [SerializeField] protected Text timerText = default;

        protected GameplayController controller = default;

        protected virtual void Awake()
            => controller = GetComponent<GameplayController>();

        protected virtual void OnEnable()
            => controller.OnTimerChanged += UpdateTimerView;

        protected virtual void OnDisable()
            => controller.OnTimerChanged -= UpdateTimerView;

        protected virtual void UpdateTimerView(TimeSpan timer)
            => timerText.text = timer.TotalSeconds.ToString();
    }
}
