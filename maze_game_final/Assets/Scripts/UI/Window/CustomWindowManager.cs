namespace UI.Window
{
    using UnityEngine;
    using System.Collections.Generic;

    /// <summary>
    /// class of custom window manager
    /// </summary>
    public class CustomWindowManager : MonoBehaviour
    {
        [SerializeField] protected WindowSO originWindow = default;
        [SerializeField] protected HashSet<WindowSO> availableWindows = new HashSet<WindowSO>();

        protected WindowSO currentWindow = default;

        protected virtual void Awake()
            => currentWindow = originWindow;

        /// <summary>
        /// method to open current window
        /// </summary>
        /// <param name="window"></param>
        public void OpenWindow(WindowSO window)
        {
            if (TryToCloseWindow())
            {
                currentWindow = window;
                currentWindow.OpenWindow();
            }
        }

        /// <summary>
        /// method to close current window
        /// </summary>
        public bool TryToCloseWindow()
        {
            if (currentWindow == null)
                return false;

            currentWindow.CloseWindow();
            return true;
        }

        /// <summary>
        /// method to close certain window
        /// </summary>
        /// <param name="window"></param>
        /// <returns></returns>
        public bool TryToCloseWindow(WindowSO window)
        {
            if(window == null)
                return false;

            window.CloseWindow();
            return true;
        }
    }
}