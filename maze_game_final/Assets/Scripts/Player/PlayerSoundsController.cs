namespace Player
{
    using UnityEngine;
    using Audio;


    public class PlayerSoundsController : SoundControllerProvider
    {
        [SerializeField] protected AudioClip footstepsAudio = default;
        [SerializeField] protected PlayerMovement movement = default;

        protected override void OnEnable()
        {
            base.OnEnable();
            movement.OnPlayerMoved += PlayFootstepSound;
            movement.OnPlayerStopped += StopFootstepSound;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            movement.OnPlayerMoved -= PlayFootstepSound;
            movement.OnPlayerStopped -= StopFootstepSound;
        }

        protected virtual void PlayFootstepSound(Vector2 dir)
            => soundController.PlaySoundEffectNonRepeat(footstepsAudio);

        protected virtual void StopFootstepSound()
            => soundController.StopSoundEffect();
    }
}
