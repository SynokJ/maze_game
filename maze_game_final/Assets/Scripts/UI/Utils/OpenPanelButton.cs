namespace UI.Utils
{
    using UnityEngine;
    using UI.Window;

    /// <summary>
    /// class of open panel button
    /// </summary>
    public class OpenPanelButton : AbstractButtonView
    {
        [SerializeField] protected WindowSO windowToOpen = default;
        [SerializeField] protected CustomWindowManager customWindowManager = default;

        protected override void OnButtonClicked()
            => customWindowManager.OpenWindow(windowToOpen);
    }
}