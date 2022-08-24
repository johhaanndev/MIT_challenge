using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class GameCore : MonoBehaviour
    {
        [SerializeField] GameObject endGameCanvas;
        [SerializeField] Text resultText;

        private List<GameObject> enemies = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            enemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        public void RemoveDeadEnemy(GameObject enemy)
        {
            enemies.Remove(enemy);
            if (enemies.Count == 0)
            {
                WinGame();
            }
        }

        public void WinGame()
        {
            endGameCanvas.SetActive(true);
            resultText.text = "MISSION COMPLETE";
        }

        public void LoseGame()
        {
            endGameCanvas.SetActive(true);
            resultText.text = "FAILURE";
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}