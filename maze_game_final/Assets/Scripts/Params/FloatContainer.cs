namespace Params
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Params/"+nameof(FloatContainer), fileName = nameof(FloatContainer))]
    public class FloatContainer : ScriptableObject
    {
        public float DefaultValue => defaultValue;
        public float Value {
            get => currentValue;
            set {
                currentValue = Mathf.Abs(value);
                ValueChanged(currentValue);
            }
        }
        public event Action<float> ValueChanged = delegate { };

        [SerializeField, Min(0.0f)] protected float currentValue = 0.0f;
        [SerializeField, Min(0.0f)] protected float defaultValue = 0.0f;

        protected virtual void OnEnable()
            => ResetValue();

        public virtual void ResetValue()
            => Value = defaultValue;
    }
}