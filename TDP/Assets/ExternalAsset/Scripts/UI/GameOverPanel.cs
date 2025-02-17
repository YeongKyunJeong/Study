using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDP
{
    public class GameOverPanel : MonoBehaviour
    {
        private GameManager gameManager;

        public Text roundsText;

        public void Initialize()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            if (gameObject.activeInHierarchy)
            {
                OnEnableByManual(false);
            }
        }
        //private void OnEnable()
        //{
        //    roundsText.text = PlayerStats.Rounds.ToString();
        //}

        public void OnEnableByManual(bool isOn)
        {
            gameObject.SetActive(isOn);
            if (isOn)
            {
                roundsText.text = PlayerStats.Rounds.ToString();
            }
        }

        public void RetryCall()
        {
            gameManager.RetryCall();
        }

        public void MenuCall()
        {
            gameManager.MenuCall();
        }
    }
}
