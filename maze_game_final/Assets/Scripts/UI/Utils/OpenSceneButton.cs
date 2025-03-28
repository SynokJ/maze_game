namespace UI.Utils
{
    using UnityEditor;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// class of open scene button
    /// </summary>
    public class OpenSceneButton : AbstractButtonView
    {
        [SerializeField] protected SceneAsset openScene = default;

        protected override void OnButtonClicked()
            => SceneManager.LoadSceneAsync(openScene.name);
    }
}