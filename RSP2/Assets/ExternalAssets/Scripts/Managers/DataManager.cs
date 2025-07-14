using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DataManager : MonoSingleton<DataManager>
    {
        public TableDataLoader TableDataLoader { get; private set; }

        public SaveDataWriter SaveDataWriter { get; private set; }

        public SaveDataLoader SaveDataLoader { get; private set; }

        public void Initialize()
        {
            TableDataLoader = new TableDataLoader();
            TableDataLoader.Initialize();

            SaveDataWriter = new SaveDataWriter();
            SaveDataLoader = new SaveDataLoader();
        }

        public void CallSave()
        {
            SaveDataWriter.SavePlayerDataToJson();
        }

        public PlayerSaveData CallLoad(int saveNumber)
        {
            return SaveDataLoader.LoadSaveData(saveNumber);
        }
    }


}
