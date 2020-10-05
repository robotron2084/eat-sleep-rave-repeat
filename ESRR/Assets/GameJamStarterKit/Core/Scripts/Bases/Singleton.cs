using UnityEngine.SceneManagement;

namespace GameJamStarterKit
{
    public class Singleton<T> where T : new()
    {
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }

        private static T _instance;

        protected Singleton()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
        }

        private static void SceneManagerOnActiveSceneChanged(Scene currentScene, Scene nextScene)
        {
            SceneManager.activeSceneChanged -= SceneManagerOnActiveSceneChanged;
            _instance = new T();
        }
    }
}