namespace Boss.ShootinBoss
{
    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    [RequireComponent(typeof(ShootingBossBullet))]
    public class ShootingBossBulletView : MonoBehaviour
    {
        [SerializeField] protected Light2D bulletLight = default;
        [SerializeField] protected Collider2D currentCollider = default;
        [SerializeField] protected SpriteRenderer currentRenderer = default;
        [SerializeField] protected ParticleSystem bulletFlyEffect = default;
        [SerializeField] protected SpriteRenderer currentShadowRenderer = default;

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
        {
            SetVisualByStatus(true);
            bulletFlyEffect.Play();
        }

        protected virtual void HideBullet()
        {
            SetVisualByStatus(false);
            bulletFlyEffect.Stop();
        }

        protected virtual void SetVisualByStatus(bool status)
        {
            bulletLight.enabled = status;
            currentRenderer.enabled = status;
            currentCollider.enabled = status;
            currentShadowRenderer.enabled = status;
        }
    }
}