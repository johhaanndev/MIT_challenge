using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
    public class NexusController : MonoBehaviour
    {
        [SerializeField] ParticleSystem explosionParticles;
        [SerializeField] ParticleSystem fireParticles;

        private GameCore gameCore;
        private Health health;

        private void Start()
        {
            gameCore = GameObject.Find("GameCore").GetComponent<GameCore>();
            health = GetComponent<Health>();
        }

        public void NexusDestroyed()
        {
            //var explosion = Instantiate(explosionParticles, transform.position, Quaternion.identity, null);
            //var fire = Instantiate(fireParticles, transform.position, Quaternion.identity, null);

            gameCore.LoseGame();
        }
    }
}