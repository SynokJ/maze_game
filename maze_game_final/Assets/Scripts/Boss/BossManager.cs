namespace Boss
{
    using System.Collections.Generic;
    using UnityEngine;

    public class BossManager : MonoBehaviour
    {
        [SerializeField] protected List<AbstractBoss> bosses = new List<AbstractBoss>();
        [SerializeField] protected Transform spawnPos = default;

        protected AbstractBoss tempBoss = default;

        protected virtual void Awake()
        {
            tempBoss = bosses[2];//Random.Range(0, bosses.Count)];
            Instantiate(tempBoss, spawnPos.position, Quaternion.identity);
        }
    }
}
