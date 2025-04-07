namespace Params
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "Params/" + nameof(IntegerContainer), fileName = nameof(IntegerContainer))]
    public class IntegerContainer : ScriptableObject
    {
        public int DefaultValue => defaultValue;
        public int Value
        {
            get => currentValue;
            set
            {
                currentValue = Mathf.Abs(value);
                ValueChanged(currentValue);
            }
        }
        public event Action<int> ValueChanged = delegate { };

        [SerializeField, Min(0)] protected int currentValue = 0;
        [SerializeField, Min(0)] protected int defaultValue = 0;

        protected virtual void OnEnable()
            => ResetValue();

        public virtual void ResetValue()
            => Value = defaultValue;
    }
}
