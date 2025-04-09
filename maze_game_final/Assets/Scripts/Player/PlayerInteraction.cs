namespace Player
{
    using Enemy;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] protected string sceneName = default;

        protected Enemy tempEnemy = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out tempEnemy))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
