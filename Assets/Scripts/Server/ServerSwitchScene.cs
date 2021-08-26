using System;
using System.Collections;
using System.Collections.Generic;
using MLAPI;
using MLAPI.SceneManagement;
using UnityEngine;

public class ServerSwitchScene : NetworkBehaviour
{
    [SerializeField] private string sceneName = "ArenaBoss";
    public override void NetworkStart()
    {
        base.NetworkStart();
        if (!IsServer)
        {
            enabled = false;
            return;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        NetworkSceneManager.SwitchScene(sceneName);
    }
}
