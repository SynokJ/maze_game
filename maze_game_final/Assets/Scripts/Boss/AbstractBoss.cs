namespace Boss
{
    using Player;
    using System;
    using UnityEngine;
    using System.Collections;

    public abstract class AbstractBoss : AbstractPlayerProvider
    {
        protected const float TIMER_STEP_VALUE = 1.0f;

        [SerializeField, Min(1.0f)] protected float secondsToAttack = 0.0f;

        protected Transform playerTr = default;
        protected TimeSpan timeToSubstract = default;
        protected TimeSpan currentTimerValue = default;
        protected bool canAttack = false;

        protected virtual void Awake()
        {
            currentTimerValue = TimeSpan.FromSeconds(secondsToAttack);
            timeToSubstract = TimeSpan.FromSeconds(TIMER_STEP_VALUE);
        }

        protected override void InitPlayer()
        {
            base.InitPlayer();
            canAttack = true;
            playerTr = controller.transform;
            StartCoroutine(UpdateStageTimer());
        }

        protected override void DestroyPlayer()
            => canAttack = false;

        protected abstract void PrepareAttack(float preparePercent);

        protected abstract void Attack();

        protected IEnumerator UpdateStageTimer()
        {
            yield return new WaitForSeconds(TIMER_STEP_VALUE);
            currentTimerValue = currentTimerValue.Subtract(timeToSubstract);

            if (currentTimerValue >= TimeSpan.Zero)
            {
                PrepareAttack((secondsToAttack - (float)currentTimerValue.TotalSeconds) / secondsToAttack);
                StartCoroutine(UpdateStageTimer());
            }
            else if (canAttack)
            {
                Attack();
            }
        }

        protected virtual void RestartAttack()
        {
            currentTimerValue = TimeSpan.FromSeconds(secondsToAttack);
            StartCoroutine(UpdateStageTimer());
        }
    }
}
