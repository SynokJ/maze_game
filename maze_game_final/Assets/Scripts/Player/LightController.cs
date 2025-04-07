namespace Player
{
    using Params;
    using UnityEngine;
    using UnityEngine.Rendering.Universal;

    [RequireComponent(typeof(Light2D))]
    public class LightController : MonoBehaviour
    {
        [SerializeField] protected FloatContainer lightOuterRadius = default;

        protected Light2D targetLight = default;

        protected virtual void Awake()
        {
            targetLight = GetComponent<Light2D>();
            SetOuterRadius(lightOuterRadius.DefaultValue);
        }

        protected virtual void OnEnable()
            => lightOuterRadius.ValueChanged += SetOuterRadius;

        protected virtual void OnDisable()
            => lightOuterRadius.ValueChanged -= SetOuterRadius;

        protected virtual void SetOuterRadius(float val)
            => targetLight.pointLightOuterRadius = val;
    }
}
