namespace Boss.MovingBoss
{
    using UnityEngine;

    [RequireComponent(typeof(MovingBoss))]
    public class MovingBossView : MonoBehaviour
    {
        protected const string MOVEMENT_ANIMATION_NAME = "on_right";

        [SerializeField] protected Animator animator = default;

        protected MovingBoss controller = default;

        protected virtual void Awake()
            => controller = GetComponent<MovingBoss>();

        protected virtual void OnEnable()
            => controller.OnMoving += AnimateMovementByState;

        protected virtual void OnDisable()
            => controller.OnMoving -= AnimateMovementByState;

        protected virtual void AnimateMovementByState(bool isRightState)
            => animator.SetBool(MOVEMENT_ANIMATION_NAME, isRightState);
    }
}
