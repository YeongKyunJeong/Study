using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class LevelStatTableForPlayer // To do : Use TableLoaderTarget class as parent to other target
    {
        public int key;
        public int Level;
        public float MaxHP;
        public float HPRegen;
        public float MaxMP;
        public float MPRegen;
        public float MaxStamina;
        public float StaminaRegen;
        public float Attack;
        public float Deffence;
    }
    public class LevelStatLoaderForPlayer
    {
        public List<LevelStatTableForPlayer> TableList { get; private set; }
        public Dictionary<int, LevelStatTableForPlayer> TableDict { get; private set; }
        private LevelStatTableForPlayer levelStatTable { get; set; }

        public LevelStatLoaderForPlayer(string path = "JSON/StatData_Player")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(path).text;
            TableList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
            TableDict = new Dictionary<int, LevelStatTableForPlayer>();
            foreach (var item in TableList)
            {
                TableDict.Add(item.key, item);
            }
            levelStatTable = TableDict[1];
        }

        [Serializable]
        private class Wrapper
        {
            public List<LevelStatTableForPlayer> Items;
        }

        public LevelStatTableForPlayer GetStat()
        {
            return levelStatTable == null ? null : levelStatTable;
        }
    }
}
