using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameJamStarterKit
{
    public class CommonButtonActions : MonoBehaviour
    {
        public virtual void LoadScene(string scene)
        {
            var async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Single);
            async.allowSceneActivation = true;
        }

        public virtual void LoadSceneAdditive(string scene)
        {
            var async = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            async.allowSceneActivation = true;
        }

        public virtual void UnloadScene(string scene)
        {
            SceneManager.UnloadSceneAsync(scene, UnloadSceneOptions.None);
        }

        public virtual void QuitApplication()
        {
            Application.Quit();
        }

        public virtual void OpenURL(string url)
        {
            Application.OpenURL(url);
        }
    }
}