namespace UI.Window
{
    using UnityEngine;

    /// <summary>
    /// sciptable object window 
    /// </summary>
    [CreateAssetMenu(menuName = "UI/Window/"+nameof(WindowSO), fileName = nameof(WindowSO))]
    public class WindowSO : ScriptableObject
    {
        [SerializeField] protected Canvas currentCanvas = default;

        /// <summary>
        /// method to open window
        /// </summary>
        public void OpenWindow()
            => Instantiate(currentCanvas);

        /// <summary>
        /// method to close window
        /// </summary>
        public void CloseWindow()
            => Destroy(currentCanvas.gameObject);
    }
}