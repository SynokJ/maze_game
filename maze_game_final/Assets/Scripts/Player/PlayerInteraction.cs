namespace Player
{
    using Boss;
    using Enemy;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public class PlayerInteraction : MonoBehaviour
    {
        [SerializeField] protected string sceneName = default;

        protected Enemy tempEnemy = default;
        protected AbstractBoss tempBoss = default;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out tempEnemy))
            {
                SceneManager.LoadScene(sceneName);
            }
            else if (collision.gameObject.TryGetComponent(out tempBoss))
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
