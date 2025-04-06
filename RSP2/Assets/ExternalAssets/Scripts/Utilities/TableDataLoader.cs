using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class TableDataLoader
    {
        public StatisticsLoaderForPlayer StatisticsLoaderForPlayer { get; private set; }

        public void Initialize()
        {
            StatisticsLoaderForPlayer = new StatisticsLoaderForPlayer();
        }
    }


}
