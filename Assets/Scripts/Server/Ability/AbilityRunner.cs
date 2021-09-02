using System;
using System.Collections.Generic;
using Runnable;
using Server.Character;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Server.Ability
{
    public class AbilityRunner : Runner<AbilityBase, AbilityRuntimeParams>
    {
        private readonly List<AbilityBase> blockingAbilities = new List<AbilityBase>();

        private readonly Dictionary<AbilityType, float> abilityCooldowns = new Dictionary<AbilityType, float>();
        private readonly ServerCharacter serverCharacter;

        public AbilityRunner(ServerCharacter serverCharacter)
        {
            this.serverCharacter = serverCharacter;
        }
        // Start is called before the first frame update

        // Update is called once per frame
        public override void Update()
        {
            if (blockingAbilities.Count > 0)
            {
                var ability = blockingAbilities[0];
                if (!ability.IsBlocking())
                {
                    CurrentRunnables.Add(ability);
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

            for (int i = CurrentRunnables.Count - 1; i >= 0; i--)
            {
                if (!UpdateAbility(CurrentRunnables[i]))
                {
                    CurrentRunnables[i].End();
                    TryStartCooldown(CurrentRunnables[0]);

                    CurrentRunnables.RemoveAt(i);
                }
            }
        }

        private bool UpdateAbility(AbilityBase ability)
        {
            var isActive = ability.Update();
            var canExpire = ability.Description.duration > 0;
            var isExpired = canExpire && ability.StartTime + ability.Description.duration < Time.time;
            return isActive && !isExpired;
        }

        private void AdvanceAbilityQueue(bool shouldEnd = false)
        {
            if (shouldEnd)
            {
                blockingAbilities[0].End();
                TryStartCooldown(blockingAbilities[0]);
            }

            blockingAbilities.RemoveAt(0);

            RunAbility();
        }

        public override void AddRunnable(ref AbilityRuntimeParams runtimeParams)
        {
            if (GameDataManager.TryGetAbilityDescriptionByType(runtimeParams.AbilityType, out var description))
            {
                //dont go for reactivation if we are currently in a hitEffect instead of normal ability
                if (description.isUnique)
                {
                    //try to find this ability in queue
                    AbilityBase match = null;
                    if (blockingAbilities.Count > 0 &&
                        blockingAbilities[0].Description.abilityType == description.abilityType)
                    {
                        match = blockingAbilities[0];
                    }
                    
                    if (match == null)
                    {
                        foreach (var nonBlockingAbility in CurrentRunnables)
                        {
                            if (nonBlockingAbility.Description.abilityType == description.abilityType)
                            {
                                match = nonBlockingAbility;
                                break;
                            }
                        }
                    }

                    if (match != null)
                    {
                        if (match is Ability matchedAbility)
                        {
                            //we found a match, reactivate it
                            var shouldEnd = !matchedAbility.Reactivate();
                            TryStartCooldown(match);

                            if (shouldEnd)
                            {
                                //reactivate returned false, end the ability
                                match.End();
                                TryStartCooldown(match);
                                CurrentRunnables.Remove(match);
                            }
                        }
                        else
                        {
                            //TODO refactor to some other, nicer way potentially separating hiteffects more from normal abilities?
                            throw new ArgumentException("something wonky here, investigate");
                        }

                        return;
                    }
                    //ability not found, create a new one normally
                }

                var ability = GetRunnable(ref runtimeParams);
                blockingAbilities.Add(ability);
                if (blockingAbilities.Count == 1)
                {
                    RunAbility();
                }
            }
        }

        protected override AbilityBase GetRunnable(ref AbilityRuntimeParams runtimeParams)
        {
            return Ability.CreateAbility(ref runtimeParams);
        }

        private void RunAbility()
        {
            if (blockingAbilities.Count > 0)
            {
                var ability = blockingAbilities[0];
                var description = ability.Description;
                var canUse =
                    description.cooldown == 0f //ability has no cooldown
                    || !abilityCooldowns.TryGetValue(description.abilityType,
                        out var lastUseTime) //ability has cooldown but we havent used it yet
                    || Time.time - description.cooldown >
                    lastUseTime; //ability has cooldown and was used before but cooldown is expired
                if (canUse)
                {
                    if (description.isInterruptable && serverCharacter is ServerPlayerCharacter playerCharacter)
                    {
                        //ability is canceled by movement so before starting the ability we will instead cancel movement
                        //this prevents the ability to immediately be canceled if we cast it during movement
                        playerCharacter.CancelMovement();
                    }

                    ability.StartTime = Time.time;

                    var shouldEnd = !ability.Start();
                    if (ability.Description.cooldown > 0)
                    {
                        TryStartCooldown(ability);
                    }

                    if (shouldEnd)
                    {
                        AdvanceAbilityQueue();
                        return;
                    }

                    if (!ability.IsBlocking())
                    {
                        CurrentRunnables.Add(ability);
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

        private bool TryStartCooldown(AbilityBase ability)
        {
            if (!ability.ShouldStartCooldown()) return false; //cooldown not started

            abilityCooldowns[ability.Description.abilityType] =
                Time.time; //TODO possibly dont set cooldown when ability cancels out of start
            if (serverCharacter is ServerPlayerCharacter playerCharacter)
            {
                playerCharacter.NetworkCharacterState.StartCooldownClientRpc(ability.Description.abilityType,
                    ability.Description.cooldown);
            }

            return true; //cooldown started
        }
    }
}