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

        public virtual void PlaySound(AudioClip clickSound)
            => audioSource.PlayOneShot(clickSound);
    }
}
