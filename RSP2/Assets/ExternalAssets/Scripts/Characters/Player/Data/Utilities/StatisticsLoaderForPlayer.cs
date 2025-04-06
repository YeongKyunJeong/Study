using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class StatisticsTableForPlayer
    {
        public int MaxHP;
        public int MaxMP;
        public int Attack;
        public int Deffence;
        public float MovementSpeed;
        public float AttackSpeed;

    }

    public class StatisticsLoaderForPlayer
    {
        public StatisticsTableForPlayer PlayerStatisticsTable { get; private set; }

        public StatisticsLoaderForPlayer(string path = "")
        {
            string loadedTableDataString;
            loadedTableDataString = Resources.Load<TextAsset>(path).text;
            PlayerStatisticsTable = JsonUtility.FromJson<StatisticsTableForPlayer>(loadedTableDataString);
        }

        public StatisticsTableForPlayer GetStatistics()
        {
            return PlayerStatisticsTable;
        }
    }

}
