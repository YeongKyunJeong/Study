using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RSP2
{
    public class DialogueDisplay : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI talkerNameTMP { get; set; }
        [field: SerializeField] private TextMeshProUGUI speechContentTMP { get; set; }
        private string speechScript { get; set; }
        private Coroutine speechCoroutine { get; set; }
        



    }
}
