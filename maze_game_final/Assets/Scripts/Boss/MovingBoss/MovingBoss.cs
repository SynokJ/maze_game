namespace Boss.MovingBoss
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class MovingBoss : AbstractBoss
    {
        public event Action<bool> OnMoving = delegate { };

        [SerializeField] protected ParticleSystem slideEffect = default;
        [SerializeField, Range(0.0f, 1.0f)] protected float chanceToHit = 0.0f;

        protected Vector3 tempTargetPos = default;
        protected Vector2 movementDir = default;

        protected virtual void Update()
        {
            movementDir = (playerTr.position - transform.position).normalized;
            OnMoving(movementDir.x >= 0);
        }

        protected override void Attack()
        {
            tempTargetPos = playerTr.position;
            StartCoroutine(ChargeMoveToDestination());
        }

        protected override void PrepareAttack(float preparePercent)
        {
            if(preparePercent > 0.5f)
            {
                slideEffect.Play();
            }
        }

        protected IEnumerator ChargeMoveToDestination()
        {
            for (float i = 0; i <= 1.0f; i += 0.0025f)
            { 
                transform.position = Vector3.Lerp(transform.position, tempTargetPos, i);
                yield return new WaitForSeconds(0.005f);
            }
            slideEffect.Stop();
            RestartAttack();
        }
    }
}