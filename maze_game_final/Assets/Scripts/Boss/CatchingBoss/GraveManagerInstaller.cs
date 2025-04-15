namespace Boss.CatchingBoss
{
    using UnityEngine;

    public class GraveManagerInstaller : MonoBehaviour
    {
        [SerializeField] protected GameObject graveManagerPref = default;
        [SerializeField] protected GraveManagerContainer graveManagerContainer = default;

        protected GameObject tempGraveManager = default;

        protected virtual void Start()
        {
            ResetPlayer();
            tempGraveManager = graveManagerPref;
            graveManagerContainer.InitGraveManager(tempGraveManager);
        }

        protected virtual void ResetPlayer()
        {
            if (tempGraveManager != null)
            {
                graveManagerContainer.DestroyGraveManager();
            }
        }
    }
}
