using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

namespace RSP2
{
    public class DialogueUI : MonoBehaviour
    {
        private GameManager gameManager;

        [field: SerializeField] private DialogueDisplay otherDialogueDisplay { get; set; }
        [field: SerializeField] private DialogueDisplay playerDialogueDisplay { get; set; }

        private DialogueData totalDialogueDataTable { get; set; }
        private DialogueScript[] thisStateScript { get; set; }
        private int scriptLength { get; set; }
        private int scriptIndex { get; set; }

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            Deactivate();
        }

        public void StartDialogue(DialogueType dialogueType, int key, int startState)
        {
            totalDialogueDataTable =
        DataManager.Instance.CSVDataLoader.DialogueDataLoader.GetDialogueData(dialogueType, key);

            thisStateScript = FindThisStateScripts(startState);
            scriptLength = thisStateScript.Length;
            scriptIndex = 0;

            TalkOneScript(thisStateScript[scriptIndex]);
            /////////////////////////////////////////////////////////

        }

        private DialogueScript[] FindThisStateScripts(int state)
        {
            return totalDialogueDataTable.DialogueScripts.Where(x => x.State == state).ToArray();
        }

        private void TalkOneScript(DialogueScript script)
        {
            if (script.IsRandom)
            {
                // TO DO :: Add random talk logic


            }
            else
            {
                if (script.IsPlayerScript)
                {
                    playerDialogueDisplay.SetName(script.Name);
                    playerDialogueDisplay.SetScript(script);
                    return;
                }

                otherDialogueDisplay.SetName(script.Name);
                otherDialogueDisplay.SetScript(script);
                return;
            }

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
