﻿using System;
using MLAPI;
using SejDev.Systems.Ability;
using UnityEngine;

namespace Abilities
{
    public class ServerAoeZone :NetworkBehaviour
    {
        private float expireTime;
        private AbilityType[] hitEffects;
        private ulong actorId;

        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }
        }

        public void Initialize(float duration, float range, AbilityType[] hitEffects, ulong actorId)
        {
            expireTime = Time.time + duration;
            transform.localScale *= range / 2;
            this.hitEffects = hitEffects;
            this.actorId = actorId;
        }
        

        private void OnTriggerEnter(Collider other)
        {
            if (!IsServer)
            {
                return;
            }
            foreach (var hitEffect in hitEffects)
            {
                var netState = other.GetComponent<NetworkState>();
                var runtimeParams = new AbilityRuntimeParams(hitEffect, actorId, netState.NetworkObjectId, Vector3.zero,
                    Vector3.zero, transform.position);
                netState.CastAbilityServerRpc(runtimeParams);
            }
        }
    }
}