using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsHandlerForCharacter : MonoBehaviour
    {
        public StatisticsForCharacter BaseStatistics { get; private set; }
        public StatisticsForCharacter CurrentStatistics { get; private set; }

        public void Initialize(StatisticsForCharacter initialStatistics)
        {
            BaseStatistics = new StatisticsForCharacter(initialStatistics);
            CurrentStatistics = new StatisticsForCharacter(initialStatistics);
            CalculateFinalStat();
        }

        protected virtual void CalculateFinalStat()
        {
            //    int health = baseStat.BaseHealth;
            //    float speed = baseStat.BaseSpeed;
            //    int attack = baseStat.BaseAttack;

            //    int flatHealth = 0;
            //    float flatSpeed = 0;
            //    int flatAttack = 0;

            //    float percentHealth = 1;
            //    float percentSpeed = 1;
            //    float percentAttack = 1;

            //    foreach (StatModifier modifier in modifiers)
            //    {
            //        switch (modifier.statType)
            //        {
            //            case StatType.Attack:
            //                if (modifier.modType == StatModType.Flat)
            //                    flatAttack += (int)modifier.value;
            //                else if (modifier.modType == StatModType.Percent)
            //                    percentAttack += modifier.value / 100f;
            //                break;
            //            case StatType.Speed:
            //                if (modifier.modType == StatModType.Flat)
            //                    flatSpeed += (int)modifier.value;
            //                else if (modifier.modType == StatModType.Percent)
            //                    percentSpeed += modifier.value / 100f;
            //                break;
            //            case StatType.Health:
            //                if (modifier.modType == StatModType.Flat)
            //                    flatHealth += (int)modifier.value;
            //                else if (modifier.modType == StatModType.Percent)
            //                    percentHealth += modifier.value / 100f;
            //                break;
            //        }
            //    }

            //    CurrentStat.Initialize(
            //        (int)((health + flatHealth) * percentHealth),
            //        (speed + flatSpeed) * percentSpeed,
            //        (int)((attack + flatAttack) * percentAttack)
            //    );
        }
    }
}
