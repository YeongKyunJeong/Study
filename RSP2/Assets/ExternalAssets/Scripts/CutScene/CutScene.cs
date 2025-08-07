using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum CutsceneAnimation
    {
        Idling,
        Walking,
        StandingUp,
        Laying
    }

    public enum CutsceneActor
    {
        None,
        Player,
        Enemy,
        NPC
    }

    public enum FadingType
    {
        None,
        SlowFadeIn,
        FastFadeIn,
        SlowFadeOut,
        FastFadeOut,
    }

    public class Cutscene : MonoBehaviour
    {
        private readonly int instantIdlingUpHash = Animator.StringToHash("CutScene.Idling");
        private readonly int instantWalkingUpHash = Animator.StringToHash("CutScene.Walking");
        private readonly int instantLayingHash = Animator.StringToHash("CutScene.Laying");
        private readonly int instantStandingUpHash = Animator.StringToHash("CutScene.StandingUp");

        private readonly int cutsceneEndHash = Animator.StringToHash("CutsceneEnd");

        //[field: SerializeField] private int cutSceneID;
        [field: SerializeField] private CinemachineVirtualCamera cutsceneCamera;
        [field: SerializeField] private List<OneCut> cuts;

        private Coroutine cutsceneCoroutine;
        private Coroutine timerCoroutine;
        private bool skipSignal = false;
        private float elapsed = 0f;
        private float cameraT;
        private float actorT;
        private Transform actorTransform;
        private int animationHash;

        public void Play()
        {
            if (cuts == null || cuts.Count == 0) return;


            cutsceneCoroutine = StartCoroutine(PlayCutscene());

        }

        private IEnumerator PlayCutscene(float waitTime = 5)
        {
            CameraManager.Instance.AddCamera(cutsceneCamera);

            foreach (OneCut cut in cuts)
            {
                skipSignal = false;


                if (cut.startWithScreen)
                {
                    GameManager.Instance.SceneFader.SetScreen(true, true);
                }
                else
                {
                    GameManager.Instance.SceneFader.SetScreen(false, true);
                }

                if (cut.fadeInOrOut != FadingType.None)
                {
                    GameManager.Instance.SceneFader.CallFade(cut.fadeInOrOut, false, null);
                }

                ReadyCamera(cut);

                ReadyActor(cut);

                elapsed = 0;
                while (elapsed < cut.cameraMoveTime || elapsed < cut.actorMoveTime ) // 
                {
                    if (cut.moveCamera && elapsed < cut.cameraMoveTime)
                    {
                        ///////////////////////////////////////////////
                        // TO DO :: Add Camera Moving Logic
                    }

                    if (cut.moveCamera && elapsed < cut.cameraMoveTime)
                    {
                        ///////////////////////////////////////////////
                        // To DO :: Actor Moving Logic
                    }

                    elapsed += Time.deltaTime;
                    yield return null;
                }


                elapsed = 0;
                if (cut.needClickToEnd)
                {
                    while (elapsed < waitTime && !skipSignal) // 
                    {
                        elapsed += Time.deltaTime;
                        yield return null;
                    }
                }
                else
                {
                    yield return new WaitForSeconds(3);
                }

                EndActorAnimation(cut);

            }

            CameraManager.Instance.RemoveCamera(cutsceneCamera);
        }

        private void ReadyActor(OneCut cut)
        {
            if (cut.actor != CutsceneActor.None)
            {
                switch (cut.actorAnimation)
                {
                    case CutsceneAnimation.Idling:
                        {
                            animationHash = instantIdlingUpHash;
                            break;
                        }
                    case CutsceneAnimation.Walking:
                        {
                            animationHash = instantWalkingUpHash;
                            break;
                        }
                }
            }

            switch (cut.actor)
            {
                case CutsceneActor.None: break;
                case CutsceneActor.Player:
                    {
                        actorTransform = InGameManager.Instance.Player.transform;
                        actorT = 0;
                        InGameManager.Instance.Player.Animator.Play(animationHash);
                        break;
                    }
                    // TO DO :: Add Logic To Find NPC or Enemy
            }
        }

        private void ReadyCamera(OneCut cut)
        {
            cutsceneCamera.transform.position = cut.cameraStartPos;
            cutsceneCamera.transform.eulerAngles = cut.cameraStartDir;

            if (cut.moveCamera)
            {
                Quaternion from = Quaternion.Euler(cut.cameraStartDir);
                Quaternion to = Quaternion.Euler(cut.cameraEndDir);
            }
        }

        private void EndActorAnimation(OneCut cut)
        {
            switch (cut.actor)
            {
                case CutsceneActor.None: break;
                case CutsceneActor.Player:
                    {
                        InGameManager.Instance.Player.Animator.SetTrigger(cutsceneEndHash);
                        break;
                    }
                    // TO DO :: Add Logic To Find NPC or Enemy
            }
        }

        private IEnumerator CutsceneTimer(float waitTime = 5)
        {
            float elapsed = 0;
            skipSignal = false;

            while (elapsed < waitTime && !skipSignal)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
        }
    }

    [System.Serializable]
    public class OneCut
    {
        [field: SerializeField] public bool needClickToEnd;
        [field: SerializeField] public bool startWithScreen;
        [field: SerializeField] public FadingType fadeInOrOut;

        [Header("Camera")]
        [field: SerializeField] public Vector3 cameraStartPos;
        [field: SerializeField] public Vector3 cameraStartDir;

        [Header("Actor")]
        [field: SerializeField] public CutsceneActor actor;
        [field: SerializeField] public CutsceneAnimation actorAnimation;

        [field: SerializeField] public bool moveActor;
        [field: SerializeField] public float actorMoveTime;

        [field: SerializeField] public Vector3 actorStartPos;
        [field: SerializeField] public Vector3 actorEndPos;
        [field: SerializeField] public Vector3 actorStartDir;
        [field: SerializeField] public Vector3 actorEndDir;

        [Header("Dialogue")]
        [field: SerializeField] public bool needDialogue;
        [field: SerializeField] public int dialogueDataKey;

        [Header("Option")]
        [field: SerializeField] public bool moveCamera;

        [field: SerializeField] public float cameraMoveTime;
        [field: SerializeField] public Vector3 cameraEndPos;
        [field: SerializeField] public Vector3 cameraEndDir;
    }
}
