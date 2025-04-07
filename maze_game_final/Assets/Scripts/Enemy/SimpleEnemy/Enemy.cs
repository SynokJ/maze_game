namespace Enemy
{
    using Player;
    using System;
    using UnityEngine;

    public class Enemy : AbstractPlayerProvider
    {
        public event Action OnEnemyPatroling = delegate { };

        [SerializeField, Min(0.0f)] protected float distanceToTrigger = 0.0f;
        [SerializeField, Min(1.0f)] protected float movementSpeed = 1.0f;
        [SerializeField] protected Rigidbody2D enemyRb = default;

        protected bool canMove = false;
        protected Transform playerTr = default;
        protected float distanceToPlayer = 0.0f;
        protected Vector2 movementDirection = default;

        protected virtual void Start()
        {
            if (playerContainer.isPlayerInited)
            {
                InitPlayer();
            }
        }

        protected override void InitPlayer()
        {
            base.InitPlayer();
            playerTr = controller.transform;
            canMove = true;
        }

        protected override void DestroyPlayer()
            => canMove = false;

        protected virtual void Update()
        {
            if (!canMove)
                return;

            if (IsEnemyOnTrigger())
            {
                movementDirection = (playerTr.position - transform.position).normalized;
                enemyRb.MovePosition((Vector2)transform.position + movementDirection * movementSpeed * Time.deltaTime);
            }
            else
            {
                OnEnemyPatroling();
            }
        }

        protected virtual bool IsEnemyOnTrigger()
        {
            distanceToPlayer = Vector2.Distance(transform.position, playerTr.position);
            return distanceToPlayer < distanceToTrigger;
        }
    }
}
