using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public class StatisticsForEnemy : StatisticsForCharacter
    {
        public ChasingTargetTpye ChasingTargetTpye { get; set; }
        public float SearchingDistance { get; set; }

        public StatisticsForEnemy()
        {
            key = -1;

            Name = "Default";

            Faction = Faction.Enemy;

            ChasingTargetTpye = ChasingTargetTpye.PlayerOnly;

            SearchingDistance = 4;

            MaxHP = 50;

            HPRegen = 1;

            MaxMP = 20;

            MPRegen = 1;

            MaxStamina = 20;

            StaminaRegen = 5;

            Attack = 3;

            Deffence = 3;

            MovementSpeed = 4;

            AttackSpeed = 5;
            return;
        }

        public StatisticsForEnemy(StatisticsTableForEnemy baseStatisticsTable)
        {
            Debug.Log("Loaded by JSON : Enemy");
            SetStatisticsByTable(baseStatisticsTable);
        }

        public StatisticsForEnemy(StatisticsForEnemy baseStatistics)
        {
            SetStatistics(baseStatistics);
        }

        public void SetStatisticsByTable(StatisticsTableForEnemy newDataTable)
        {
            key = newDataTable.key;

            Name = newDataTable.Name;

            Faction = newDataTable.Faction;

            ChasingTargetTpye = newDataTable.ChasingTargetTpye;

            SearchingDistance = newDataTable.SearchingDistance;

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

        private void SetStatistics(StatisticsForEnemy newData)
        {
            key = newData.key;

            Name = newData.Name;

            Faction = newData.Faction;

            ChasingTargetTpye = newData.ChasingTargetTpye;

            SearchingDistance = newData.SearchingDistance;

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
