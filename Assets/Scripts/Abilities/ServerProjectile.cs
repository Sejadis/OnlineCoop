using System;
using MLAPI;
using MLAPI.Spawning;
using SejDev.Systems.Ability;
using UnityEngine;

public class ServerProjectile : NetworkBehaviour
{
    public float Speed { get; set; }
    public float Range { get; set; }
    public Action<ulong> OnHit;
    private AbilityType[] hitEffects;

    private bool isInitialized = false;
    private float startTime;
    private float lifeTime;
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

//TODO rework to ability description
    public void Initialize(float speed, float range, AbilityType[] hitEffects, ulong actorId)
    {
        Speed = speed;
        Range = range;
        lifeTime = Range / Speed;
        startTime = Time.time;
        isInitialized = true;
        this.hitEffects = hitEffects;
        this.actorId = actorId;
    }

    private void OnTriggerEnter(Collider other)
    {
        // var entity =  other.GetComponent<NetworkObject>();
        // OnHit?.Invoke(entity.NetworkObjectId);
        //TODO refactor to decrease duplicated code (like server aoe zone)
//TODO catch self
        var netObject = other.GetComponent<NetworkCharacterState>();
        if (!IsServer || other.gameObject.name == "PlayerPrefab(Clone)" || netObject == null)
        {
            return;
        }

        var actor = NetworkSpawnManager.SpawnedObjects[actorId].GetComponent<NetworkCharacterState>();
        foreach (var hitEffect in hitEffects)
        {
            var runtimeParams = new AbilityRuntimeParams(hitEffect, actorId, netObject.NetworkObjectId, Vector3.zero,
                Vector3.zero, transform.position);

            actor.CastAbilityServerRpc(runtimeParams);
            // netObject.CastAbilityServerRpc(runtimeParams);
        }
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