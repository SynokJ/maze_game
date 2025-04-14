namespace Boss.ShootinBoss
{
    using UnityEngine;

    [RequireComponent(typeof(ShootingBossBullet))]
    public class ShootingBossBulletView : MonoBehaviour
    {
        [SerializeField] protected SpriteRenderer currentRenderer = default;
        [SerializeField] protected Collider2D currentCollider = default;

        protected ShootingBossBullet bulletController = default;

        protected virtual void Awake()
        {
            bulletController = GetComponent<ShootingBossBullet>();
        }

        protected virtual void OnEnable()
        {
            bulletController.OnBulletActivated += ShowBullet;
            bulletController.OnBulletInactivated += HideBullet;
        }

        protected virtual void OnDisable()
        {
            bulletController.OnBulletActivated -= ShowBullet;
            bulletController.OnBulletInactivated -= HideBullet;
        }

        protected virtual void ShowBullet()
            => SetVisualByStatus(true);

        protected virtual void HideBullet()
            => SetVisualByStatus(false);

        protected virtual void SetVisualByStatus(bool status)
        {
            currentRenderer.enabled = status;
            currentCollider.enabled = status;
        }
    }
}