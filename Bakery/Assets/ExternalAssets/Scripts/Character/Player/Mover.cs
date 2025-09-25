using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Bakery
{
    public class Mover : MonoBehaviour
    {
        private CharacterController controller;

        public void Initialize(PlayerManager _player)
        {
            controller = _player.Controller;
        }
    }
}
