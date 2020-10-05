using UnityEngine.SceneManagement;

namespace GameJamStarterKit
{
    public static class KItSceneManager
    {
        public static void ReloadCurrentScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}