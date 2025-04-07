namespace Enemy
{
    using System;
    using System.Linq;
    using UnityEngine;

    [RequireComponent(typeof(Enemy))]
    public class EnemyPatrol : MonoBehaviour
    {
        protected const float DISTANCE_TO_MOVE = 2.0f;
        protected readonly Vector2[] patrolDirection = new Vector2[]{
            Vector2.up, Vector2.down, Vector2.left, Vector2.right,
            new Vector2(0.5f, 0.5f), new Vector2(-0.5f, 0.5f),
            new Vector2(-0.5f, -0.5f), new Vector2(0.5f, -0.5f)
        };

        public event Action<bool> OnMovementDirectionChanged = delegate { };

        [SerializeField, Min(1.0f)] protected float movementSpeed = 1.0f;
        [SerializeField] protected Rigidbody2D playerRb = default;

        protected int patrolDirectionID = 0;
        protected Enemy enemyController = default;
        protected RaycastHit2D[] hits = null;
        protected Vector2[] tempAvailableDirection = null;
        protected Vector2 currentpatrolDirection = default;

        protected virtual void Awake()
        {
            enemyController = GetComponent<Enemy>();
            UpdatePatrolDirection();
        }

        protected virtual void OnEnable()
            => enemyController.OnEnemyPatroling += MoveEnemyByPatrolDirection;

        protected virtual void OnDisable()
            => enemyController.OnEnemyPatroling -= MoveEnemyByPatrolDirection;

        private void OnCollisionEnter2D(Collision2D collision)
            => UpdatePatrolDirection();

        protected virtual void MoveEnemyByPatrolDirection()
            => playerRb.MovePosition((Vector2)transform.position + currentpatrolDirection * movementSpeed * Time.deltaTime);

        protected virtual void UpdatePatrolDirection()
        {
            tempAvailableDirection = patrolDirection.Where(x => x != currentpatrolDirection).ToArray();
            CalculatedPatrolDirections();

            try 
            {
                patrolDirectionID = UnityEngine.Random.Range(0, tempAvailableDirection.Length);
                currentpatrolDirection = tempAvailableDirection[patrolDirectionID];
            } catch(Exception e)
            {
                currentpatrolDirection *= -1;
                Debug.Log(e.ToString());
            }
            OnMovementDirectionChanged(currentpatrolDirection.x >= 0);
        }

        protected virtual void CalculatedPatrolDirections()
        {
            Vector2[] result = new Vector2[tempAvailableDirection.Length];
            tempAvailableDirection.CopyTo(result, 0);

            for (int i = 0; i < result.Length; ++i)
                result[i] = IsAvailableDireciton(result[i]) ? result[i] : Vector2.zero;
            tempAvailableDirection = result.Where(dir => dir != Vector2.zero).ToArray();
        }

        protected virtual bool IsAvailableDireciton(Vector2 dir)
        {
            hits = Physics2D.RaycastAll(transform.position, dir, DISTANCE_TO_MOVE);
            foreach (RaycastHit2D hit in hits)
                if (!hit.transform.name.Equals(gameObject.name))
                    return false;
            return true;
        }
    }
}