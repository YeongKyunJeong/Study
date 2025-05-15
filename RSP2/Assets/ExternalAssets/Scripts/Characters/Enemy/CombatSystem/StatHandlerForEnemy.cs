using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatHandlerForEnemy : StatHandlerForCharacter
    {
        public StatForEnemy EnemyBaseStatistics; 
        public StatForEnemy EnemyCurrentStatistics;

        public void Initialize(StatTableForEnemy baseStatisticsTable)
        {
            StatTableForEnemy OriginalLoadedDataTable = baseStatisticsTable;
            EnemyBaseStatistics = new StatForEnemy(baseStatisticsTable);
            EnemyCurrentStatistics = new StatForEnemy(baseStatisticsTable);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        public void Initialize(StatForEnemy initialStatistics)
        {
            EnemyBaseStatistics = new StatForEnemy(initialStatistics);
            EnemyCurrentStatistics = new StatForEnemy(initialStatistics);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            BaseStat = EnemyBaseStatistics as StatForCharacter;
            CurrentStatistics = EnemyCurrentStatistics as StatForCharacter;
            base.CalculateFinalStat();
        }
    }
}
