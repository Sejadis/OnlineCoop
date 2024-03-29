﻿using System.Collections;
using MLAPI;
using UnityEngine;

namespace Server.Character
{
    public class ServerAIMovement : NetworkBehaviour
    {
        [SerializeField] private ServerCharacterMovement serverCharacterMovement;

        public override void NetworkStart()
        {
            if (!IsServer)
            {
                enabled = false;
                return;
            }

            StartCoroutine(Move());
        }

        private IEnumerator Move()
        {
            while (true)
            {
                serverCharacterMovement.MoveInput = Vector2.left;
                yield return new WaitForSeconds(2);
                serverCharacterMovement.MoveInput = Vector2.right;
                yield return new WaitForSeconds(2);
            }
        }
    }
}