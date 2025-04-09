namespace Gameplay
{
    using Collectable;
    using System;
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameplayController : MonoBehaviour
    {
        public event Action<TimeSpan> OnTimerChanged = delegate { };

        [SerializeField] protected string bossSceneName = default;
        [SerializeField] protected string loseSceneName = default;
        [SerializeField, Min(0.0f)] protected float timeSeconds = 0.0f;
        [SerializeField] protected CollectablesDataSO collectablesData = default;

        protected TimeSpan currentTimerTime = default;
        protected Coroutine currentCoroutine = default;
        protected TimeSpan currentTimerSteps = TimeSpan.FromSeconds(1.0f);

        protected virtual void Awake()
        {
            currentTimerTime = TimeSpan.FromSeconds(timeSeconds);
            OnTimerChanged(currentTimerTime);
            StartTimer();
        }

        protected virtual void OnEnable()
            => collectablesData.OnCollectablesCollected += LoadBossScene;

        protected virtual void OnDisable()
            => collectablesData.OnCollectablesCollected -= LoadBossScene;

        protected virtual void LoadBossScene()
            => SceneManager.LoadScene(bossSceneName);

        protected virtual void StartTimer()
            => currentCoroutine = StartCoroutine(StartCountDown());

        protected virtual void StopTimer()
            => StopCoroutine(currentCoroutine);

        protected IEnumerator StartCountDown()
        {
            yield return new WaitForSeconds(1.0f);
            currentTimerTime = currentTimerTime.Subtract(currentTimerSteps);
            OnTimerChanged(currentTimerTime);

            if (currentTimerTime.TotalSeconds > 0)
            {
                StartCoroutine(StartCountDown());
            } else
            {
                SceneManager.LoadScene(loseSceneName);
            }
        }
    }
}
