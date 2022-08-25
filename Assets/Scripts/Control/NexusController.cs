using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class NexusController : MonoBehaviour, IControllerBase
    {
        [SerializeField] ParticleSystem fireParticles;

        private GameCore gameCore;

        private void Start()
        {
            InitializeReferences();
        }

        public void NexusDestroyed(ParticleSystem explosion)
        {
            var particles = Instantiate(explosion, transform.position, Quaternion.identity, null);
            var fire = Instantiate(fireParticles, transform.position, Quaternion.identity, null);

            gameCore.LoseGame();
        }

        public void InitializeReferences()
        {
            gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
        }
    }
}