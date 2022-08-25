using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Core
{
    public class GameCore : MonoBehaviour, IGameCore
    {
        [SerializeField] GameObject endGameCanvas;
        [SerializeField] Text resultText;
        [SerializeField] PhaseChanger phaseChanger;

        private List<GameObject> enemies = new List<GameObject>();

        // Start is called before the first frame update
        void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            enemies = GameObject.FindGameObjectsWithTag(GameTags.ENEMY).ToList();
            phaseChanger = GameObject.Find("PhaseChanger").GetComponent<PhaseChanger>();
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
            phaseChanger.FinishGame();
        }

        public void LoseGame()
        {
            endGameCanvas.SetActive(true);
            resultText.text = "FAILURE";
            phaseChanger.FinishGame();
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}