namespace Boss
{
    using System.Collections;
    using UnityEngine;

    public class MovingBoss : AbstractBoss
    {
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
            StartCoroutine(ChargeMoveToDestination());
        }

        protected override void PrepareAttack()
        {
        }

        protected IEnumerator ChargeMoveToDestination()
        {
            for (float i = 0; i <= 1.0f; i += 0.01f)
            {
                transform.position = Vector3.Lerp(transform.position, tempTargetPos, i);
                yield return new WaitForEndOfFrame();
            }
            RestartAttack();
        }

        protected override void RestartAttack()
        {
            base.RestartAttack();
            tempTargetPos = playerTr.position;
        }
    }
}