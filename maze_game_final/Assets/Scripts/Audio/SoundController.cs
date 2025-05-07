namespace Audio
{
    using UnityEngine;

    [RequireComponent(typeof(AudioSource))]
    public class SoundController : MonoBehaviour
    {
        [SerializeField] protected SoundControllerContainer container = default;

        protected AudioSource audioSource = default;

        protected virtual void Awake()
            => audioSource = GetComponent<AudioSource>();

        protected virtual void Start()
           => container.Controller = this;

        public virtual void PlaySoundEffect(AudioClip clip)
            => audioSource.PlayOneShot(clip);

        public virtual void PlaySoundEffectNonRepeat(AudioClip clip)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(clip);
            }
        }

        public virtual void StopSoundEffect()
        {
            audioSource.Stop();
        }
    }
}
