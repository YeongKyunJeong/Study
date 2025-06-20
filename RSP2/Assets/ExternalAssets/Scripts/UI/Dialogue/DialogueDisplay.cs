using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace RSP2
{
    public class DialogueDisplay : MonoBehaviour
    {
        [field: SerializeField] private TextMeshProUGUI talkerNameTMP { get; set; }
        [field: SerializeField] private TextMeshProUGUI speechContentTMP { get; set; }
        //[field: SerializeField] private TMP_Text speechContentTMP { get; set; }
        private string speechScript { get; set; }
        private Coroutine speechCoroutine { get; set; }
        private WaitForSeconds waitingSecond;
        private StringBuilder stringBuilder;
        ////////////////////////////////////////////////////


        public void Initialize()
        {
            stringBuilder = new StringBuilder();
            Deactivate();
        }

        public void SetName(string name)
        {
            talkerNameTMP.text = name;
        }

        public void SetScript(DialogueScript script, float letterPerSec = 30)
        {
            speechContentTMP.text = string.Empty;
            speechScript = script.ScriptContent;

            speechCoroutine = StartCoroutine(TalkTyping(letterPerSec));
        }

        public void MakeScriptDone()
        {
            StopCoroutine(speechCoroutine);


        }

        IEnumerator TalkTyping(float letterPerSec)
        {
            stringBuilder.Clear();
            waitingSecond = new WaitForSeconds(1 / letterPerSec);
            for (int i = 0; i < speechScript.Length; i++)
            {
                stringBuilder.Append(speechScript[i]);
                speechContentTMP.text = stringBuilder.ToString();
                if (speechScript[i] == ' ')
                {
                    yield return null;
                }
                yield return waitingSecond;
            }

            // TO DO:: End Logic
            yield return null;
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
