using System.Collections.Generic;
using System.Linq;
using MLAPI;
using MLAPI.SceneManagement;
using MLAPI.Spawning;
using Server.Character;
using Shared;
using Shared.Abilities;
using UnityEngine;

namespace Server
{
    [RequireComponent(typeof(NetworkCharacterState))]
    public class BaseBoss : ServerCharacter
    {
        [SerializeField] private int maxHealth = 100;
        [SerializeField] private int[] healthTriggers = {75, 50, 25};
        [SerializeField] private Transform[] shardPositions;
        [SerializeField] private GameObject shardPrefab;
        [SerializeField] private GameObject shieldObject;
        [SerializeField] private NetworkObject[] energyWalls;
        [SerializeField] private AbilityType wallCollisionAbility;
        private bool isImmuneToDamage;

        private int shardsAlive = 0;
        private List<int> activeTriggers;

        // Start is called before the first frame update
        public override void NetworkStart()
        {
            base.NetworkStart();
            shieldObject.SetActive(false);

            networkCharacterState.NetHealthState.MaxHealth.Value = maxHealth;
            networkCharacterState.NetHealthState.CurrentHealth.Value = maxHealth;

            activeTriggers = new List<int>(healthTriggers);
            activeTriggers = activeTriggers.OrderByDescending(i => i).ToList();
        }

        public override void Damage(ulong actor, int amount)
        {
            if (shardsAlive > 0)
            {
                return;
            }

            networkCharacterState.NetHealthState.CurrentHealth.Value -= amount;
            if (activeTriggers.Count > 0 &&
                networkCharacterState.NetHealthState.CurrentHealth.Value <= activeTriggers[0])
            {
                activeTriggers.RemoveAt(0);
                shieldObject.SetActive(true);
                foreach (var shardPos in shardPositions)
                {
                    shardsAlive++;
                    var shard = Instantiate(shardPrefab, shardPos.position, Quaternion.identity);
                    shard.GetComponent<ServerCharacter>().OnDeath += OnShardDeath;
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

        private void OnShardDeath(ulong objId, ulong source)
        {
            NetworkSpawnManager.SpawnedObjects[objId].Despawn(true);
            shardsAlive--;
            if (shardsAlive == 0)
            {
                shieldObject.SetActive(false);
                //TODO why am i doing this check?
                if (NetworkSpawnManager.SpawnedObjects.TryGetValue(source, out var playerCollider))
                {
                    var targets = new ulong[energyWalls.Length + 1];
                    var i = 0;
                    targets[i++] = source;
                    foreach (var energyWall in energyWalls)
                    {
                        targets[i++] = energyWall.NetworkObjectId;
                    }

                    var runtimeParams = new AbilityRuntimeParams(wallCollisionAbility, NetworkObjectId, targets,
                        Vector3.zero, Vector3.zero, Vector3.zero);
                    NetworkCharacterState.CastAbilityServerRpc(runtimeParams);
                }
            }
        }
    }
}