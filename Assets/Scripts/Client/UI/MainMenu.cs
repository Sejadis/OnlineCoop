using System.Net;
using MLAPI;
using MLAPI.Transports.UNET;
using TMPro;
using UnityEngine;

namespace Client.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private TMP_InputField adressInput;
        public void StartAsHost()
        {
            NetworkManager.Singleton.StartHost();
            gameObject.SetActive(false);
        }
    
        public void StartAsClient()
        {
            var hostAddresses = Dns.GetHostAddresses(adressInput.text);
            NetworkManager.Singleton.GetComponent<UNetTransport>().ConnectAddress = hostAddresses[0]?.ToString();
            NetworkManager.Singleton.StartClient();
            gameObject.SetActive(false);
        }
    }
}
