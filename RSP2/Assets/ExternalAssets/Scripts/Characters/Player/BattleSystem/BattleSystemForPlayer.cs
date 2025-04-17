using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class BattleSystemForPlayer : BattleSystem
    {
        public StatisticsHandlerForPlayer StatisticsHandlerFoPlayer;
        protected override void Awake()
        {
            base.Awake();

            StatisticsHandlerFoPlayer = GetComponent<StatisticsHandlerForPlayer>();
        }
    }
}
