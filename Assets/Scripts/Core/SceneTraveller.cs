using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Core
{
    public class SceneTraveller : MonoBehaviour
    {
        public void MoveToScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}