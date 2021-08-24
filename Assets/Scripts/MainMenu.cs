using System.Collections;
using System.Collections.Generic;
using MLAPI;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void StartAsHost()
    {
        NetworkManager.Singleton.StartHost();
        gameObject.SetActive(false);
    }
    
    public void StartAsClient()
    {
        NetworkManager.Singleton.StartClient();
        gameObject.SetActive(false);
    }
}
