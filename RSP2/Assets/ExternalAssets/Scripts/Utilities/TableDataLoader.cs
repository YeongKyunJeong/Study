using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class TableDataLoader
    {
        public BaseStatLoaderForPlayer BaseStatLoaderForPlayer { get; private set; }
        public StatLoaderForEnemy StatLoaderForEnemy { get; private set; } 

        public void Initialize()
        {
            BaseStatLoaderForPlayer = new BaseStatLoaderForPlayer();
            StatLoaderForEnemy = new StatLoaderForEnemy();
        }
    }


}
