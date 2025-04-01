namespace Joystick
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Joystick))]
    public class JoystickView : MonoBehaviour
    {
        [SerializeField] protected Image innerCircle = default;
        [SerializeField] protected Image outterCircle = default;

        protected float distance = 0.0f;
        protected float joystickRadius = 0.0f;
        protected Joystick joystick = default;
        protected Vector2 movementPosition = default;

        protected virtual void Awake()
        {
            joystick = GetComponent<Joystick>();
            float innerRadius = innerCircle.rectTransform.sizeDelta.y * 0.25f;
            float outterRadius = outterCircle.rectTransform.sizeDelta.y * 0.5f;
            joystickRadius = outterRadius + innerRadius;
            HideJoystick();
        }

        protected virtual void OnEnable()
        {
            joystick.OnJoystickActivated += ShowJoystick;
            joystick.OnJoystickDeactivated += HideJoystick;
            joystick.OnJoystickChanged += UpdateJoystickView;
        }

        protected virtual void OnDisable()
        {
            joystick.OnJoystickActivated -= ShowJoystick;
            joystick.OnJoystickDeactivated -= HideJoystick;
            joystick.OnJoystickChanged -= UpdateJoystickView;
        }

        protected virtual void ShowJoystick(Vector2 position)
        {
            SetViewByStatus(true);
            outterCircle.rectTransform.position = position;
            innerCircle.rectTransform.position = position;
        }

        protected virtual void HideJoystick()
            => SetViewByStatus(false);

        protected virtual void SetViewByStatus(bool status)
        {
            innerCircle.enabled = status;
            outterCircle.enabled = status;
        }

        protected virtual void UpdateJoystickView(Vector2 movementDirection)
        {
            movementPosition = joystick.CurrentPosition;
            distance = Vector2.Distance(joystick.OriginPosition, movementPosition);

            innerCircle.rectTransform.position = distance < joystickRadius ?
                movementPosition : joystick.OriginPosition + movementDirection * joystickRadius;
        }
    }
}
