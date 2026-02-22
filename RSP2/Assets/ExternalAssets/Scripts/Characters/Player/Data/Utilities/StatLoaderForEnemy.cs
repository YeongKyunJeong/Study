using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class StatTableForEnemy
    {
        public int key;
        public string Name;
        public int Exp;
        public Faction Faction;
        public ChasingTargetType ChasingTargetType;
        public float SearchingDistance;
        public float MaxHP;
        public float HPRegen;
        public float MaxMP;
        public float MPRegen;
        public float MaxStamina;
        public float StaminaRegen;
        public float Attack;
        public float Defense;
        public float MovementSpeed;
        public float AttackSpeed;
    }

    public class StatLoaderForEnemy
    {

        public List<StatTableForEnemy> EnemyTableList { get; private set; }
        public Dictionary<int, StatTableForEnemy> EnemyTableDict { get; private set; }
        private StatTableForEnemy enemyStatTable { get; set; }

        public List<StatTableForEnemy> BossTableList { get; private set; }
        public Dictionary<int, StatTableForEnemy> BossTableDict { get; private set; }
        private StatTableForEnemy bossStatTable { get; set; }
        
        public StatLoaderForEnemy(string enemyPath = "JSON/Statistics/StatData_Enemy", string bossPath = "JSON/Statistics/StatData_Boss")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(enemyPath).text;
            EnemyTableList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
            EnemyTableDict = new Dictionary<int, StatTableForEnemy>();
            foreach (var item in EnemyTableList)
            {
                EnemyTableDict.Add(item.key, item);
            }
            enemyStatTable = EnemyTableDict[0];

            loadedTableDataString = Resources.Load<TextAsset>(bossPath).text;
            BossTableList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
            BossTableDict = new Dictionary<int, StatTableForEnemy>();
            foreach (var item in BossTableList)
            {
                BossTableDict.Add(item.key, item);
            }
            bossStatTable = BossTableDict[0];
        }

        [Serializable]
        private class Wrapper
        {
            public List<StatTableForEnemy> Items;
        }

        public StatTableForEnemy GetStat()
        {
            return enemyStatTable == null ? null : enemyStatTable;
        }

        public StatTableForEnemy GetByKey(int key, bool isBoss = false)
        {
            if (EnemyTableDict.ContainsKey(key))
            {
                if (isBoss) return BossTableDict[key];

                return EnemyTableDict[key];
            }
            return null;
        }

        public StatTableForEnemy GetByIndex(int index, bool isBoss = false)
        {
            if(isBoss)
            {
                if (index >= 0 && index < BossTableList.Count)
                {
                    return BossTableList[index];
                }
                return null;
            }

            if (index >= 0 && index < EnemyTableList.Count)
            {
                return EnemyTableList[index];
            }
            return null;
        }

    }
}