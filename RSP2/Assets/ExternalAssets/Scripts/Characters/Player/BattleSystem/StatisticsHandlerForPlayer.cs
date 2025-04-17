using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsHandlerForPlayer : StatisticsHandlerForCharacter
    {
        new public StatisticsForPlayer BaseStatistics { get; private set; }
        new public StatisticsForPlayer CurrentStatistics { get; private set; }

        public void Initialize()
        {
            BaseStatistics = new StatisticsForPlayer();
            CurrentStatistics = new StatisticsForPlayer();
        }

        public void Initialize(StatisticsTableForPlayer baseStatisticsTable)
        {
            StatisticsTableForPlayer OriginalLoadedDataTable = baseStatisticsTable;
            BaseStatistics = new StatisticsForPlayer(baseStatisticsTable);
            CurrentStatistics = new StatisticsForPlayer(baseStatisticsTable);
            CalculateFinalStat();
        }

        public void Initialize(StatisticsForPlayer initialStatistics)
        {
            BaseStatistics = new StatisticsForPlayer(initialStatistics);
            CurrentStatistics = new StatisticsForPlayer(initialStatistics);
            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            base.CalculateFinalStat();
        }
    }
}
