namespace Joystick
{
    using Player;
    using System;
    using UnityEngine;

    /// <summary>
    /// class of joystick system
    /// </summary>
    public class Joystick : AbstractPlayerProvider
    {
        public Vector2 OriginPosition => originPosition;
        public Vector2 CurrentPosition => firstTouch.position;
        public event Action OnJoystickDeactivated = delegate { };
        public event Action<Vector2> OnJoystickActivated = delegate { };
        public event Action<Vector2> OnJoystickChanged = delegate { };

        protected bool isProceeding = false;
        protected Touch firstTouch = default;
        protected Vector2 movementDirection = default;
        protected Vector2 originPosition = default;

        protected override  void InitPlayer()
        {
            base.InitPlayer();
            controller.SubscribeJoystick(this);
        }

        protected override void DestroyPlayer()
            => controller.UnsubscribeJoystick(this);

        protected virtual void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                isProceeding = true;
                originPosition = Input.mousePosition;
                OnJoystickActivated(Input.mousePosition);
            }

            if(Input.GetMouseButtonUp(0))
            {
                isProceeding = false;
                OnJoystickDeactivated();
            }

            if (isProceeding)
            {
                movementDirection = ((Vector2)Input.mousePosition - originPosition).normalized;
                OnJoystickChanged(movementDirection);
            }
        }
    }
}