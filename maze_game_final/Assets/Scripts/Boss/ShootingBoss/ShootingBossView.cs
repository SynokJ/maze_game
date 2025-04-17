namespace Boss.ShootingBoss
{
    using UnityEngine;

    [RequireComponent(typeof(ShootingBoss))]
    public class ShootingBossView : MonoBehaviour
    {
        protected string SHOOT_ANIMATION_NAME = "on_shoot";

        [SerializeField] protected Animator animator = default;

        protected ShootingBoss controller = default;

        protected virtual void Awake()
        {
            controller = GetComponent<ShootingBoss>();
        }

        protected virtual void OnEnable()
        {
            controller.OnAttackProceeded += AnimateShoot;
        }

        protected virtual void OnDisable()
        {
            controller.OnAttackProceeded -= AnimateShoot;
        }

        protected virtual void AnimateShoot()
        {
            animator.SetTrigger(SHOOT_ANIMATION_NAME);
        }
    }
}
