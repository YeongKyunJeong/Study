using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DialogueData : MonoBehaviour
    {
        public DialogueScript[] DialogueScripts;
    }

    public struct DialogueScript
    {
        public int State;
        public int Random;
        public int Player;
        public string Name;
        public int Redirection;
        public string Content;

        public int ButtonAction1;
        public string ButtonContent1;
        public int ButtonRedirection1;
       
        public int ButtonAction2;
        public string ButtonContent2;
        public int ButtonRedirection2;

        public int ButtonAction3;
        public string ButtonContent3;
        public int ButtonRedirection3;


    }
}
