namespace Boss.ShootinBoss
{
    using UnityEngine;
    using UnityEngine.Pool;

    public class ShootingBoss : AbstractBoss
    {
        [SerializeField, Min(0)] protected int bulletAmount = 0;
        [SerializeField] protected Transform bulletSpawnPoint = default;
        [SerializeField] protected ShootingBossBullet bulletPref = default;

        protected Vector2 shootDirection = default;
        protected ShootingBossBullet tempBullet = default;
        protected ObjectPool<ShootingBossBullet> bulletPool = null;

        protected override void Awake()
        {
            base.Awake();
            bulletPool = new ObjectPool<ShootingBossBullet>(GenerateBullet, null, null, null, true, bulletAmount);
        }

        protected override void Attack()
        {
            Debug.Log("Attacked => " + Time.time);

            shootDirection = (playerTr.position - transform.position).normalized;
            tempBullet = bulletPool.Get();
            Vector3 targ = playerTr.transform.position;
            targ.z = 0f;

            Vector3 objectPos = tempBullet.transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            tempBullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            tempBullet.transform.localScale = new Vector2(tempBullet.transform.localScale.x, shootDirection.x < 0 ? -1.0f : 1.0f);
            tempBullet.Shoot(shootDirection);
            RestartAttack();
        }

        protected override void PrepareAttack(float preparePercent)
        {
            Debug.Log(currentTimerValue.ToString());
        }

        protected virtual ShootingBossBullet GenerateBullet()
        {
            tempBullet = Instantiate(bulletPref, bulletSpawnPoint.position, Quaternion.identity);
            tempBullet.InitBullet(bulletPool);
            return tempBullet;
        }
    }
}