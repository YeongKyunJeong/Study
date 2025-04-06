using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public interface IStatistics
    {
        public int MaxHP { get; set; }
        public int Attack { get; set; }
        public int Deffence { get; set; }
        public float MovementSpeed { get; set; }
        public float AttackSpeed { get; set; }
    }
}
