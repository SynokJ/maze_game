namespace Camera
{
    using Player;
    using UnityEngine;

    public class FollowingCamera : AbstractPlayerProvider
    {
        [SerializeField, Min(0.5f)] protected float movementSpeed = 0.5f;

        protected bool canFollow = false;
        protected float movementStep = 0.0f;
        protected Vector3 offsetPos = default;
        protected Vector3 targetPos = default;
        protected Transform playerTr = default;

        protected override void InitPlayer()
        {
            base.InitPlayer();
            canFollow = true;
            playerTr = controller.transform;
            offsetPos = playerTr.position - transform.position;
        }

        protected override void DestroyPlayer()
            => canFollow = false;

        protected virtual void Update()
        {
            if (canFollow)
            {
                targetPos = playerTr.position - offsetPos;
                movementStep = movementSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);
            }
        }
    }
}
