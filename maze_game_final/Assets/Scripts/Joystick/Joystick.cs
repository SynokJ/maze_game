namespace Joystick
{
    using System;
    using UnityEngine;
    using UnityEngine.EventSystems;

    /// <summary>
    /// class of joystick system
    /// </summary>
    public class Joystick : MonoBehaviour
    {
        public Vector2 OriginPosition => originPosition;
        public Vector2 CurrentPosition => firstTouch.position;
        public event Action OnJoystickDeactivated = delegate { };
        public event Action<Vector2> OnJoystickActivated = delegate { };
        public event Action<Vector2> OnJoystickChanged = delegate { };
        
        protected Touch firstTouch = default;
        protected Vector2 movementDirection = default;
        protected Vector2 originPosition = default;

        protected virtual void Update()
        {
            if (IsTouchAccepted())
            {
                firstTouch = Input.GetTouch(0);
                switch (firstTouch.phase)
                {
                    case TouchPhase.Began:
                        originPosition = firstTouch.position;
                        OnJoystickActivated(firstTouch.position);
                        break;
                    case TouchPhase.Ended:
                        OnJoystickDeactivated();
                        break;
                    case TouchPhase.Moved:
                        movementDirection = (firstTouch.position - originPosition).normalized;
                        OnJoystickChanged(movementDirection);
                        break;
                }
            }
        }

        protected virtual bool IsTouchAccepted()
        {
            bool isTouchExists = Input.touchCount > 0;
            bool isTouchOverUI = EventSystem.current.IsPointerOverGameObject();
            return isTouchExists && !isTouchOverUI;
        }
    }
}