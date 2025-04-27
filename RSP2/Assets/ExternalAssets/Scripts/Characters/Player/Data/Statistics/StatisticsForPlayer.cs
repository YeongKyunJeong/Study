using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatisticsForPlayer : StatisticsForCharacter
    {
        public StatisticsForPlayer()
        {
            key = -1;

            Name = "Player";

            Faction = Faction.Player;

            MaxHP = 10;

            HPRegen = 10;

            MaxMP = 10;

            MPRegen = 10;

            MaxStamina = 10;

            StaminaRegen = 10;

            Attack = 10;

            Deffence = 10;

            MovementSpeed = 10;

            AttackSpeed = 10;
        }

        public StatisticsForPlayer(StatisticsTableForPlayer baseStatisticsTable)
        {
            //Debug.Log("Loaded by JSON : Player");
            SetStatisticsByTable(baseStatisticsTable);
        }

        public StatisticsForPlayer(StatisticsForPlayer baseStatistics)
        {
            SetStatistics(baseStatistics);
        }

        public void SetStatisticsByTable(StatisticsTableForPlayer newDataTable)
        {
            key = newDataTable.key;

            Name = newDataTable.Name;
            
            Faction = newDataTable.Faction;

            MaxHP = newDataTable.MaxHP;

            HPRegen = newDataTable.HPRegen;

            MaxMP = newDataTable.MaxMP;

            MPRegen = newDataTable.MPRegen;

            MaxStamina = newDataTable.MaxStamina;

            StaminaRegen = newDataTable.StaminaRegen;

            Attack = newDataTable.Attack;

            Deffence = newDataTable.Deffence;

            MovementSpeed = newDataTable.MovementSpeed;

            AttackSpeed = newDataTable.AttackSpeed;
        }

        private void SetStatistics(StatisticsForPlayer newData)
        {
            key = newData.key;

            Name = newData.Name;

            Faction = newData.Faction;

            MaxHP = newData.MaxHP;

            HPRegen = newData.HPRegen;

            MaxMP = newData.MaxMP;

            MPRegen = newData.MPRegen;

            MaxStamina = newData.MaxStamina;

            StaminaRegen = newData.StaminaRegen;

            Attack = newData.Attack;

            Deffence = newData.Deffence;

            MovementSpeed = newData.MovementSpeed;

            AttackSpeed = newData.AttackSpeed;
        }



    }
}
