namespace UI.Utils
{
    using UnityEngine;
    using System.Collections;
    using UnityEngine.SceneManagement;

    /// <summary>
    /// class of open scene button
    /// </summary>
    public class OpenSceneButton : AbstractButtonView
    {
        [SerializeField] protected string openSceneName = default;

        protected override void OnButtonClicked()
        {
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