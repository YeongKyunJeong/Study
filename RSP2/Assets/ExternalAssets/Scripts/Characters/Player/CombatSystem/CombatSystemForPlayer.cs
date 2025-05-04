using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CombatSystemForPlayer : CombatSystem
    {
        public StatisticsHandlerForPlayer StatisticsHandlerForPlayer;

        private Coroutine mPRegenCoroutine;
        private Coroutine staminaRegenCoroutine;

        protected override void Awake()
        {
            base.Awake();

            StatisticsHandlerForPlayer = GetComponent<StatisticsHandlerForPlayer>();
        }
    }
}
