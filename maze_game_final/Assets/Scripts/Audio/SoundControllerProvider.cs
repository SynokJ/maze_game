namespace Audio
{
    using UnityEngine;

    public class SoundControllerProvider : MonoBehaviour
    {
        [SerializeField] protected SoundControllerContainer container = default;

        protected SoundController soundController = default;

        protected virtual void OnEnable()
            => container.OnControllerInited += InitSoundController;

        protected virtual void OnDisable()
            => container.OnControllerInited -= InitSoundController;

        protected virtual void InitSoundController(SoundController controller)
            => soundController = controller;
    }
}