namespace UI.Utils
{
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// class of open scene button
    /// </summary>
    public class OpenSceneButton : AbstractButtonView
    {
        [SerializeField] protected string openSceneName = default;

        protected override void OnButtonClicked()
            => SceneManager.LoadSceneAsync(openSceneName);
    }
}