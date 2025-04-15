namespace Boss.CatchingBoss
{
    using UnityEngine;

    [RequireComponent(typeof(Grave))]
    public class GraveView : MonoBehaviour
    {
        protected const string GRAVE_APPEARING_NAME = "on_inited";

        [SerializeField] protected Animator animator = default;

        protected Grave graveController = default;

        protected virtual void Awake()
        {
            graveController = GetComponent<Grave>();
        }

        protected virtual void OnEnable()
        {
            graveController.OnGraveInited += ShowGraveAppearing;
        }

        protected virtual void OnDisable()
        {
            graveController.OnGraveInited -= ShowGraveAppearing;
        }

        protected virtual void ShowGraveAppearing()
        {
            animator.SetTrigger(GRAVE_APPEARING_NAME);
        }
    }
}