using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RSP2
{
    public enum ChasingTargetTpye
    {
        PlayerOnly,
        AllFaction,
        NotMyFaction
    }

    //public enum EnemyBasicAttack
    //{
    //    Melee,
    //    Ranged
    //}

    public class StatForEnemy : StatForCharacter
    {
        public int Exp;

        public ChasingTargetTpye ChasingTargetTpye { get; set; }
        public float SearchingDistance { get; set; }

        public StatForEnemy()
        {
            key = -1;

            Name = "Default";

            Exp = 300;

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

            Defence = 3;

            MovementSpeed = 4;

            AttackSpeed = 5;

            return;
        }

        public StatForEnemy(StatTableForEnemy baseStatisticsTable)
        {
            //Debug.Log("Loaded by JSON : Enemy");
            SetStatisticsByTable(baseStatisticsTable);
        }

        public StatForEnemy(StatForEnemy baseStatistics)
        {
            SetStatistics(baseStatistics);
        }

        public void SetStatisticsByTable(StatTableForEnemy newDataTable)
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
        }

        private void SetStatistics(StatForEnemy newData)
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
        }
    }
}
