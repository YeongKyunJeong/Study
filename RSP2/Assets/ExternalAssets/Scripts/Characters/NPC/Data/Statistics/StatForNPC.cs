using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatForNPC : StatForEnemy
    {
        protected bool Tradable = false;

        public StatForNPC()
        {
            key = -1;

            Name = "Default NPC";

            Exp = 300;

            Faction = Faction.Enemy;

            ChasingTargetTpye = ChasingTargetTpye.NotMyFaction;

            SearchingDistance = 4;

            MaxHP = 50;

            HPRegen = 1;

            MaxMP = 20;

            MPRegen = 1;

            MaxStamina = 20;

            StaminaRegen = 5;

            Attack = 3;

            Defence = 3;

            MovementSpeed = 4;

            AttackSpeed = 5;

            Tradable = false;

            return;
        }

        public StatForNPC(StatTableForNPC baseStatisticsTable)
        {
            SetStatisticsByTable(baseStatisticsTable);
        }

        public StatForNPC(StatForNPC baseStatistics)
        {
            SetStatistics(baseStatistics);
        }

        public void SetStatisticsByTable(StatTableForNPC newDataTable)
        {
            key = newDataTable.key;

            Name = newDataTable.Name;

            Exp = newDataTable.Exp;

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

            Defence = newDataTable.Deffence;

            MovementSpeed = newDataTable.MovementSpeed;

            AttackSpeed = newDataTable.AttackSpeed;
            
            Tradable = newDataTable.Tradable;
        }

        private void SetStatistics(StatForNPC newData)
        {
            key = newData.key;

            Name = newData.Name;

            Exp = newData.Exp;

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

            Defence = newData.Defence;

            MovementSpeed = newData.MovementSpeed;

            AttackSpeed = newData.AttackSpeed;

            Tradable = newData.Tradable;
        }
    }
}
