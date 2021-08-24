using System.Collections;
using System.Collections.Generic;
using MLAPI;
using SejDev.Systems.Ability;
using UnityEngine;

public class ServerCharacter : NetworkBehaviour
{
    private AbilityHandler abilityHandler;
    private NetworkState networkState;
    
    // Start is called before the first frame update
    void Start()
    {
        networkState = GetComponent<NetworkState>();
        abilityHandler = new AbilityHandler(networkState);
    }

    // Update is called once per frame
    void Update()
    {
        abilityHandler.Update();

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            var runtimeParams = new AbilityRuntimeParams(AbilityType.Fireball,NetworkObjectId,0,Vector3.zero, transform.forward,transform.position);
            abilityHandler.StartAbility(ref runtimeParams);
        }
    }
}
