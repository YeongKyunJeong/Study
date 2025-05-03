using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsHandlerForEnemy : StatisticsHandlerForCharacter
    {
        public StatisticsForEnemy EnemyBaseStatistics; 
        public StatisticsForEnemy EnemyCurrentStatistics;

        public void Initialize(StatisticsTableForEnemy baseStatisticsTable)
        {
            StatisticsTableForEnemy OriginalLoadedDataTable = baseStatisticsTable;
            EnemyBaseStatistics = new StatisticsForEnemy(baseStatisticsTable);
            EnemyCurrentStatistics = new StatisticsForEnemy(baseStatisticsTable);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        public void Initialize(StatisticsForEnemy initialStatistics)
        {
            EnemyBaseStatistics = new StatisticsForEnemy(initialStatistics);
            EnemyCurrentStatistics = new StatisticsForEnemy(initialStatistics);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            BaseStatistics = EnemyBaseStatistics as StatisticsForCharacter;
            CurrentStatistics = EnemyCurrentStatistics as StatisticsForCharacter;
            base.CalculateFinalStat();
        }
    }
}
