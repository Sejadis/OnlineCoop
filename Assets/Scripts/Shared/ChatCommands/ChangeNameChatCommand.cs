﻿using MLAPI.Spawning;
using Server.Chat;

namespace Shared.ChatCommands
{
    public class ChangeNameChatCommand : ChatCommand
    {
        public override void Execute(string[] parameters, ulong receiveSenderClientId)
        {
            var id = ulong.Parse(parameters[0]);
            NetworkSpawnManager.SpawnedObjects[id].GetComponent<NetworkCharacterState>().PlayerName.Value =
                parameters[1];
        }

        public ChangeNameChatCommand() : base("name", 2)
        {
        }
    }
}