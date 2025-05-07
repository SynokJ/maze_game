namespace Audio
{
    using System;
    using UnityEngine;

    [CreateAssetMenu(menuName = "MainMenu/" + nameof(SoundControllerContainer), fileName = nameof(SoundControllerContainer))]
    public class SoundControllerContainer : ScriptableObject
    {
        public event Action<SoundController> OnControllerInited = delegate { };

        public SoundController Controller
        {
            get => controller;
            set
            {
                if (value != null)
                {
                    controller = value;
                    OnControllerInited(controller);
                }
            }
        }

        public bool IsInited => controller != null;

        [SerializeField] protected SoundController controller = default;
    }
}
