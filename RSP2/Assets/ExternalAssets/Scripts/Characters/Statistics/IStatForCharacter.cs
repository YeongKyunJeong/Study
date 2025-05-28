using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [Flags]
    public enum Faction
    {
        Null = 0,   // Not assigned yet
        Player = 1,
        Enemy = 2,
        Neutral = 4,
        None = 8
    }

    public interface IStatForCharacter
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
        public float Defence { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }
    }
}
