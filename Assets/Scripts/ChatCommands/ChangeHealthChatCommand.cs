using MLAPI.Spawning;

namespace DefaultNamespace.Chat
{
    public class ChangeHealthChatCommand : ChatCommand
    {
        public override void Execute(string[] parameters, ulong receiveSenderClientId)
        {
            var id = ulong.Parse(parameters[0]);
            var isMax = parameters[1] == "max";
            var isCurrent = parameters[1] == "current";
            var netState = NetworkSpawnManager.SpawnedObjects[id].GetComponent<NetworkState>();
            if (isMax)
            {
                netState.MaxHealth.Value = int.Parse(parameters[2]);
            }

            if (isCurrent)
            {
                netState.CurrentHealth.Value = int.Parse(parameters[2]);
            }
        }

        public ChangeHealthChatCommand() : base("setHealth", 3)
        {
        }
    }
}