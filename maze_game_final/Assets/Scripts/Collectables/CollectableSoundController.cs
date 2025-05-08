namespace Colelctable
{
    using Audio;
    using Collectable;
    using UnityEngine;

    [RequireComponent(typeof(AbstractCollectable))]
    public class CollectableSoundController : SoundControllerProvider
    {
        [SerializeField] protected AudioClip collectClip = default;

        protected AbstractCollectable collectable = default;

        protected virtual void Awake()
        {
            collectable = GetComponent<AbstractCollectable>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            collectable.OnCollectablePicked += PlayPickSound;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            collectable.OnCollectablePicked -= PlayPickSound;
        }

        protected virtual void PlayPickSound()
        {
            if (container.IsInited)
            {
                soundController.PlaySoundEffect(collectClip);
            }
        }
    }
}
