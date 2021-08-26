using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MLAPI;
using MLAPI.SceneManagement;
using SejDev.Systems.Ability;
using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(NetworkCharacterState))]
//TODO serverchar that has abilityhandler
public class BaseBoss : NetworkBehaviour, IDamagable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int[] healthTriggers = {75, 50, 25};
    [SerializeField] private Transform[] shardPositions;
    [SerializeField] private GameObject shardPrefab;
    [SerializeField] private GameObject shieldObject;
    private bool isImmuneToDamage;
    private NetworkCharacterState networkCharacterState;
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

        networkCharacterState = GetComponent<NetworkCharacterState>();
        networkCharacterState.NetHealthState.MaxHealth.Value = maxHealth;
        networkCharacterState.NetHealthState.CurrentHealth.Value = maxHealth;

        abilityHandler = new AbilityHandler(networkCharacterState);
        networkCharacterState.OnServerAbilityCast += OnAbilityCast;
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

        networkCharacterState.NetHealthState.CurrentHealth.Value -= amount;
        if (activeTriggers.Count > 0 && networkCharacterState.NetHealthState.CurrentHealth.Value <= activeTriggers[0])
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

        if (networkCharacterState.NetHealthState.CurrentHealth.Value <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        NetworkSceneManager.SwitchScene("SampleScene");
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