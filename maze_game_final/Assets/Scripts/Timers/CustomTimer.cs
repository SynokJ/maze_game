namespace Timers
{
    using System;
    using UnityEngine;
    using System.Collections;

    public class CustomTimer
    {
        public event Action OnTimerFinished = delegate { };
        public event Action<TimeSpan> OnTimerChanged = delegate { };

        protected YieldInstruction instruction = default;
        protected MonoBehaviour targetComponent = default;
        protected TimeSpan currentTimerTime = default;
        protected TimeSpan currentTimerStep = default;

        public CustomTimer(MonoBehaviour target)
        {
            targetComponent = target;
        }

        ~CustomTimer()
        {
            targetComponent = null;
            instruction = null;
        }

        public virtual void StartTimer(YieldInstruction instructions, float seconds)
        {
            this.instruction = instructions;
            currentTimerTime = TimeSpan.FromSeconds(seconds);
            currentTimerStep = TimeSpan.FromSeconds(1.0f);
            targetComponent.StartCoroutine(UpdateTimer());
        }

        protected virtual IEnumerator UpdateTimer()
        {
            OnTimerChanged(currentTimerTime);
            yield return instruction;
            currentTimerTime = currentTimerTime.Subtract(currentTimerStep);

            if (currentTimerTime >= TimeSpan.Zero)
            {
                targetComponent.StartCoroutine(UpdateTimer());
            }
            else
            {
                OnTimerFinished();
            }
        }
    }
}