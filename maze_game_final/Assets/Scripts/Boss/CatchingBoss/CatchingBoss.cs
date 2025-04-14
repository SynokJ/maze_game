namespace Boss.CatchingBoss
{
    using Enemy;
    using UnityEngine;

    public class CatchingBoss : AbstractBoss
    {
        protected const float HALF_OF_CHACE = 0.5f;

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

            spawnPos = CalculateSpawnPos();
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
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
            float posX = transform.position.x + Random.Range(minSpawnRadius, maxSpawnRadius);
            float posY = transform.position.y + Random.Range(minSpawnRadius, maxSpawnRadius);
            posX *= Random.value < HALF_OF_CHACE ? -1.0f : 1.0f;  
            posY *= Random.value < HALF_OF_CHACE ? -1.0f : 1.0f;  
            return new Vector2(posX, posY);
        }
    }
}
