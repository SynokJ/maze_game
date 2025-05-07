namespace UI.Utils
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    /// <summary>
    /// class of open scene button
    /// </summary>
    public class OpenSceneButton : AbstractButtonView
    {
        [SerializeField] protected Image blockImage = default;
        [SerializeField] protected string openSceneName = default;

        protected override void Awake()
        {
            base.Awake();
            blockImage.enabled = false;
        }

        protected override void OnButtonClicked()
        {
            blockImage.enabled = true;
            StartCoroutine(OpenSceneWithDelay());
        }

        protected virtual IEnumerator OpenSceneWithDelay()
        {
            yield return new WaitForSeconds(1.0f);

            AsyncOperation currentTask = SceneManager.LoadSceneAsync(openSceneName);
            while (!currentTask.isDone)
            {
                Debug.Log(currentTask.progress + "=>" + Time.time);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}