using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CombatSystemForEnemy : CombatSystem
    {
        public StatisticsHandlerForEnemy StatisticsHandlerForEnemy;
        protected override void Awake()
        {
            base.Awake();

            StatisticsHandlerForEnemy = GetComponent<StatisticsHandlerForEnemy>();
        }
    }
}
