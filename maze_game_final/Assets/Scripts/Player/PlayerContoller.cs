namespace Player
{
    using System;
    using Joystick;
    using UnityEngine;

    /// <summary>
    /// class of player controller
    /// </summary>
    public class PlayerContoller : MonoBehaviour
    {
        public event Action OnPlayerStopped = delegate { };
        public event Action<Vector2> OnPlayerMoved = delegate { };

        [SerializeField] protected Joystick joystick = default;
        [SerializeField] protected Rigidbody2D playerRb = default;
        [SerializeField, Min(1.0f)] protected float movementSpeed = 1.0f;

        protected Vector2 movementStep = default;

        protected virtual void OnEnable()
        {
            joystick.OnJoystickChanged += MovePlayer;
            joystick.OnJoystickDeactivated += StopPlayer;
        }

        protected virtual void OnDisable()
        {
            joystick.OnJoystickChanged -= MovePlayer;
            joystick.OnJoystickDeactivated -= StopPlayer;
        }

        protected virtual void MovePlayer(Vector2 movementDir)
        {
            movementStep = movementDir * movementSpeed * Time.deltaTime;
            playerRb.MovePosition(playerRb.position + movementStep);
            OnPlayerMoved(movementDir);
        }

        protected virtual void StopPlayer()
        {
            OnPlayerStopped();
        }
    }
}
