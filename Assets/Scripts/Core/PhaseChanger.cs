using Game.Control;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Core
{
    public partial class PhaseChanger : MonoBehaviour
    {
        [SerializeField] GameObject planningButtons;
        [SerializeField] List<GameObject> allTurrets = new List<GameObject>();

        [SerializeField] GameObject fightCanvas;

        private Phase phase;

        // Start is called before the first frame update
        void Start()
        {
            phase = Phase.planning;
        }

        public void StartFight()
        {
            phase = Phase.fight;
            planningButtons.SetActive(false);
            fightCanvas.SetActive(true);
        }

        public void FinishGame()
        {
            phase = Phase.end;
        }

        public void AddTurretToList(GameObject turret) => allTurrets.Add(turret);

        public bool GetIsFight() => phase == Phase.fight;
    }
}