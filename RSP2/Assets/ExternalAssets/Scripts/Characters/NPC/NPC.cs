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
        [field: SerializeField] public NPCCamera NPCCamera { get; private set; }

        [field: SerializeField] public bool HasDialogue { get; set; }
        [field: SerializeField] public int DialogueKey { get; set; }

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

            if (NPCCamera != null)
            {
                CameraManager.Instance.AddCamera(NPCCamera.VirtualCamera);
            }


            StatHandler.Initialize(this, DataManager.Instance.TableDataLoader.StatLoaderForNPC.GetByKey(NPCKey));

            if (HasDialogue)
            {
                // TO DO :: Add logic to save and load dialogue state
                //DataManager.Instance.TableDataLoader.DialogueDataLoader.CallDialogueDataLoading(DialogueType.NPC, NPCKey, Name);
            }


            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            RuntimeData.SearchingDistance = searchingDistance;
            RuntimeData.SearchingDistanceSqr = searchingDistance * searchingDistance;

            RuntimeData.IsHostile = false;
        }

    }
}

