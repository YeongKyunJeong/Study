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

        // TO DO:: Add dialogue data CSV loading logic

        public void CallDialogue(int npcKey, int startState)
        {
            if (totalDialogueDataTable == null) totalDialogueDataTable = LoadTotalDialogueData();

            thisStateScript = FindThisStateScripts(startState);
            scriptLength = thisStateScript.Length;
            scriptIndex = 0;


            // TO DO:: 
        }

        private DialogueData LoadTotalDialogueData()
        {
            // TO DO:: CSV File path
            return new DialogueData();
        }

        private DialogueScript[] FindThisStateScripts(int state)
        {
            return totalDialogueDataTable.DialogueScripts.Where(x => x.State == state).ToArray();
        }

        private void TalkOneScript(DialogueScript script)
        {
            if (script.Random == 0)
            {
                if (script.Player == 0)
                {

                }


            }
            else
            {
                // TO DO :: Add random talk logic
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
