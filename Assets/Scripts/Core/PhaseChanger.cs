using Game.Control;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public class PhaseChanger : MonoBehaviour
    {
        [SerializeField] GameObject planningButtons;
        [SerializeField] List<GameObject> allTurrets = new List<GameObject>();
        [SerializeField] List<GameObject> allEnemies = new List<GameObject>();

        [SerializeField] GameObject fightCanvas;

        private enum Phase
        {
            planning,
            fight,
            end
        }

        private Phase phase;

        // Start is called before the first frame update
        void Start()
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        public void StartFight()
        {
            phase = Phase.fight;
            planningButtons.SetActive(false);
            fightCanvas.SetActive(true);
        }

        public void FinishGame()
        {

        }

        public void AddTurretToList(GameObject turret) => allTurrets.Add(turret);

        public bool GetIsFight() => phase == Phase.fight;
    }
}