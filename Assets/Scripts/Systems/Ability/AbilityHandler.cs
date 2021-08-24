using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SejDev.Systems.Ability
{
    public class AbilityHandler
    {
        private List<Ability> blockingAbilities = new List<Ability>();

        private List<Ability> nonBlockingAbilities = new List<Ability>();

        private Dictionary<AbilityType, float> abilityCooldowns = new Dictionary<AbilityType, float>();
        private NetworkState networkState;

        public AbilityHandler(NetworkState networkState)
        {
            this.networkState = networkState;
        }
        // Start is called before the first frame update

        // Update is called once per frame
        public void Update()
        {
            if (blockingAbilities.Count > 0)
            {
                var ability = blockingAbilities[0];
                if (!ability.IsBlocking())
                {
                    nonBlockingAbilities.Add(ability);
                    AdvanceAbilityQueue();
                }
                else
                {
                    if (!UpdateAbility(blockingAbilities[0]))
                    {
                        AdvanceAbilityQueue(true);
                    }
                }
            }

            for (int i = nonBlockingAbilities.Count - 1; i >= 0; i--)
            {
                if (!UpdateAbility(nonBlockingAbilities[i]))
                {
                    nonBlockingAbilities[i].End();
                    nonBlockingAbilities.RemoveAt(i);
                }
            }
        }

        private bool UpdateAbility(Ability ability)
        {
            var isActive = ability.Update();
            var isExpired = ability.StartTime + ability.Description.duration < Time.time;
            return isActive && !isExpired;
        }

        private void AdvanceAbilityQueue(bool shouldEnd = false)
        {
            if (shouldEnd)
            {
                blockingAbilities[0].End();
            }

            blockingAbilities.RemoveAt(0);

            RunAbility();
        }

        public void StartAbility(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.Instance.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var descr))
            {
                var ability = Ability.CreateAbility(ref runtimeParams);
                blockingAbilities.Add(ability);
                if (blockingAbilities.Count == 1)
                {
                    RunAbility();
                }
            }
        }

        private void RunAbility()
        {
            if (blockingAbilities.Count > 0)
            {
                var ability = blockingAbilities[0];
                var descr = ability.Description;
                var canUse = descr.cooldown == 0f //ability has no cooldown
                             || !abilityCooldowns.TryGetValue(descr.abilityType,
                                 out var lastUseTime) //ability has cooldown but we havent used it yet
                             || Time.time - descr.cooldown >
                             lastUseTime; //ability has cooldown and was used before but cooldown is expired
                if (canUse)
                {
                    ability.StartTime = Time.time;
                    if (ability.Description.cooldown > 0)
                    {
                        abilityCooldowns[ability.Description.abilityType] =
                            Time.time; //TODO possibly dont set cooldown when ability cancels out of start
                        networkState.StartCooldownClientRpc(ability.Description.abilityType,
                            ability.Description.cooldown);
                    }

                    if (!ability.Start())
                    {
                        AdvanceAbilityQueue();
                        return;
                    }

                    if (!ability.IsBlocking())
                    {
                        nonBlockingAbilities.Add(ability);
                        AdvanceAbilityQueue();
                    }
                }
                else
                {
                    //still on cooldown, go to next ability
                    AdvanceAbilityQueue();
                }
            }
        }
    }
}