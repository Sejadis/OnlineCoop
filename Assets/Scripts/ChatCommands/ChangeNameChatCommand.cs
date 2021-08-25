using MLAPI.Spawning;
using UnityEngine;

public class ChangeNameChatCommand : ChatCommand
{
    public override void Execute(string[] parameters, ulong receiveSenderClientId)
    {
        var id = ulong.Parse(parameters[0]);
        NetworkSpawnManager.SpawnedObjects[id].GetComponent<CharacterNetworkState>().PlayerName.Value =
            parameters[1];
    }

    public ChangeNameChatCommand() : base("name", 2)
    {
    }
}