using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class NPC : Enemy
    {
        public int NPCKey;
        [field: SerializeField] public NPCInteraction Interactions { get; private set; }
        [field: SerializeField] public InteractionHitBoxForNPC InteractionHitBox { get; private set; }
        [field: SerializeField] public NPCCamera nPCCamera { get; private set; }

        [field: SerializeField] public bool hasDialogue { get; private set; }

        protected override void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            if (InteractionHitBox == null)
            {
                Debug.LogError("Interaction Hit Box Not Assigned");
                //throw new NotImplementedException("Interaction Hit Box Not Assigned");
            }

            if (nPCCamera != null)
            {
                CameraManager.Instance.AddCamera(nPCCamera.VirtualCamera);
            }

            InteractionHitBox.Initialize(this, new bool[] { true, false });

            //StatisticsHandler.InitializeByDefault();
            StatHandler.Initialize(DataManager.Instance.TableDataLoader.StatLoaderForNPC.GetByKey(NPCKey));

            // TO DO :: Add loading logic whether has dialogue
            if (hasDialogue)
            {
                // TO DO :: Change to be done via DataManager
                DataManager.Instance.CSVDataLoader.DialogueDataLoader.CallDialogueDataLoading(DialogueType.NPC, NPCKey, Name);
            }


            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            RuntimeData.SearchingDistance = searchingDistance;
            RuntimeData.SearchingDistanceSqr = searchingDistance * searchingDistance;

            RuntimeData.IsHostile = false;
        }

    }
}

