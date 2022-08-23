using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core
{
    public class GameCore : MonoBehaviour
    {
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
            Debug.Log("WIN");
        }

        public void LoseGame()
        {
            Debug.Log("LOSE");
        }
    }
}