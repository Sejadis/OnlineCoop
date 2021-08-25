using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.NetworkVariable;
using UnityEngine;

public class NetworkHealthState : NetworkState
{
    public NetworkVariableInt MaxHealth = new NetworkVariableInt(100);
    public NetworkVariableInt CurrentHealth = new NetworkVariableInt(50);
}