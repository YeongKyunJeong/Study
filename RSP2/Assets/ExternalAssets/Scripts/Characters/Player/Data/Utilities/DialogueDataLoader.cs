using CsvHelper;
using CsvHelper.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace RSP2
{
    public enum DialogueType
    {
        None,
        NPC,
        Narration
    }

    public class DialogueData
    {
        public DialogueScript[] DialogueScripts;
        public int Length;
    }

    public struct DialogueScript
    {
        public int State;
        public bool IsRandom;
        public bool IsPlayerScript;
        public string Name;
        public int Redirection;
        public string Content;

        public int ButtonAction1;
        public string ButtonContent1;
        public int ButtonRedirection1;

        public int ButtonAction2;
        public string ButtonContent2;
        public int ButtonRedirection2;

        public int ButtonAction3;
        public string ButtonContent3;
        public int ButtonRedirection3;


    }

    public class DialogueDataLoader : MonoBehaviour
    {
        public DialogueDataLoader(DialogueType dialogueType, string name, string path = "CSV/Dialogue")
        {
            string loadedCSVDataString = string.Empty;
            switch (dialogueType)
            {
                case DialogueType.NPC:
                    {
                        loadedCSVDataString = Resources.Load<TextAsset>(string.Concat(path, "/NPC/", name)).text;
                        break;
                    }
            }

            using (var reader = new StringReader(loadedCSVDataString))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true,
                    IgnoreBlankLines = true,
                    TrimOptions = TrimOptions.Trim,
                    BadDataFound = null
                }))
                {
                    var records = csv.GetRecords<Dictionary<string, string>>();
                    DialogueData result = new DialogueData();
                    result.DialogueScripts = new DialogueScript[records.ToList().Count];
                    int ind = 0;

                    foreach (var row in records)
                    {
                        DialogueScript oneScript = new DialogueScript();

                        oneScript.State = int.Parse(row["State"]);
                        oneScript.IsRandom = int.Parse(row["Random"]) == 0 ? false : true;
                        oneScript.IsPlayerScript = int.Parse(row["Player"]) == 0 ? false : true;
                        oneScript.Redirection = string.IsNullOrWhiteSpace(row["Redirection"])
                            ? -1 
                            : int.TryParse(row["Redirection"], out int parsedValue)
                                ? parsedValue : -1;
                        oneScript.Name = string.IsNullOrWhiteSpace(row["Name"])
                            ? "?"
                            : row["Name"];
                        oneScript.Content = row["Content"];
                        // TO DO:: End Parse

                        ind++;
                    }

                    //7.Button content 1 ,2 ,3(string) : 버튼 위 내용
                    //8.Button function 1, 2, 3(int) : 버튼 기능(0: redirection, 1: 거래)
                    //9.Button redirection 1, 2, 3(int) : 해당 버튼 클릭 시 해당 state로 변경
                }


                //TableList = JsonUtility.FromJson<Wrapper>(loadedTableDataString).Items;
                //TableDict = new Dictionary<int, ExpDataTable>();
                //foreach (var item in TableList)
                //{
                //    TableDict.Add(item.key, item);
                //}
                //ExpDataTable = TableDict[1];
            }

        }
    }
}
