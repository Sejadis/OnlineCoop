using System.Collections.Generic;
using MLAPI.Messaging;
using MLAPI.Spawning;
using UnityEngine;

namespace Shared.Abilities
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class ServerElectroPole : ServerPlaceableObject
    {
        [SerializeField] private Material lineMaterial;
        public List<ServerElectroPole> connectedPoles = new List<ServerElectroPole>();

        private Dictionary<ulong, LineRenderer> lineRenderers =
            new Dictionary<ulong, LineRenderer>();


        public override void NetworkStart()
        {
            base.NetworkStart();
            if (!IsServer)
            {
                enabled = false;
                return;
            }

            var hits = Physics.OverlapSphere(transform.position, abilityDescription.size / 2);
            foreach (var hit in hits)
            {
                var pole = hit.GetComponent<ServerElectroPole>();
                if (pole != null && pole != this)
                {
                    CreateConnectionsClientRpc(pole.NetworkObjectId);
                }
            }
        }

        [ClientRpc]
        private void CreateConnectionsClientRpc(ulong netObjId)
        {
            var netObj = NetworkSpawnManager.SpawnedObjects[netObjId];
            var pole = netObj.GetComponent<ServerElectroPole>();
            if (pole != null && !connectedPoles.Contains(pole))
            {
                Connect(pole, true);
                pole.Connect(this);
            }
        }

        private void Update()
        {
            List<ulong> keysToRemove = new List<ulong>();
            foreach (var key in lineRenderers.Keys)
            {
                if (NetworkSpawnManager.SpawnedObjects.ContainsKey(key))
                {
                    continue;
                }

                DestroyConnectionClientRpc(key);
                keysToRemove.Add(key);
            }

            if (!IsClient)
            {
                //it will be removed in the client rpc, so if we are host dont remove it here
                foreach (var key in keysToRemove)
                {
                    lineRenderers.Remove(key);
                }
            }
        }

        [ClientRpc]
        private void DestroyConnectionClientRpc(ulong networkObjectId)
        {
            //TODO rework completely to seperate server / client
            if (lineRenderers.ContainsKey(networkObjectId))
            {
                Destroy(lineRenderers[networkObjectId].gameObject);
                lineRenderers.Remove(networkObjectId);
            }
        }

        private void Connect(ServerElectroPole pole, bool hasAuthority = false)
        {
            connectedPoles.Add(pole);
            if (hasAuthority)
            {
                var netId = pole.NetworkObjectId;
                var obj = new GameObject();
                obj.transform.parent = transform;
                obj.transform.position = Vector3.zero;
                lineRenderers[netId] = obj.AddComponent<LineRenderer>();
                lineRenderers[netId].materials = new[] {lineMaterial};
                lineRenderers[netId].SetPositions(new[] {transform.position, pole.transform.position});
            }
        }
    }
}