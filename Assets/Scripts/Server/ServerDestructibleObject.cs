using System;
using MLAPI;
using Shared;
using UnityEngine;

namespace Server
{
    public class ServerDestructibleObject : NetworkBehaviour, IDamagable
    {
        [SerializeField] private int maxHealth = 10;
        [SerializeField] private NetworkHealthState networkHealthState;

        public Action<NetworkObject> OnDeath;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }

            networkHealthState.MaxHealth.Value = maxHealth;
            networkHealthState.CurrentHealth.Value = maxHealth;
        }

        public void Damage(int amount)
        {
            networkHealthState.CurrentHealth.Value -= amount;
            if (networkHealthState.CurrentHealth.Value <= 0)
            {
                OnDeath?.Invoke(NetworkObject);
            }
        }
    }
}
