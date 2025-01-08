using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageManagerNeo))]
public class StageEditor :Editor
{
    private static int defaultLevel;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(20);

        StageManagerNeo stageManagerNeo = (StageManagerNeo)target;

        GUILayout.Label("Stage Editor", EditorStyles.boldLabel);
        if (GUILayout.Button("GenerateBricks"))
        {
            stageManagerNeo.GenerateBricks(5, 7, 0.5f, 0.25f);
        }
    }
}
