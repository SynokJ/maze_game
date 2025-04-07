namespace Enemy
{
    using UnityEngine;

    [RequireComponent(typeof(EnemyPatrol))]
    public class EnemyPatrolView : MonoBehaviour
    {
        protected EnemyPatrol enemy = default;

        protected virtual void Awake()
           => enemy = GetComponent<EnemyPatrol>();

        protected virtual void OnEnable()
            => enemy.OnMovementDirectionChanged += ScaleEnemyByDirStatus;

        protected virtual void OnDisable()
            => enemy.OnMovementDirectionChanged -= ScaleEnemyByDirStatus;

        protected virtual void ScaleEnemyByDirStatus(bool isRightMove)
            => transform.localScale = new Vector3(isRightMove ? 1 : -1, transform.localScale.y, transform.localScale.z);
    }
}