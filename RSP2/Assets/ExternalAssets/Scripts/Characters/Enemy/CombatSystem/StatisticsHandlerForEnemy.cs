using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsHandlerForEnemy : StatisticsHandlerForCharacter
    {
        private StatisticsForEnemy PlayerBaseStatistics; 
        private StatisticsForEnemy PlayerCurrentStatistics;

        public void Initialize(StatisticsTableForEnemy baseStatisticsTable)
        {
            StatisticsTableForEnemy OriginalLoadedDataTable = baseStatisticsTable;
            PlayerBaseStatistics = new StatisticsForEnemy(baseStatisticsTable);
            PlayerCurrentStatistics = new StatisticsForEnemy(baseStatisticsTable);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        public void Initialize(StatisticsForEnemy initialStatistics)
        {
            PlayerBaseStatistics = new StatisticsForEnemy(initialStatistics);
            PlayerCurrentStatistics = new StatisticsForEnemy(initialStatistics);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            BaseStatistics = PlayerBaseStatistics as StatisticsForCharacter;
            CurrentStatistics = PlayerCurrentStatistics as StatisticsForCharacter;
            base.CalculateFinalStat();
        }
    }
}
