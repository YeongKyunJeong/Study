using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum CutSceneAnimation
    {
        Idling,
        Walking,
        StandingUp,
        Laying
    }

    public enum CutSceneActor
    {
        None,
        Player,
        Enemy,
        NPC
    }

    public enum CutSceneFadeInAndOut
    {
        None,
        SlowFadeIn,
        FastFadeIn,
        SlowFadeOut,
        FastFadeOut,
    }

    public class CutScene : MonoBehaviour
    {
        //[field: SerializeField] private int cutSceneID;
        [field: SerializeField] private CinemachineVirtualCamera cutSceneCamera;
        [field: SerializeField] private List<OneCut> cuts;
    }

    [System.Serializable]
    public class OneCut
    {
        [field: SerializeField] private bool needClickToEnd;
        [field: SerializeField] private bool startWithScreen;
        [field: SerializeField] private CutSceneFadeInAndOut fadeInOrOut;

        [Header("Camera")]
        [field: SerializeField] private Vector3 cameraStartPos;
        [field: SerializeField] private Vector3 cameraStartDir;

        [Header("Actor")]
        [field: SerializeField] private CutSceneActor actor;
        [field: SerializeField] private CutSceneAnimation actorAnimation;

        [field: SerializeField] private bool moveActor;
        [field: SerializeField] private float actorMoveTime;

        [field: SerializeField] private Vector3 actorStartPos;
        [field: SerializeField] private Vector3 actorEndPos;
        [field: SerializeField] private Vector3 actorStartDir;
        [field: SerializeField] private Vector3 actorEndDir;

        [Header("Dialogue")]
        [field: SerializeField] private bool needDialogue;
        [field: SerializeField] private int dialogueDataKey;

        [Header("Option")]
        [field: SerializeField] private bool moveCamera;

        [field: SerializeField] private float cameraMoveTime;
        [field: SerializeField] private Vector3 cameraEndPos;
        [field: SerializeField] private Vector3 cameraEndDir;
    }
}
