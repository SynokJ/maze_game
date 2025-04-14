namespace Boss.ShootinBoss
{
    using UnityEngine;
    using UnityEngine.Pool;

    public class ShootingBoss : AbstractBoss
    {
        [SerializeField, Min(5)] protected int bulletAmount = 5;
        [SerializeField] protected Rigidbody2D bossRb = default;
        [SerializeField] protected Transform bulletSpawnPoint = default;
        [SerializeField, Min(0.0f)] protected float movementSpeed = 0.0f;
        [SerializeField] protected ShootingBossBullet bulletPref = default;

        protected bool canMove = false;
        protected Vector2 shootDirection = default;
        protected ShootingBossBullet tempBullet = default;
        protected IObjectPool<ShootingBossBullet> bulletPool = null;

        protected override void Awake()
        {
            base.Awake();
            bulletPool = new ObjectPool<ShootingBossBullet>(
                GenerateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPoolObject,
                true, bulletAmount);
        }

        protected virtual void Update()
        {
            //if (canMove && canAttack)
            //{
            //    shootDirection = (playerTr.position - transform.position).normalized;
            //    transform.localScale = new Vector2(shootDirection.x < 0 ? 1.0f : -1.0f, transform.localScale.y);
            //    bossRb.MovePosition((Vector2)transform.position + shootDirection * movementSpeed);
            //}
        }

        protected override void Attack()
        {
            canMove = false;
            shootDirection = (playerTr.position - transform.position).normalized;
            tempBullet = bulletPool.Get();
            SetupBulletTransform();
            RestartAttack();
        }

        protected override void PrepareAttack(float preparePercent)
            => canMove = true;

        protected virtual ShootingBossBullet GenerateBullet()
        {
            ShootingBossBullet result = Instantiate(bulletPref);
            result.InitBullet(bulletPool);
            return result;
        }

        protected virtual void OnGetFromPool(ShootingBossBullet bullet)
            => bullet.transform.position = bulletSpawnPoint.position;

        protected virtual void OnReleaseToPool(ShootingBossBullet bullet)
        {
            // TODO
        }

        protected virtual void OnDestroyPoolObject(ShootingBossBullet bullet)
            => Destroy(bullet.gameObject);

        protected virtual void SetupBulletTransform()
        {
            Vector3 targ = playerTr.transform.position;
            targ.z = 0f;

            Vector3 objectPos = tempBullet.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            tempBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            tempBullet.transform.localScale = new Vector2(tempBullet.transform.localScale.x, shootDirection.x < 0 ? -1.0f : 1.0f);
            tempBullet.Shoot(shootDirection);
        }
    }
}