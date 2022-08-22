using Game.Control;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Core
{
    public class PhaseChanger : MonoBehaviour
    {
        [SerializeField] List<GameObject> allTurrets = new List<GameObject>();
        [SerializeField] List<GameObject> allEnemies = new List<GameObject>();

        private bool isFight = false;

        // Start is called before the first frame update
        void Start()
        {
            allEnemies = GameObject.FindGameObjectsWithTag("Enemy").ToList();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartFight()
        {
            isFight = true;

            foreach (var turret in allTurrets)
            {
                turret.GetComponent<TurretController>().enabled = true;
            }

            foreach (var enemy in allEnemies)
            {
                enemy.GetComponent<AIController>().enabled = true;
            }
        }

        public void AddTurretToList(GameObject turret) => allTurrets.Add(turret);

        public bool GetIsFight() => isFight;
    }
}