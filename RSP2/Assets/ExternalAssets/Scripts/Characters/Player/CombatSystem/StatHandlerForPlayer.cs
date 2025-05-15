using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatHandlerForPlayer : StatHandlerForCharacter
    {
        public StatForPlayer PlayerBaseStatistics; /*{ get; private set; }*/
        public StatForPlayer PlayerCurrentStatistics;/* { get; private set; }*/

        public void Initialize(BaseStatTableForPlayer baseStatisticsTable)
        {
            BaseStatTableForPlayer OriginalLoadedDataTable = baseStatisticsTable;
            PlayerBaseStatistics = new StatForPlayer(baseStatisticsTable);
            PlayerCurrentStatistics = new StatForPlayer(baseStatisticsTable);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        public void Initialize(StatForPlayer initialStatistics)
        {
            PlayerBaseStatistics = new StatForPlayer(initialStatistics);
            PlayerCurrentStatistics = new StatForPlayer(initialStatistics);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }

        protected override void CalculateFinalStat()
        {
            BaseStat = PlayerBaseStatistics as StatForCharacter;
            CurrentStatistics = PlayerCurrentStatistics as StatForCharacter;
            base.CalculateFinalStat();
        }
    }
}
