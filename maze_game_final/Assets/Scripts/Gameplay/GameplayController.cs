namespace Gameplay
{
    using Collectable;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class GameplayController : MonoBehaviour
    {
        [SerializeField] protected string sceneName = default;
        [SerializeField] protected CollectablesDataSO collectablesData = default;

        protected virtual void OnEnable()
        {
            collectablesData.OnCollectablesCollected += CheckForWin;
        }

        protected virtual void OnDisable()
        {
            collectablesData.OnCollectablesCollected -= CheckForWin;
        }

        protected virtual void CheckForWin()
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
