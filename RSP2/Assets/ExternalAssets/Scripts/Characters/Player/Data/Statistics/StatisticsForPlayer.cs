using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsForPlayer : StatisticsForCharacter
    {
        public StatisticsTableForPlayer OriginalLoadedData { get; private set; }

        
        public void SetStatisticsFromLoader(StatisticsTableForPlayer loadedData)
        {
            Faction = loadedData.Faction;

            MaxHP = loadedData.MaxHP;

            HPRegen = loadedData.HPRegen;

            MaxMP = loadedData.MaxMP;

            MPRegen = loadedData.MPRegen;

            MaxStamina = loadedData.MaxStamina;

            StaminaRegen = loadedData.StaminaRegen;

            Attack = loadedData.Attack;

            Deffence = loadedData.Deffence;

            MovementSpeed = loadedData.MovementSpeed;

            AttackSpeed = loadedData.AttackSpeed;
        }
    }
}
