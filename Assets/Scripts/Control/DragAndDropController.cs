using Game.Core;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class DragAndDropController : MonoBehaviour
    {
        [SerializeField] PhaseChanger phaseChanger;
        [SerializeField] List<GameObject> turretsPrefab;
        [SerializeField] List<GameObject> turretsPlanning;

        private int turretIndex;
        private Dragger dragger;

        // Start is called before the first frame update
        void Start()
        {
            dragger = GetComponent<Dragger>();
        }

        // Update is called once per frame
        void Update()
        {
            if (phaseChanger.GetIsFight())
                return;
        }

        public void DragBehaviour(int index)
        {
            turretIndex = index;
            dragger.Drag(turretsPlanning[turretIndex]);
        }

        public void DropBehaviour()
        {
            dragger.Drop(turretsPlanning[turretIndex], turretsPrefab[turretIndex]);
        }
    }
}