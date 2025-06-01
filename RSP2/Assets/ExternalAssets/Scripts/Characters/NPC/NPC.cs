using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class NPC : Enemy
    {
        public int NPCKey;
        protected override void Start()
        {
            if (gameManager == null)
            {
                gameManager = GameManager.Instance;
            }

            //StatisticsHandler.InitializeByDefault();
            StatHandler.Initialize(gameManager.DataManager.TableDataLoader.StatLoaderForNPC.GetByKey(NPCKey));

            CombatSystem.DamageEvent += OnHit;
            CombatSystem.DieEvent += OnDie;

            RuntimeData.SearchingDistance = searchingDistance;
            RuntimeData.SearchingDistanceSqr = searchingDistance * searchingDistance;

            RuntimeData.IsHostile = false;
        }

    }
}

