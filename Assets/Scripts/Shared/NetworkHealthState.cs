using MLAPI;
using MLAPI.NetworkVariable;

namespace Shared
{
    public class NetworkHealthState : NetworkBehaviour
    {
        public NetworkVariableInt MaxHealth = new NetworkVariableInt(100);
        public NetworkVariableInt CurrentHealth = new NetworkVariableInt(50);
    }
}