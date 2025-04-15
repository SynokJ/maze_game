namespace Boss.CatchingBoss
{
    using UnityEngine;

    [RequireComponent(typeof(CatchingBoss))]
    public class CatchingBossEnemySpawner : AbstractGraveManagerProvider
    {
        protected CatchingBoss boss = default;
        protected bool canSpawn = false;

        protected virtual void Awake()
        {
            boss = GetComponent<CatchingBoss>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            boss.OnEnemySpawned += SpawnEnemy;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            boss.OnEnemySpawned -= SpawnEnemy;
        }

        protected override void InitPlayer()
        {
            base.InitPlayer();
            canSpawn = true;
        }

        protected override void DestroyPlayer()
            => canSpawn = false;

        protected virtual void SpawnEnemy()
        {
            if (!canSpawn) return;
            controller.ActivateGrave();
        }
    }
}
