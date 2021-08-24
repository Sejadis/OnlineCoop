using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class CraftingMenu : NetworkBehaviour
{
    public void CraftStuff()
    {
        CraftServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void CraftServerRpc(ServerRpcParams rpcParams = default)
    {
        if (!IsServer) return;
        var inventory = NetworkManager.Singleton.ConnectedClients[rpcParams.Receive.SenderClientId].PlayerObject
            .GetComponent<IInventory>();
        inventory.AddItem();
    }
}

internal interface IInventory
{
    public void AddItem();
}
