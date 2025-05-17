using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class StatHandlerForPlayer : StatHandlerForCharacter
    {
        public CombatSystemForPlayer combatSystemForPlayer;
        public StatForPlayer PlayerBaseStatistics { get; private set; }
        public StatForPlayer PlayerCurrentStatistics { get; private set; }

        public ExpDataTable NowLevelExpData { get; private set; }

        public int CurrentLevel { get; private set; }
        public int CurrentExp { get; private set; }

        private int maxLevel;

        public void Initialize(BaseStatTableForPlayer baseStatisticsTable)
        {
            BaseStatTableForPlayer OriginalLoadedDataTable = baseStatisticsTable;
            PlayerBaseStatistics = new StatForPlayer(baseStatisticsTable);
            PlayerCurrentStatistics = new StatForPlayer(baseStatisticsTable);
            combatSystemForPlayer = GetComponent<CombatSystemForPlayer>();

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

            CurrentLevel = PlayerBaseStatistics.Level;

            LevelStatTable levelStatTable = dataManager.TableDataLoader.LevelStatLoaderForPlayer.GetStatByKey(CurrentLevel);
            PlayerBaseStatistics.SetStatByLevelTable(levelStatTable);
            PlayerCurrentStatistics.SetStatByLevelTable(levelStatTable);

            maxLevel = dataManager.TableDataLoader.LevelStatLoaderForPlayer.MaxLevel;

            NowLevelExpData = dataManager.TableDataLoader.ExpDataLoader.GetExpByKey(PlayerBaseStatistics.Level);
            CurrentExp = NowLevelExpData.TotalExp - NowLevelExpData.RequiredExp;

            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            gameManager.EnemyDieEvent += OnEnemyDie;

            CalculateFinalStat();
        }

        public void OnEnemyDie(Enemy enemy)
        {
            GainExperience(enemy.CombatSystem.GainExp);
        }

        public bool GainExperience(int amount)
        {
            CurrentExp += amount;
            bool isLevelUp = false;

            int limit = 0;

            while (CurrentExp >= NowLevelExpData.TotalExp)
            {
                LevelUp();
                isLevelUp = true;
                limit++;
                if (limit >= 100) break;
                if (CurrentExp < NowLevelExpData.TotalExp) break;
            }

            if (isLevelUp)
            {
                return true;
            }

            Debug.Log($"Player got {amount} Exp");
            return false;
        }

        protected override void CalculateFinalStat()
        {
            BaseStat = PlayerBaseStatistics as StatForCharacter;
            CurrentStatistics = PlayerCurrentStatistics as StatForCharacter;
            base.CalculateFinalStat();
        }

        private void LevelUp()
        {
            if (CurrentLevel >= maxLevel)
            {
                Debug.Log("Max level reached. No more leveling up.");
                return;
            }

            CurrentLevel++;
            Debug.Log($"Player bacame Level {CurrentLevel}");

            ExpDataTable nextLevelExpData = dataManager.TableDataLoader.ExpDataLoader.GetExpByKey(CurrentLevel);
            if (nextLevelExpData != null)
            {
                NowLevelExpData = nextLevelExpData;
                LevelStatTable levelStatTable = dataManager.TableDataLoader.LevelStatLoaderForPlayer.GetStatByKey(CurrentLevel);
                combatSystemForPlayer.ChangeStatByLevelUp(levelStatTable);
                PlayerBaseStatistics.SetStatByLevelTable(levelStatTable);
                PlayerCurrentStatistics.SetStatByLevelTable(levelStatTable);
            }
        }
    }
}
