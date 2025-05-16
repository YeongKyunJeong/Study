using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum Faction
    {
        Null,   // Not assigned yet
        Player,
        Enemy,
        Neutral,
        None
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
