using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RSP2
{
    public abstract class StatisticsForCharacter : IStatistics
    {
        public StatisticsTableForPlayer OriginalLoadedData { get; private set; }

        public Faction Faction { get; set; }
        public float MaxHP { get; set; }
        public float HPRegen { get; set; }
        public float MaxMP { get; set; }
        public float MPRegen { get; set; }
        public float MaxStamina { get; set; }
        public float StaminaRegen { get; set; }
        public int Attack { get; set; }
        public int Deffence { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }

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
