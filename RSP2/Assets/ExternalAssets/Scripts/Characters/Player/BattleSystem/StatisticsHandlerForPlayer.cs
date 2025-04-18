using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsHandlerForPlayer : StatisticsHandlerForCharacter
    {
        private StatisticsForPlayer PlayerBaseStatistics; /*{ get; private set; }*/
        private StatisticsForPlayer PlayerCurrentStatistics;/* { get; private set; }*/

        public void Initialize()
        {
            PlayerBaseStatistics = new StatisticsForPlayer();
            PlayerCurrentStatistics = new StatisticsForPlayer();
        }

        public void Initialize(StatisticsTableForPlayer baseStatisticsTable)
        {
            StatisticsTableForPlayer OriginalLoadedDataTable = baseStatisticsTable;
            PlayerBaseStatistics = new StatisticsForPlayer(baseStatisticsTable);
            PlayerCurrentStatistics = new StatisticsForPlayer(baseStatisticsTable);
            CalculateFinalStat();
        }

        public void Initialize(StatisticsForPlayer initialStatistics)
        {
            PlayerBaseStatistics = new StatisticsForPlayer(initialStatistics);
            PlayerCurrentStatistics = new StatisticsForPlayer(initialStatistics);
            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            BaseStatistics = PlayerBaseStatistics as StatisticsForCharacter;
            CurrentStatistics = PlayerCurrentStatistics as StatisticsForCharacter;
            base.CalculateFinalStat();
            //BattleSystem battleSystem = GetComponent<BattleSystem>();
            //if (battleSystem != null)
            //{
            //    if (PlayerCurrentStatistics != null)
            //    {
            //        battleSystem.MyFaction = PlayerCurrentStatistics.Faction;

            //    }
            //}
        }
    }
}
