using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace RSP2
{
    public class NPCandEnemyManager : MonoBehaviour
    {
        private InGameManager gameManager;
        private HashSet<NPC> nPCs;
        public HashSet<NPC> NPCs
        {
            get
            {
                if (nPCs == null) nPCs = new HashSet<NPC>();
                return nPCs;
            }
        }
        private Queue<NPC> nPCQueue;
        public Queue<NPC> NPCQueue
        {
            get
            {
                if (nPCQueue == null) nPCQueue = new Queue<NPC>();
                return nPCQueue;
            }
        }
        private HashSet<Enemy> enemies;
        public HashSet<Enemy> Enemies
        {
            get
            {
                if (enemies == null) enemies = new HashSet<Enemy>();
                return enemies;
            }
        }
        private Queue<Enemy> enemyQueue;
        public Queue<Enemy> EnemyQueue
        {
            get
            {
                if (enemyQueue == null) enemyQueue = new Queue<Enemy>();
                return enemyQueue;
            }
        }

        private Coroutine enrollCoroutine;

        public void Initialize(InGameManager _gameManager)
        {
            gameManager = _gameManager;

        }

        public void EnrollNPC(NPC nPC)
        {
            NPCQueue.Enqueue(nPC);
            NPCs.Add(nPC);
            //if (gameManager.InitializeDone)
            //{
            //    nPC.Initialize(this);
            //}
            //else
            //{
            //    WaitForInitialize(nPC);
            //}
        }

        public void EnrollEnemy(Enemy enemy)
        {
            Enemies.Add(enemy);
            EnemyQueue.Enqueue(enemy);
            //if (gameManager.InitializeDone)
            //{
            //    enemy.Initialize(this);
            //}
            //else
            //{
            //    WaitForInitialize(enemy);
            //}
        }

        public void RemoveEnemy(Enemy enemy)
        {
            Enemies.Remove(enemy);
        }

        public void CallUpdate()
        {
            while(NPCQueue.Any())
            {
                NPC npc = NPCQueue.Dequeue();
                npc.Initialize(this);
            }

            while(EnemyQueue.Any())
            {
                Enemy enemy = EnemyQueue.Dequeue();
                enemy.Initialize(this);
            }

            foreach (NPC nPC in NPCs)
            {
                nPC.CallUpdate();
            }

            foreach (Enemy enemy in Enemies)
            {
                enemy.CallUpdate();
            }
        }

        private void WaitForInitialize(Enemy enemy)
        {
            enemyQueue.Enqueue(enemy);
            if (enrollCoroutine == null)
            {
                enrollCoroutine = StartCoroutine(EnrollmentWaitingCoroutine());
            }
        }

        private void WaitForInitialize(NPC nPC)
        {
            nPCQueue.Enqueue(nPC);
            if (enrollCoroutine == null)
            {
                enrollCoroutine = StartCoroutine(EnrollmentWaitingCoroutine());
            }
        }

        private IEnumerator EnrollmentWaitingCoroutine()
        {
            while (!gameManager.IsInitialized) yield return null;

            while(nPCQueue.Any() || enemyQueue.Any())
            {
                if(nPCQueue.Any())
                {
                    nPCQueue.Dequeue().Initialize(this);
                }

                if(enemyQueue.Any())
                {
                    enemyQueue.Dequeue().Initialize(this);
                }
            }

            enrollCoroutine = null;
        }
    }
}
