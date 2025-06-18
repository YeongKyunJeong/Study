using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CSVDataLoader : MonoBehaviour
    {
        public DialogueDataLoader DialogueDataLoader { get; private set; }


        public void Initialize()
        {
            DialogueDataLoader = new DialogueDataLoader();
        }
    }
}
