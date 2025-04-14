namespace Boss.ShootinBoss
{
    using System;
    using UnityEngine;
    using UnityEngine.Pool;

    public class ShootingBossBullet : MonoBehaviour
    {
        public event Action OnBulletActivated = delegate { };
        public event Action OnBulletInactivated = delegate { };

        [SerializeField, Min(0.0f)] protected float bulletSpeed = 0.0f;
        [SerializeField] protected Rigidbody2D bulletRb = default;

        protected IObjectPool<ShootingBossBullet> pool = default;
        protected AbstractBoss playerInteraction = default;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.TryGetComponent(out playerInteraction))
            {
                pool.Release(this);
                OnBulletInactivated();
            }
        }

        public virtual void InitBullet(IObjectPool<ShootingBossBullet> data)
        {
            OnBulletInactivated();
            pool = data;
        }

        public virtual void Shoot(Vector2 direction)
        {
            bulletRb.velocity = Vector2.zero;
            bulletRb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);
            OnBulletActivated();
        }
    }
}
