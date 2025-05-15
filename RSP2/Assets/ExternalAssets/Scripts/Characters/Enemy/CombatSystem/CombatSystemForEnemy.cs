using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CombatSystemForEnemy : CombatSystem
    {
        public StatHandlerForEnemy StatHandlerForEnemy;
        protected override void Awake()
        {
            base.Awake();

            StatHandlerForEnemy = GetComponent<StatHandlerForEnemy>();
        }
    }
}
