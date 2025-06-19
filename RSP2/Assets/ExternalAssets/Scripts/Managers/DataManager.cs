using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public TableDataLoader TableDataLoader { get; private set; }
        public CSVDataLoader CSVDataLoader { get; private set; }

        public void Initialize()
        {
            TableDataLoader = new TableDataLoader();
            //CSVDataLoader = new CSVDataLoader();

            TableDataLoader.Initialize();
            //CSVDataLoader.Initialize();
        }
    }


}
