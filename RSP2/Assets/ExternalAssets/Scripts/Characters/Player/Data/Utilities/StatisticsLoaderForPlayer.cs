using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class StatisticsTableForPlayer // To do : Use TableLoaderTarget class as parent to other target
    {
        public Faction Faction;
        public float MaxHP;
        public float HPRegen;
        public float MaxMP;
        public float MPRegen;
        public float MaxStamina;
        public float StaminaRegen;
        public int Attack;
        public int Deffence;
        public float MovementSpeed;
        public float AttackSpeed;

    }

    public class StatisticsLoaderForPlayer
    {
        private StatisticsTableForPlayer playerStatisticsTable { get; set; }

        public StatisticsLoaderForPlayer(string path = "")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(path).text;
            playerStatisticsTable = JsonUtility.FromJson<StatisticsTableForPlayer>(loadedTableDataString);
        }

        public StatisticsTableForPlayer GetStatistics()
        {
            return playerStatisticsTable;
        }
    }

}
