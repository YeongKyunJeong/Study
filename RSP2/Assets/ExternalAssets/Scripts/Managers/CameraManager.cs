using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CameraManager : MonoSingleton<CameraManager>
    {
        GameManager gameManager;
        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
        }
    }
}
