using MLAPI.Spawning;
using UnityEngine;

public class ChangeIconChatCommand : ChatCommand
{
    public override void Execute(string[] parameters, ulong receiveSenderClientId)
    {
        var id = ulong.Parse(parameters[0]);
        NetworkSpawnManager.SpawnedObjects[id].GetComponent<NetworkState>().IconName.Value =
            parameters[1];
    }

    public ChangeIconChatCommand() : base("icon", 2)
    {
    }
}