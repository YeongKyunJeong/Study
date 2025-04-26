using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class StatisticsTableForEnemy
    {
        public int key;
        public string Name;
        public Faction Faction;
        public ChasingTargetTpye ChasingTargetTpye;
        public float SearchingDistance;
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
    public class StatisticsLoaderForEnemy
    {

        public List<StatisticsTableForEnemy> TableList { get; private set; }
        public Dictionary<int, StatisticsTableForEnemy> TableDict { get; private set; }
        private StatisticsTableForEnemy enemyStatisticsTable { get; set; }

        public StatisticsLoaderForEnemy(string path = "JSON/StatisticsData_Enemy")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(path).text;
            TableList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
            TableDict = new Dictionary<int, StatisticsTableForEnemy>();
            foreach (var item in TableList)
            {
                TableDict.Add(item.key, item);
            }
            enemyStatisticsTable = TableDict[2];
        }

        [Serializable]
        private class Wrapper
        {
            public List<StatisticsTableForEnemy> Items;
        }

        public StatisticsTableForEnemy GetStatistics()
        {
            return enemyStatisticsTable == null ? null : enemyStatisticsTable;
        }

        public StatisticsTableForEnemy GetByKey(int key)
        {
            if (TableDict.ContainsKey(key))
            {
                return TableDict[key];
            }
            return null;
        }
        public StatisticsTableForEnemy GetByIndex(int index)
        {
            if (index >= 0 && index < TableList.Count)
            {
                return TableList[index];
            }
            return null;
        }

    }
}