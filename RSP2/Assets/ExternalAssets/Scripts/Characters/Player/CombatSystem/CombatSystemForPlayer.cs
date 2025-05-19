using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CombatSystemForPlayer : CombatSystem
    {
        public StatHandlerForPlayer StatHandlerForPlayer;
        private ForceReceiverForPlayer forceReceiver;


        private Coroutine mPRegenCoroutine;
        private Coroutine staminaRegenCoroutine;

        protected override void Awake()
        {
            base.Awake();

            StatHandlerForPlayer = GetComponent<StatHandlerForPlayer>();
            forceReceiver = GetComponent<ForceReceiverForPlayer>();
        }

        public void ChangeStatByLevelUp(LevelStatTable nextLevelData)
        {
            CurrentHP += nextLevelData.MaxHP - MaxHP;
            CurrentMP += nextLevelData.MaxHP - MaxHP;
            CurrentStamina += nextLevelData.MaxHP - MaxHP;
        }

        public override void TakeForce(Vector3 force)
        {
            forceReceiver.AddForce(force);
        }
    }
}
