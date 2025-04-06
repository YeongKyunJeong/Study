using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Faction
{
    Player, Enemy, Neutrality, None
}

namespace RSP2
{
    public abstract class StatisticsForCharacter : IStatistics
    {
        public int MaxHP { get; set; }
        public int MaxMP { get; set; }
        public int Attack { get; set; }
        public int Deffence { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }

    }
}
