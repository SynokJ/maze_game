namespace Boss.CatchingBoss
{
    using Enemy;
    using System;
    using UnityEngine;

    public class CatchingBoss : AbstractBoss
    {
        protected const float HALF_OF_CHACE = 0.5f;

        public event Action OnEnemySpawned = delegate { };

        [SerializeField] protected float minSpawnRadius = 0.0f;
        [SerializeField] protected float maxSpawnRadius = 0.0f;
        [SerializeField] protected Enemy enemyPrefab = default;
        [SerializeField] protected Rigidbody2D bossRb = default;
        [SerializeField, Min(0.0f)] protected float movementSpeed = 0.0f;

        protected Vector2 movementDirection = default;
        protected Vector2 spawnPos = default;
        protected bool canMove = false;

        protected override void Attack()
        {
            canMove = false;
            OnEnemySpawned();
            RestartAttack();
        }

        protected override void PrepareAttack(float preparePercent)
        {
            canMove = true;
        }

        protected virtual void Update()
        {
            if (canMove && canAttack)
            {
                movementDirection = (playerTr.position - transform.position).normalized;
                bossRb.MovePosition((Vector2)transform.position + movementDirection * movementSpeed);
            }
        }

        protected Vector2 CalculateSpawnPos()
        {
            float posX = transform.position.x + UnityEngine.Random.Range(minSpawnRadius, maxSpawnRadius);
            float posY = transform.position.y + UnityEngine.Random.Range(minSpawnRadius, maxSpawnRadius);
            posX *= UnityEngine.Random.value < HALF_OF_CHACE ? -1.0f : 1.0f;  
            posY *= UnityEngine.Random.value < HALF_OF_CHACE ? -1.0f : 1.0f;  
            return new Vector2(posX, posY);
        }
    }
}
