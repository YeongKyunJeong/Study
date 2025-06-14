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
        [field: SerializeField] public InteractionHitBoxForNPC InteractionHiyBox { get; private set; }

        protected override void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }


            if (InteractionHiyBox == null)
            {
                throw new NotImplementedException("Enemy Mover Not Assigned");
            }
            InteractionHiyBox.Initialize(this, new bool[] { true, false });

            //StatisticsHandler.InitializeByDefault();
            StatHandler.Initialize(DataManager.Instance.TableDataLoader.StatLoaderForNPC.GetByKey(NPCKey));

            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            RuntimeData.SearchingDistance = searchingDistance;
            RuntimeData.SearchingDistanceSqr = searchingDistance * searchingDistance;

            RuntimeData.IsHostile = false;
        }

    }
}

