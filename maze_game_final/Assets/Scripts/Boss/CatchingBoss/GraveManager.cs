namespace Boss.CatchingBoss
{
    using Player;
    using System.Collections.Generic;
    using UnityEngine;

    public class GraveManager : AbstractPlayerProvider
    {
        [SerializeField] protected List<Grave> graves = new List<Grave>();

        protected bool canActivateGreave = false;
        protected Grave tempGrave = default;

        public virtual void ActivateGrave()
        {
            tempGrave = GetNearestGrave();
            tempGrave.SpawnEnemy();
        }

        protected override void InitPlayer()
        {
            base.InitPlayer();
            canActivateGreave = true;
            graves.ForEach(grave => { grave.InitGrave(); });
        }

        protected override void DestroyPlayer()
            => canActivateGreave = false;

        protected virtual Grave GetNearestGrave()
        {
            tempGrave = graves[0];
            foreach (Grave grave in graves)
            {
                float dist1 = Vector2.Distance(tempGrave.transform.position, controller.transform.position);
                float dist2 = Vector2.Distance(grave.transform.position, controller.transform.position);

                if(dist1 > dist2)
                {
                    tempGrave = grave;
                }
            }
            return tempGrave;
        }
    }
}