namespace Gameplay
{
    using Collectable;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameplayController : AbstractGameplayController
    {
        [SerializeField] protected CollectablesDataSO collectablesData = default;

        protected virtual void OnEnable()
            => collectablesData.OnCollectablesCollected += LoadBossScene;

        protected virtual void OnDisable()
            => collectablesData.OnCollectablesCollected -= LoadBossScene;

        protected virtual void LoadBossScene()
            => SceneManager.LoadScene(winSceneName);
    }
}
