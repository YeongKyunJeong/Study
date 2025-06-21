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
        [field: SerializeField] private DialogueDisplay currentDialogueDisplay;

        private DialogueData dialogueData { get; set; }
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
            dialogueData = DataManager.Instance.TableDataLoader.DialogueDataLoader.GetByKey(key);

            switch (dialogueType)
            {
                case DialogueType.NPC:
                    {
                        dialogueScriptSet =
                    DataManager.Instance.TableDataLoader.DialogueScriptsLoader.GetByMultipleKeys(dialogueData.ScriptKeys);
                        break;
                    }
            }


            scriptLength = dialogueScriptSet.Length;
            scriptIndex = 0;

            if (dialogueData.Random)
            {
                scriptIndex = Random.Range(0, scriptLength);
            }

            TalkOneScript(dialogueData, dialogueScriptSet[scriptIndex]);
        }

        public int Next()
        {
            if (currentDialogueDisplay.isPlaying)
            {
                currentDialogueDisplay.EndScript();
                return -1;
            }

            if (!dialogueData.Random && scriptIndex < scriptLength)
            {
                TalkOneScript(dialogueData, dialogueScriptSet[scriptIndex]);
                return -1;
            }

            return dialogueData.Redirection;

        }

        private void TalkOneScript(DialogueData data, DialogueScript script)
        {
            if (script.Player)
            {
                currentDialogueDisplay = playerDialogueDisplay;
                otherDialogueDisplay.Deactivate();
                playerDialogueDisplay.Activate();
                playerDialogueDisplay.SetName(gameManager.Player.Name);
                playerDialogueDisplay.SetScript(script);
            }
            else
            {
                currentDialogueDisplay = otherDialogueDisplay;
                playerDialogueDisplay.Deactivate();
                otherDialogueDisplay.Activate();
                otherDialogueDisplay.SetName(data.Name);
                otherDialogueDisplay.SetScript(script);
            }
            scriptIndex++;
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
