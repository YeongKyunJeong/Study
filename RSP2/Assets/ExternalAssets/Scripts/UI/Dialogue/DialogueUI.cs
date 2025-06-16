using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DialogueUI : MonoBehaviour
    {
        private GameManager gameManager;

        [field: SerializeField] private DialogueDisplay otherDialogueDisplay { get; set; }

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            Deactivate();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}
