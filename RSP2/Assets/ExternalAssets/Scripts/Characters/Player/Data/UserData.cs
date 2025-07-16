using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class UserData
    {
        public const int SAVE_DATA_LIMIT = 8;

        public int UserID;
        public string UserName;
        public List<int> SaveNumberList = new List<int>();

        public static UserData InitialUserData(int userID = 0, string userName = "You")
        {
            UserData userData = new UserData();

            userData.UserID = userID;
            userData.UserName = userName;
            userData.SaveNumberList.Add(0);

            return userData;
        }
    }

    public class UserDataWriter
    {
        private SaveDataWriter saveDataWriter;
        private UserDataLoader userDataLoader;

        public void Initialize(UserDataLoader _userDataLoader, SaveDataWriter _saveDataWriter)
        {
            userDataLoader = _userDataLoader;
            saveDataWriter = _saveDataWriter;
        }

        public void SaveCurrentUserData(string path = "/Json/UserData")
        {
            if (userDataLoader.CurrentUserData == null) return;

            SaveUserDataToJson(userDataLoader.CurrentUserData);
        }

        public bool SaveUserDataToJson(UserData userData, string path = "/Json/UserData")
        {
            path = string.Concat(Application.persistentDataPath, path);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = string.Concat(path, "/", userData.UserID.ToString());

            string jsonData = JsonUtility.ToJson(userData, true);
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

    public class UserDataLoader
    {
        private SaveDataLoader saveDataLoader;

        private UserData userData;
        public UserData CurrentUserData { get => userData; }
        private List<SaveDataLoader> saveDataList;

        public void Initialize(SaveDataLoader _saveDataLoader, int userID = 0)
        {
            saveDataLoader = _saveDataLoader;
            LoadUserData(userID);
            saveDataList = new List<SaveDataLoader>();
            foreach (int saveNumber in userData.SaveNumberList)
            {
                saveDataLoader.LoadSaveData(userData.UserName, saveNumber);
            }
        }

        public PlayerSaveData LoadLastSaveData(int userID, bool rememberUserData = true, string path = "/Json/UserData")
        {
            if ((userData != null) && (userData.UserID == userID))
                return saveDataLoader.LoadSaveData(userData.UserName, userData.SaveNumberList[userData.SaveNumberList.Count - 1]);

            UserData _userData = LoadUserData(userID, rememberUserData);

            return saveDataLoader.LoadSaveData(_userData.UserName, _userData.SaveNumberList[_userData.SaveNumberList.Count - 1]);
        }

        public UserData LoadUserData(int userID, bool rememberUserData = true, string path = "/Json/UserData")
        {
            if ((userData != null) && (userData.UserID == userID)) return userData;

            path = string.Concat(Application.persistentDataPath, path, "/", userID.ToString());

            if (!File.Exists(path))
            {
                Debug.Log("No User Data Exists");
                userData = UserData.InitialUserData();
                return userData;
            }

            string loadedUserDataString;
            loadedUserDataString = File.ReadAllText(path);

            if (rememberUserData)
            {
                userData = JsonUtility.FromJson<UserData>(loadedUserDataString);
                return userData;
            }

            return JsonUtility.FromJson<UserData>(loadedUserDataString);

        }
    }
}