using Shared;
using Shared.Abilities;
using Shared.Data;
using UnityEngine;

namespace Client.UI
{
    public class GameHudUI : UIScreen
    {
        [SerializeField] private AbilityUI abilityUI;
        [SerializeField] private PartyUI partyUI;
        [SerializeField] private AbilityProgressUI abilityProgressUI;
        [SerializeField] private StatusEffectUI statusEffectUI;
        
        public void InitPlayer(NetworkCharacterState playerState, string playerName)
        {
            Show();
            //setup player frame
            partyUI.RegisterPlayer(playerName, playerState);

            //setup abilities
            playerState.OnStartCooldown += abilityUI.TriggerCooldown;
            for (int i = 0; i < 3; i++)
            {
                var type = (AbilityType) playerState.equippedAbilities.Value[i];
                if (GameDataManager.TryGetAbilityDescriptionByType(type, out var ability))
                {
                    abilityUI.SetAbility(ability, i);
                }
            }

            //setup status effects
            playerState.OnClientStatusEffectAdded +=
                runtimeParams => statusEffectUI.AddStatusEffect(ref runtimeParams);

            //setup castbar
            abilityProgressUI.Init(playerState);
        }

        public void InitPartyMember(NetworkCharacterState playerState, string playerName)
        {
            partyUI.RegisterPartyMember(playerName, playerState);
        }
    }
}