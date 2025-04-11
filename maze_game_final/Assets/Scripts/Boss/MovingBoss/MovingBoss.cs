namespace Boss
{
    using UnityEngine;
    using System.Collections;

    public class MovingBoss : AbstractBoss
    {
        [SerializeField] protected ParticleSystem slideEffect = default;
        [SerializeField, Range(0.0f, 1.0f)] protected float chanceToHit = 0.0f;

        protected Vector3 tempTargetPos = default;

        protected override void InitPlayer()
        {
            base.InitPlayer();
            tempTargetPos = playerTr.position;
        }

        protected override void DestroyPlayer()
        {
            canAttack = false;
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
            for (float i = 0; i <= 1.0f; i += 0.01f)
            { 
                transform.position = Vector3.Lerp(transform.position, tempTargetPos, i);
                yield return new WaitForSeconds(0.01f);
            }
            slideEffect.Stop();
            RestartAttack();
        }
    }
}