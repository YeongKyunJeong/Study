using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RSP2
{
    public class StatHandlerForEnemy : StatHandlerForCharacter
    {

        public StatForEnemy EnemyBaseStatistics;
        public StatForEnemy EnemyCurrentStatistics;

        private Enemy enemy;
        private RuntimeDataForEnemy runtimeData;
        private Coroutine attackCoolTimeCoroutine;

        public void Initialize(Enemy _enemy, StatTableForEnemy baseStatisticsTable)
        {
            enemy = _enemy;
            runtimeData = _enemy.RuntimeData;
            //StatTableForEnemy OriginalLoadedDataTable = baseStatisticsTable;
            EnemyBaseStatistics = new StatForEnemy(baseStatisticsTable);
            EnemyCurrentStatistics = new StatForEnemy(baseStatisticsTable);

            SetRuntimeDataAndCombatSystem();
        }


        public void Initialize(NPC nPC, StatTableForNPC baseStatisticsTable)
        {
            enemy = nPC as Enemy;
            runtimeData = enemy.RuntimeData;
            //StatTableForNPC OriginalLoadedDataTable = baseStatisticsTable;
            EnemyBaseStatistics = new StatForNPC(baseStatisticsTable);
            EnemyCurrentStatistics = new StatForNPC(baseStatisticsTable);

            nPC.Name = EnemyBaseStatistics.Name;
            nPC.DialogueKey = baseStatisticsTable.DialogueStartKey;
            bool[] interactions = new bool[2] { false, false };

            if (baseStatisticsTable.HasDialogue)
            {
                nPC.HasDialogue = true;
                interactions[0] = true;

            }
            if (baseStatisticsTable.Tradable) interactions[1] = true;
            nPC.InteractionHitBox.Initialize(nPC, interactions);

            SetRuntimeDataAndCombatSystem(true);
        }

        public void Initialize(Enemy _enemy, StatForEnemy initialStatistics)
        {
            enemy = _enemy;
            runtimeData = _enemy.RuntimeData;
            EnemyBaseStatistics = new StatForEnemy(initialStatistics);
            EnemyCurrentStatistics = new StatForEnemy(initialStatistics);

            SetRuntimeDataAndCombatSystem();
        }

        private void SetRuntimeDataAndCombatSystem(bool isNPC = false)
        {
            if (combatSystem == null)
            {
                combatSystem = GetComponent<CombatSystem>();
            }

            combatSystem.MyFaction = CurrentStatistics.Faction;

            runtimeData.ChasingTargetType = EnemyCurrentStatistics.ChasingTargetType;

            if (isNPC)
            {
                runtimeData.IsHostile = false;
            }
            else
            {
                runtimeData.IsHostile = true;
            }

            runtimeData.SearchingDistance = EnemyCurrentStatistics.SearchingDistance;
            runtimeData.SearchingDistanceSqr = EnemyCurrentStatistics.SearchingDistance * EnemyCurrentStatistics.SearchingDistance;

            //SetAttackRange(enemy.AttackHitBox.GetNowColliderAttackRange);
            SetAttackRange();

            CalculateFinalStat();
        }

        //public void SetAttackRange(float range)
        //{
        //    runtimeData.AttackRange = range;
        //    runtimeData.AttackRangeSqr = range * range;

        //    runtimeData.MinChasingDistance = runtimeData.AttackRange / 2;
        //    runtimeData.MinChasingDistanceSqr = runtimeData.AttackRangeSqr / 4;
        //}

        public void SetAttackRange()
        {
            int count = enemy.AttackDataArray.Length;
            float[] ranges = new float[count];

            for (int i = 0; i < count; i++)
            {
                ranges[i] = (enemy.AttackDataArray[i].ColliderSize.z + enemy.AttackDataArray[i].ColliderPosition.z);
                runtimeData.AttackRanges[i] = ranges[i];
                runtimeData.AttackRangeSqrs[i] = ranges[i] * ranges[i];
                runtimeData.AttackAngles[i] = Mathf.Atan2(enemy.AttackDataArray[i].ColliderSize.x, ranges[i]) * Mathf.Rad2Deg;
            }

            runtimeData.MinChasingDistanceSqr = runtimeData.AttackRangeSqrs.Max() * 0.9f;
            runtimeData.MinChasingDistance = runtimeData.AttackRanges.Max() * 0.81f;

        }

        protected override void CalculateFinalStat()
        {
            BaseStat = EnemyBaseStatistics as StatForCharacter;
            CurrentStatistics = EnemyCurrentStatistics as StatForCharacter;
            base.CalculateFinalStat();
        }

        //public bool StartAttackCoroutine(float coolTime, bool ignoreBeforeCoolTime = false)
        //{
        //    if (attackCoolTimeCoroutine != null)
        //    {
        //        if(!ignoreBeforeCoolTime && runtimeData.AttackCoolTime > 0)
        //        {
        //            return false;
        //        }

        //        StopCoroutine(attackCoolTimeCoroutine);
        //    }

        //    attackCoolTimeCoroutine = StartCoroutine(AttackCoolTimeStart(coolTime));
        //    return true;
        //}

        //private IEnumerator AttackCoolTimeStart(float coolTime)
        //{
        //    if (coolTime <= 0) yield return null;

        //    runtimeData.IsAttackReady = false;
        //    yield return new WaitForSeconds(coolTime);

        //    runtimeData.IsAttackReady = true;
        //    attackCoolTimeCoroutine = null;
        //    yield return null;
        //}
    }
}
