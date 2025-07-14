using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Windows;
using Directory = System.IO.Directory;
using File = System.IO.File;

namespace RSP2
{
    [System.Serializable]
    public class PlayerSaveData
    {
        [Header("Statistics")]
        public int Exp;
        public int Gold; // TO DO::
        public float HP;
        public float MP;
        public float Stamina;

        [Space]
        [Header("Progress Data")]
        public Vector3 Position;
        public int GameProgress;

        [Space]
        [Header("Inventory Data")]
        public List<ItemSaveData> InventoryData;

        [Space]
        [Header("Object Data")]
        public List<QuestSaveData> Quests;
        public List<NPCSaveData> NPCs;
    }

    [System.Serializable]
    public class ItemSaveData
    {
        public ItemType ItemType;
        public EquipmentType EquipmentType;
        public int ItemKey;
        public bool IsEquipped;
        public int SlotPosition;
        public int Amount;
    }

    [System.Serializable]
    public class QuestSaveData
    {
        public int QuestKey;
        public QuestStatus QuestStatus;
        public List<int> CurrentCounts;
    }

    [System.Serializable]
    public class NPCSaveData
    {
        public int NPCKey;
        public bool NPCStatus;
        public int NPCDialogueKey;
    }

    public class SaveDataWriter
    {
        public bool SavePlayerDataToJson(string path = "Json/Save")
        {
            PlayerSaveData saveData = GameManager.Instance.GetDataToSave();

            path = string.Concat(Application.persistentDataPath, "/", path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string jsonData = JsonUtility.ToJson(saveData, true);

            int index = 1;
            while (File.Exists(string.Concat(path, $"_{index:D3}.json")))
            {
                index++;

                if (index >= 1000)
                {
                    index = 999;
                    throw new InvalidOperationException("Save Failed : Save Data Is Full of");
                }
            }

            path = string.Concat(path, "/Save_", index.ToString("D3"));
            try
            {
                File.WriteAllText(path, jsonData);
                Debug.Log("File Was Saved At" + path);
            }
            catch (Exception e)
            {
                Debug.Log("Save Failed : " + e.Message);
            }

            return true;
        }
    }

    public class SaveDataLoader
    {
        private PlayerSaveData playerSaveData { get; set; }

        public PlayerSaveData LoadSaveData(int saveNumber, string path = "Json/Save")
        {
            if(saveNumber< 0)
            {





            }

            path = string.Concat(Application.persistentDataPath, "/", path, saveNumber.ToString());

            if (!File.Exists(path))
            {
                throw new InvalidOperationException("Save Failed : Save Data Not exists");
            }

            string loadedSaveDataString;
            loadedSaveDataString = File.ReadAllText(path);
            playerSaveData = JsonUtility.FromJson<PlayerSaveData>(loadedSaveDataString);

            return playerSaveData;
        }

        public PlayerSaveData GetSaveData()
        {
            return playerSaveData == null ? null : playerSaveData;
        }
    }

}
