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

        private DialogueScript[] dialogueScriptSet { get; set; }
        private int scriptLength { get; set; }
        private int scriptIndex { get; set; }

        public void Initialize(GameManager _gameManager)
        {
            gameManager = _gameManager;
            otherDialogueDisplay.Initialize();
            playerDialogueDisplay.Initialize();
            Deactivate();
        }

        public void StartDialogue(DialogueType dialogueType, int key)
        {
            DialogueData data = DataManager.Instance.TableDataLoader.DialogueDataLoader.GetByKey(key);

            switch (dialogueType)
            {
                case DialogueType.NPC:
                    {
                        dialogueScriptSet =
                    DataManager.Instance.TableDataLoader.DialogueScriptsLoader.GetByMultipleKeys(data.ScriptKeys);
                        break;
                    }
            }


            scriptLength = dialogueScriptSet.Length;
            scriptIndex = 0;

            if (data.Random)
            {
                scriptIndex = Random.Range(0, scriptLength);
            }

            TalkOneScript(data, dialogueScriptSet[scriptIndex]);
            /////////////////////////////////////////////////////////

        }

        private void TalkOneScript(DialogueData data, DialogueScript script)
        {

            if (script.Player)
            {
                otherDialogueDisplay.Deactivate();
                playerDialogueDisplay.Activate();
                playerDialogueDisplay.SetName(gameManager.Player.Name);
                playerDialogueDisplay.SetScript(script);
            }
            else
            {
                playerDialogueDisplay.Deactivate();
                otherDialogueDisplay.Activate();
                otherDialogueDisplay.SetName(data.Name);
                otherDialogueDisplay.SetScript(script);
            }
            //if (script.)
            //{
            //    // TO DO :: Add random talk logic


            //}
            //else
            //{
            //    if (script.IsPlayerScript)
            //    {
            //        playerDialogueDisplay.SetName(script.Name);
            //        playerDialogueDisplay.SetScript(script);
            //        return;
            //    }

            //    otherDialogueDisplay.SetName(script.Name);
            //    otherDialogueDisplay.SetScript(script);
            //    return;
            //}

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
