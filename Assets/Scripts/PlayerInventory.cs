using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.Messaging;
using UnityEngine;

public class PlayerInventory : NetworkBehaviour, IInventory
{
    public void AddItem()
    {
        Debug.Log("added item server " + NetworkObjectId);
        AddItemClientRpc();
    }

    [ClientRpc]
    private void AddItemClientRpc()
    {
        Debug.Log("added item client " + NetworkObjectId);
    }
}
