namespace Enemy
{
    using UnityEngine;

    [RequireComponent(typeof(Enemy))]
    public class EnemyView : MonoBehaviour
    {
        protected Enemy enemy = default;

        protected virtual void Awake()
            => enemy = GetComponent<Enemy>();

        protected virtual void OnEnable()
            => enemy.OnEnemyMoved += ScaleEnemyByDirStatus;

        protected virtual void OnDisable()
            => enemy.OnEnemyMoved -= ScaleEnemyByDirStatus;

        protected virtual void ScaleEnemyByDirStatus(bool isRightMove)
            => transform.localScale = new Vector3(isRightMove ? 1 : -1, transform.localScale.y, transform.localScale.z);
    }
}
