using MLAPI.Spawning;
using Server.Chat;

namespace Shared.ChatCommands
{
    public class ChangeIconChatCommand : ChatCommand
    {
        public override void Execute(string[] parameters, ulong receiveSenderClientId)
        {
            var id = ulong.Parse(parameters[0]);
            NetworkSpawnManager.SpawnedObjects[id].GetComponent<NetworkCharacterState>().IconName.Value =
                parameters[1];
        }

        public ChangeIconChatCommand() : base("icon", 2)
        {
        }
    }
}