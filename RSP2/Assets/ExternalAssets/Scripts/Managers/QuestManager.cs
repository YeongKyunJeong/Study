using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    public enum QuestStatus
    {
        Inactive,
        Active,
        Completed,
        Failed
    }
    public class QuestManager : MonoSingleton<QuestManager>
    {
        public List<QuestProgress> ActiveQuests = new List<QuestProgress>();

        public void StartQuest(QuestDataScriptableObject newQuestDataSO)
        {
            QuestProgress progress = new QuestProgress(newQuestDataSO);
            ActiveQuests.Add(progress);
            RegisterObjectives(progress);
        }
        private void RegisterObjectives(QuestProgress questProgress)
        {
            foreach (var obj in questProgress.ObjectiveProgresses)
            {
                switch (obj.ObjectiveData.Type)
                {
                    case ObjectiveType.CollectingItem:
                        {
                            //EventBus.OnItemCollected += id => OnObjectiveEvent(obj, id, questProgress);
                        }
                        break;
                    case ObjectiveType.HuntingEnemy:
                        {
                            //EventBus.OnMonsterKilled += id => OnObjectiveEvent(obj, id, questProgress);
                        }
                        break;
                    case ObjectiveType.TalkingToNPC:
                        {
                            //EventBus.OnNPCInteracted += id => OnObjectiveEvent(obj, id, questProgress);
                        }
                        break;
                    case ObjectiveType.ArrivingLocation:
                        {
                            break;
                        }
                    case ObjectiveType.AccessUI:
                        {
                            //EventBus.OnUIAccessed += name =>
                            //{
                            //    if (int.TryParse(name, out int id))
                            //        OnObjectiveEvent(obj, id, questProgress);
                            //};
                        }
                        break;
                }
            }
        }
    }
}
