using System;
using MLAPI;
using UnityEngine;

    public class ServerProjectile : NetworkBehaviour
    {
        public float Speed { get; set; }
        public float Range { get; set; }
        public Action<ulong> OnHit;

        private bool isInitialized = false;
        private float startTime;
        private float lifeTime;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }
        }

        public void Initialize(float speed, float range)
        {
            Speed = speed;
            Range = range;
            lifeTime = Range / Speed;
            startTime = Time.time;
            isInitialized = true;
            
        }
        private void OnTriggerEnter(Collider other)
        {
            var entity =  other.GetComponent<NetworkObject>();
            OnHit?.Invoke(entity.NetworkObjectId);
        }

        private void Update()
        {
            if (!isInitialized)
            {
                return;
            }

            if (Time.time > startTime + lifeTime)
            {
                GetComponent<NetworkObject>().Despawn(true);
            }

            var transf = transform;
            var move = transf.forward * (Time.deltaTime * Speed);
            transf.position += move;
        }
    }
