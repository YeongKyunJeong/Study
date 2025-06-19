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

        public void Initialize(NPC nPC, StatTableForNPC baseStatisticsTable)
        {
            StatTableForNPC OriginalLoadedDataTable = baseStatisticsTable;
            EnemyBaseStatistics = new StatForNPC(baseStatisticsTable);
            EnemyCurrentStatistics = new StatForNPC(baseStatisticsTable);

            nPC.Name = EnemyBaseStatistics.Name;
            nPC.DialogueKey = baseStatisticsTable.DialogueStartKey;
            bool[] interactions = new bool[2] { false, false};

            if (baseStatisticsTable.HasDialogue)
            {
                nPC.HasDialogue = true;
                interactions[0] = true;

            }
            if (baseStatisticsTable.Tradable) interactions[1] = true;
            nPC.InteractionHitBox.Initialize(nPC, interactions);

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

        private void SetNPCParameter()
        {

        }
    }
}
