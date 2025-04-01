namespace Camera
{
    using UnityEngine;

    public class FollowingCamera : MonoBehaviour
    {
        [SerializeField] protected Transform playerTr = default;
        [SerializeField, Min(0.5f)] protected float movementSpeed = 0.5f;

        protected float movementStep = 0.0f;
        protected Vector3 offsetPos = default;
        protected Vector3 targetPos = default;

        protected virtual void Awake()
            => offsetPos = playerTr.position - transform.position;

        protected virtual void Update()
        {
            targetPos = playerTr.position - offsetPos;
            movementStep = movementSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPos, movementSpeed);
        }
    }
}
