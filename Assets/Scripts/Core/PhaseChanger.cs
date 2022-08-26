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
        [Header("Objects reference")]
        [SerializeField] List<GameObject> allTurrets = new List<GameObject>();

        [Header("UI")]
        [SerializeField] GameObject planningButtons;
        [SerializeField] GameObject fightCanvas;

        [Header("Soundtrack")]
        [SerializeField] AudioSource planningMusic;
        [SerializeField] AudioSource fightMusic;

        private Phase phase;

        // Start is called before the first frame update
        void Start()
        {
            phase = Phase.Planning;
        }

        public void StartFight()
        {
            phase = Phase.Fight;
            planningMusic.Stop();
            fightMusic.Play();
            planningButtons.SetActive(false);
            fightCanvas.SetActive(true);
        }

        public void FinishGame()
        {
            fightMusic.Stop();
            planningMusic.Play();
            phase = Phase.End;
        }

        public void AddTurretToList(GameObject turret) => allTurrets.Add(turret);

        public bool GetIsFight() => phase == Phase.Fight;
    }
}