using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public TableDataLoader TableDataLoader { get; private set; }

        public SaveDataWriter SaveDataWriter { get; private set; }

        public void Initialize()
        {
            TableDataLoader = new TableDataLoader();
            TableDataLoader.Initialize();

            SaveDataWriter = new SaveDataWriter();
        }

        public void CallSave()
        {
            SaveDataWriter.SavePlayerDataToJson();
        }
    }


}
