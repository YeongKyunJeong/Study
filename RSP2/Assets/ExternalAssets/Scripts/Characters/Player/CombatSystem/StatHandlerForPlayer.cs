using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatHandlerForPlayer : StatHandlerForCharacter
    {
        public StatForPlayer PlayerBaseStatistics { get; private set; }
        public StatForPlayer PlayerCurrentStatistics { get; private set; }

        public ExpDataTable ExpDataTable { get; private set; }

        public int CurrentLevel { get; private set; }
        public int CurrentExp { get; private set; }

        private int maxLevel;

        public void Initialize(BaseStatTableForPlayer baseStatisticsTable)
        {
            BaseStatTableForPlayer OriginalLoadedDataTable = baseStatisticsTable;
            PlayerBaseStatistics = new StatForPlayer(baseStatisticsTable);
            PlayerCurrentStatistics = new StatForPlayer(baseStatisticsTable);

            SetStatAndCombatSystem();
        }

        public void Initialize(StatForPlayer initialStatistics)
        {
            PlayerBaseStatistics = new StatForPlayer(initialStatistics);
            PlayerCurrentStatistics = new StatForPlayer(initialStatistics);

            SetStatAndCombatSystem();

        }

        private void SetStatAndCombatSystem()
        {
            gameManager = GameManager.Instance;
            dataManager = DataManager.Instance;

            LevelStatTable levelStatTable = dataManager.TableDataLoader.LevelStatLoaderForPlayer.GetStatByKey(PlayerBaseStatistics.Level);
            PlayerBaseStatistics.SetStatByLevelTable(levelStatTable);
            PlayerCurrentStatistics.SetStatByLevelTable(levelStatTable);

            maxLevel = dataManager.TableDataLoader.LevelStatLoaderForPlayer.MaxLevel;


            ExpDataTable = dataManager.TableDataLoader.ExpDataLoader.GetExpByKey(PlayerBaseStatistics.Level);

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            CalculateFinalStat();
        }
        public bool GainExperience(int amount)
        {
            CurrentExp += amount;

            if (CurrentExp >= ExpDataTable.TotalExp)
            {
                //LevelUp();
                //healthSystem.InitHealth();
                return true;
            }

            return false;
        }

        protected override void CalculateFinalStat()
        {
            BaseStat = PlayerBaseStatistics as StatForCharacter;
            CurrentStatistics = PlayerCurrentStatistics as StatForCharacter;
            base.CalculateFinalStat();
        }

        //private void LevelUp()
        //{
        //    if (currentLevel >= maxLevel)
        //    {
        //        Debug.Log("Max level reached. No more leveling up.");
        //        return;
        //    }

        //    currentLevel++;

        //    ExperienceDatas nextLevelData = dataManager.ExperienceDatasLoader.GetByKey(currentLevel + characterData.minLevel - 1);
        //    if (nextLevelData != null)
        //    {
        //        experienceData = nextLevelData;
        //        PlayerStatDatas playerStatData = dataManager.PlayerStatDatasLoader.GetByKey(experienceData.statID);
        //        CharacterStat characterStat = new CharacterStat();
        //        characterStat.Initialize(playerStatData.baseHealth, playerStatData.baseSpeed, playerStatData.baseAttack);

        //        base.Initialize(characterStat);
        //    }
        //    else
        //    {
        //        Debug.Log("No experience data for the next level");
        //    }

        //}
    }
}
