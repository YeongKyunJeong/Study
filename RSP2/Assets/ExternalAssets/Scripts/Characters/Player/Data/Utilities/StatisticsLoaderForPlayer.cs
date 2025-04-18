using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class StatisticsTableForPlayer // To do : Use TableLoaderTarget class as parent to other target
    {
        public int key;
        public string Name;
        public Faction Faction;
        public float MaxHP;
        public float HPRegen;
        public float MaxMP;
        public float MPRegen;
        public float MaxStamina;
        public float StaminaRegen;
        public float Attack;
        public float Deffence;
        public float MovementSpeed;
        public float AttackSpeed;

    }

    public class StatisticsLoaderForPlayer
    {
        public List<StatisticsTableForPlayer> ItemsList { get; private set; }
        public Dictionary<int, StatisticsTableForPlayer> ItemsDict { get; private set; }
        private StatisticsTableForPlayer playerStatisticsTable { get; set; }

        public StatisticsLoaderForPlayer(string path = "JSON/StatisticsData_Player")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(path).text;
            ItemsList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
            ItemsDict = new Dictionary<int, StatisticsTableForPlayer>();
            //playerStatisticsTable = JsonUtility.FromJson<StatisticsTableForPlayer>(loadedTableDataString);
            foreach (var item in ItemsList)
            {
                ItemsDict.Add(item.key, item);
            }
            playerStatisticsTable = ItemsDict[1];
        }

        [Serializable]
        private class Wrapper
        {
            public List<StatisticsTableForPlayer> Items;
        }

        public StatisticsTableForPlayer GetStatistics()
        {
            return playerStatisticsTable == null? null : playerStatisticsTable;
        }
    }

}
