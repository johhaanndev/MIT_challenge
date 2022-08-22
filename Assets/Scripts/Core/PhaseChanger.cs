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

        public void StartFight()
        {
            isFight = true;
        }

        public void AddTurretToList(GameObject turret) => allTurrets.Add(turret);

        public bool GetIsFight() => isFight;
    }
}