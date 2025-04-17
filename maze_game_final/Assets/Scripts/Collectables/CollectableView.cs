namespace Collectable
{
    using Timers;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;
    using UnityEngine.UIElements;

    [RequireComponent(typeof(AbstractCollectable))]
    public class CollectableView : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer => collectableRenderer;

        [SerializeField] protected Transform arrowTr = default;
        [SerializeField, Min(0.0f)] protected float arrowDelay = 3.0f;
        [SerializeField] protected Light2D collectableLight = default;
        [SerializeField] protected SpriteRenderer arrowRenderer = default;
        [SerializeField] protected Collider2D collectableCollider = default;
        [SerializeField] protected SpriteRenderer collectableRenderer = default;
        [SerializeField] protected CollectablesDataSO collectableData = default;

        protected YieldInstruction arrowTimerInstruction = new WaitForSeconds(1.0f);
        protected CollectablesController collectableController = default;
        protected AbstractCollectable collectable = default;
        protected CustomTimer arrowTimer = default;

        protected virtual void Awake()
        {
            collectable = GetComponent<AbstractCollectable>();
            arrowTimer = new CustomTimer(this);
        }

        protected virtual void OnEnable()
        {
            collectable.OnCollectablePicked += HideCollectable;
            collectable.OnCollectablePicked += ShowDirectionArrow;

            arrowTimer.OnTimerFinished += HideDirectionArrow;
        }

        protected virtual void OnDisable()
        {
            collectable.OnCollectablePicked -= HideCollectable;
            collectable.OnCollectablePicked -= ShowDirectionArrow;

            arrowTimer.OnTimerFinished -= HideDirectionArrow;
        }

        protected virtual void HideCollectable()
        {
            collectableLight.enabled = false;
            collectableCollider.enabled = false;
            collectableRenderer.enabled = false;
        }

        protected virtual void ShowDirectionArrow()
        {
            if(collectableData.AvailableCollectable.Count <= 1)
            {
                return;
            }

            arrowRenderer.enabled = true;
            RotateTowardsBearestCollectable();
            arrowTimer.StartTimer(arrowTimerInstruction, arrowDelay);
        }

        protected virtual void HideDirectionArrow()
            => arrowRenderer.enabled = false;

        protected virtual void RotateTowardsBearestCollectable()
        {
            Vector2 nearestPos = Vector2.zero;
            float curDist = float.MaxValue;
            foreach (var collectable in collectableData.AvailableCollectable)
            {
                if (collectable.IsCollected || collectable.transform.position == transform.position)
                {
                    continue;
                }

                float tempDist = Vector2.Distance(collectable.transform.position, transform.position);
                if (tempDist < curDist)
                {
                    curDist = tempDist;
                    nearestPos = collectable.transform.position;
                }
            }

            arrowTr.rotation = Quaternion.Euler(CalculateRotation(nearestPos));
        }

        protected virtual Vector3 CalculateRotation(Vector2 targetPos)
        {
            Vector3 targ = targetPos;
            targ.z = 0f;

            Vector3 objectPos = transform.position;
            targ.x = targ.x - objectPos.x;
            targ.y = targ.y - objectPos.y;

            float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
            return new Vector3(0, 0, angle);
        }
    }
}
