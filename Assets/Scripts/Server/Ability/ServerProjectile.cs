using MLAPI;
using UnityEngine;

namespace Server.Ability
{
    public class ServerProjectile : NetworkBehaviour
    {
        private Ability ability;
        private bool isInitialized = false;
        private float speed;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }
        }

        public void Initialize(Ability ability)
        {
            this.ability = ability;
            speed = ability.Description.speed;
            isInitialized = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer)
            {
                return;
            }

            ability.RunHitEffects(other, Vector3.zero, transform.forward,transform.position, transform);
        }


        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            var move = transform.forward * (Time.deltaTime * speed);
            transform.position += move;
        }
    }
}