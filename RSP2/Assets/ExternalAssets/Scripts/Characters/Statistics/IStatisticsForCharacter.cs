using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum Faction
    {
        None, Player, Enemy, Neutrality
    }

    public interface IStatisticsForCharacter
    {
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
    }
}
