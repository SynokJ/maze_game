namespace Surroundings
{
    using System;
    using Timers;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;
    using UnityEngine.SocialPlatforms;

    [RequireComponent(typeof(Light2D))]
    public class BossLights : MonoBehaviour
    {

        [SerializeField, Range(0.0f, 1.0f)] protected float chanceToDestroy = 0.0f;
        [SerializeField, Min(0.0f)] protected float secondsToCheck = 0.0f;

        protected CustomTimer timer = default;
        protected Light2D targetLight = default;
        protected YieldInstruction instruction = new WaitForSeconds(1.0f);

        protected virtual void Awake()
        {
            targetLight = GetComponent<Light2D>();

            timer = new CustomTimer(this);
            timer.StartTimer(instruction, secondsToCheck);
        }

        protected virtual void OnEnable()
        {
            timer.OnTimerFinished += UpdateLightState;
            timer.OnTimerChanged += CheckTime;
        }

        protected virtual void OnDisable()
        {
            timer.OnTimerFinished -= UpdateLightState;
            timer.OnTimerChanged -= CheckTime;
        }

        protected virtual void UpdateLightState()
        {
            targetLight.enabled = UnityEngine.Random.value <= chanceToDestroy;
            timer.StartTimer(instruction, secondsToCheck);
        }

        protected virtual void CheckTime(TimeSpan timerVal)
            => Debug.Log(timerVal.ToString());
    }
}
