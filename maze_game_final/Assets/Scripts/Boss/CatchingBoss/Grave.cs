namespace Boss.CatchingBoss
{
    using System;
    using UnityEngine;

    public class Grave : MonoBehaviour
    {
        public event Action OnGraveInited = delegate { };
        public event Action OnEnemySpawned = delegate { };

        [SerializeField] protected GameObject enemyPref = default;

        public virtual void InitGrave()
            => OnGraveInited();

        public virtual void SpawnEnemy()
        {
            Instantiate(enemyPref, transform.position, Quaternion.identity);
            OnEnemySpawned();
        }
    }
}
