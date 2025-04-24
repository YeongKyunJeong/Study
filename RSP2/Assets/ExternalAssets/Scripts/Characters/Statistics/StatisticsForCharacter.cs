using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RSP2
{
    public class StatisticsForCharacter : IStatisticsForCharacter
    {
        public int key { get; set; }
        public string Name { get; set; }
        public Faction Faction { get; set; }
        public float MaxHP { get; set; }
        public float HPRegen { get; set; }
        public float MaxMP { get; set; }
        public float MPRegen { get; set; }
        public float MaxStamina { get; set; }
        public float StaminaRegen { get; set; }
        public float Attack { get; set; }
        public float Deffence { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }

        public StatisticsForCharacter()
        {
            //Debug.Log("Initialized Without Data");
        }

        public void InitializeByDefault()
        {


                key = -1;

                Name = "Default";

                Faction = Faction.Enemy;

                MaxHP = 50;

                HPRegen = 1;

                MaxMP = 20;

                MPRegen = 1;

                MaxStamina = 20;

                StaminaRegen = 5;

                Attack = 3;

                Deffence = 3;

                MovementSpeed = 8;

                AttackSpeed = 5;
                return;

            
        }

        public StatisticsForCharacter(StatisticsForCharacter baseStatistics)
        {
            SetStatistics(baseStatistics);
        }

        private void SetStatistics(StatisticsForCharacter newData)
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
