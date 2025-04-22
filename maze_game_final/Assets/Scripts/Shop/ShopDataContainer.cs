namespace Shop
{
    using Player;
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(ShopDataContainer), menuName = "Shop/" + nameof(ShopDataContainer))]
    public class ShopDataContainer : ScriptableObject
    {
        public event Action<PlayerMovement> OnPlayerChanged = delegate { };
        public PlayerMovement CurrentPlayerController => currentPlayerController;
        public IReadOnlyList<PlayerMovement> PlayerControllers => playerControllers;
        public int CurrentId
        {
            get => currentId;
            set
            {
                if (value > playerControllers.Count - 1)
                {
                    value = 0;
                }
                else if (value < 0)
                {
                    value = playerControllers.Count - 1;
                }

                currentId = value;
                currentPlayerController = playerControllers[currentId];
                OnPlayerChanged(playerControllers[currentId]);
            }
        }

        [SerializeField] protected List<PlayerMovement> playerControllers = new List<PlayerMovement>();

        protected int currentId = 0;
        protected PlayerMovement currentPlayerController = default;

        protected virtual void OnEnable()
        {
            if (playerControllers.Count > 0)
            {
                currentPlayerController = playerControllers[currentId];
            }
        }

        public virtual void SwitchToNextPlayer()
            => CurrentId++;

        public virtual void SwitchToPreviousPlayer()
            => CurrentId--;
    }
}
