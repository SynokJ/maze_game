namespace Player
{
    using UnityEngine;

    [RequireComponent(typeof(PlayerContoller))]
    public class PlayerView : MonoBehaviour
    {
        protected const string WALK_ANIMATION_NAME = "is_walk";
        protected readonly Vector3 LEFT_WALK_SCALE = new Vector3(-1.0f, 1.0f, 1.0f); 
        protected readonly Vector3 RIGHT_WALK_SCALE = new Vector3(1.0f, 1.0f, 1.0f); 

        [SerializeField] protected Animator animator = default;
        [SerializeField] protected Transform playerTr = default;

        protected PlayerContoller controller = default;

        protected virtual void Awake()
            => controller = GetComponent<PlayerContoller>();

        protected virtual void OnEnable()
        {
            controller.OnPlayerMoved += UpdatePlayerMovement;
            controller.OnPlayerStopped += ResetPlayerMovement;
        }

        protected virtual void OnDisable()
        {
            controller.OnPlayerMoved -= UpdatePlayerMovement;
            controller.OnPlayerStopped -= ResetPlayerMovement;
        }

        protected virtual void UpdatePlayerMovement(Vector2 movementDirection)
        {
            animator.SetBool(WALK_ANIMATION_NAME, true);
            playerTr.localScale = movementDirection.x > 0 ? RIGHT_WALK_SCALE : LEFT_WALK_SCALE;
        }

        protected virtual void ResetPlayerMovement()
        {
            animator.SetBool(WALK_ANIMATION_NAME, false);
        }
    }
}
