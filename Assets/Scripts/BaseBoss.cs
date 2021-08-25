using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MLAPI;
using SejDev.Systems.Ability;
using UnityEngine;

[RequireComponent(typeof(NetworkHealthState))]
//TODO serverchar that has abilityhandler
public class BaseBoss : NetworkBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int[] healthTriggers = {75, 50, 25};
    [SerializeField] private Transform[] shardPositions;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private GameObject shieldObject;
    private bool isImmuneToDamage;
    private NetworkHealthState netHealthState;
    private AbilityHandler abilityHandler;

    private int shardsAlive = 0;
    private List<int> activeTriggers;

    // Start is called before the first frame update
    public override void NetworkStart()
    {
        shieldObject.SetActive(false);
        if (!IsServer)
        {
            enabled = false;
            return;
        }

        netHealthState = GetComponent<NetworkHealthState>();
        netHealthState.MaxHealth.Value = maxHealth;
        netHealthState.CurrentHealth.Value = maxHealth;

        abilityHandler = new AbilityHandler(netHealthState);
        netHealthState.OnAbilityCast += OnAbilityCast;
        activeTriggers = new List<int>(healthTriggers);
        activeTriggers = activeTriggers.OrderByDescending(i => i).ToList();
    }

    private void OnAbilityCast(AbilityRuntimeParams runtimeParams)
    {
        abilityHandler.StartAbility(ref runtimeParams);
    }

    // Update is called once per frame
    void Update()
    {
        abilityHandler.Update();
    }

    public void Damage(int amount)
    {
        if (shardsAlive > 0)
        {
            return;
        }

        netHealthState.CurrentHealth.Value -= amount;
        if (activeTriggers.Count > 0 && netHealthState.CurrentHealth.Value <= activeTriggers[0])
        {
            activeTriggers.RemoveAt(0);
            shieldObject.SetActive(true);
            foreach (var shardPos in shardPositions)
            {
                shardsAlive++;
                var shard = Instantiate(shardPrefab, shardPos.position, Quaternion.identity);
                shard.GetComponent<ServerDestructibleObject>().OnDeath += OnShardDeath;
                shard.GetComponent<NetworkObject>().Spawn();
            }
        }
    }

    private void OnShardDeath(NetworkObject netObj)
    {
        netObj.Despawn(true);
        shardsAlive--;
        if (shardsAlive == 0)
        {
            shieldObject.SetActive(false);
        }
    }
}