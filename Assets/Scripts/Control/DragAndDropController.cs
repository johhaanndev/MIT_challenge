using Game.Core;
using Game.Movement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class DragAndDropController : MonoBehaviour, IControllerBase
    {
        [SerializeField] List<GameObject> turretsPrefab;
        [SerializeField] List<GameObject> turretsPlanning;

        private int turretIndex;
        private Dragger dragger;

        // Start is called before the first frame update
        void Start()
        {
            InitializeReferences();   
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

        public void UndoBehaviour()
        {
            dragger.UndoLastTurret();
        }

        public void InitializeReferences()
        {
            dragger = GetComponent<Dragger>();
        }
    }
}