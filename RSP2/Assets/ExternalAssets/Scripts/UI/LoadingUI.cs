using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RSP2
{
    public class LoadingUI : MonoBehaviour
    {
        [field: SerializeField] private Image loadingImg;
        [field: SerializeField] private TextMeshProUGUI loadingTMP;
        private GameManager gameManager;
        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            
        }


    }
}
