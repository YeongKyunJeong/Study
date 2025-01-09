using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(StageManagerNeo))]
public class StageEditor : Editor
{
    private static int defaultLevel;

    private const int MAX_BRICK_ROW_COUNT = 6;
    private const int MIN_BRICK_ROW_COUNT = 1;
    private const int MAX_BRICK_PER_ROW_COUNT = 7;
    private const int MIN_BRICK_PER_ROW_COUNT = 1;
    private const float MIN_BRICK_SPACE_X = 1;
    private const float MIN_BRICK_SPACE_Y = 1;


    private int brickRowCount = 5;
    private int brickPerRow = 7;
    private float brickSpaceX = 0.25f;
    private float brickSpaceY = 0.25f;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(20);

        StageManagerNeo stageManagerNeo = (StageManagerNeo)target;

        brickRowCount = EditorGUILayout.IntField("블록 행 개수", brickRowCount);
        brickPerRow = EditorGUILayout.IntField("블록 열 개수", brickPerRow);
        brickSpaceX = EditorGUILayout.FloatField("블록 열 간격", brickSpaceX);
        brickSpaceY = EditorGUILayout.FloatField("블록 행 간격", brickSpaceY);

        GUILayout.Space(10);
        GUILayout.Label("Stage Editor", EditorStyles.boldLabel);
        if (GUILayout.Button("GenerateBricks"))
        {
            if (brickRowCount > MAX_BRICK_ROW_COUNT)
            {
                brickRowCount = MAX_BRICK_ROW_COUNT;
            }
            else if (brickRowCount <= 0)
            {
                brickRowCount = MIN_BRICK_ROW_COUNT;
            }
            if (brickPerRow > MAX_BRICK_PER_ROW_COUNT)
            {
                brickPerRow = MAX_BRICK_PER_ROW_COUNT;
            }
            else if (brickPerRow <= 0)
            {
                brickPerRow = MIN_BRICK_PER_ROW_COUNT;
            }
            if (brickSpaceX < 0)
            {
                brickSpaceX = 0;
            }
            if (brickSpaceY < 0)
            {
                brickSpaceY = 0;
            }

            stageManagerNeo.GenerateBricks(brickRowCount, brickPerRow, brickSpaceX, brickSpaceY);
        }
    }
}
