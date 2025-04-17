namespace Boss.CatchingBoss
{
    using UnityEngine;

    [RequireComponent(typeof(CatchingBoss))]
    public class CatchingBossView : MonoBehaviour
    {
        protected const string ATTACK_ANIMATION_NAME = "on_attack";

        [SerializeField] protected Animator animator = default;

        protected CatchingBoss controller = default;

        protected virtual void Awake()
            => controller = GetComponent<CatchingBoss>();

        protected virtual void OnEnable()
            => controller.OnEnemySpawned += AnimateAttack;

        protected virtual void OnDisable()
            => controller.OnEnemySpawned -= AnimateAttack;

        protected virtual void AnimateAttack()
            => animator.SetTrigger(ATTACK_ANIMATION_NAME);
    }
}
