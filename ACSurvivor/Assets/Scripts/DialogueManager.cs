using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject talkUI;
    public Text talkText;
    public List<string> scriptsLines;

    public void Awake()
    {
        talkText = talkUI.GetComponentInChildren<Text>();
    }




    public void Talk(List<string> scripts)
    {
        if (!talkUI.activeSelf)
        {
            talkUI.SetActive(true);
        }
        scriptsLines = scripts;

        talkText.text = scripts[0];
    }
}
