using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TDP
{
    public class Enemy : MonoBehaviour, IPoolableObject
    {
        public float initialSpeed = 10f;

        private float speed;
        public float GetSpeed { get => speed; }
        //public bool isSlowed = false;

        public float totalHealth = 10;
        public float health = 10;

        public int dropMoney = 50;

        [SerializeField] private GameObject deathEffect;
        [SerializeField] private EnemyMovement enemyMovement;

        [Header("Unity Stuff")]
        [SerializeField] private Image healthBar;

        //[SerializeField] private int wayPointsNumber = 0; // which waypoints
        //[SerializeField] private Transform[] myWaypoints;

        //private int wayPointIndex = 0;
        //private Vector3 dir;

        public bool isAlive;

        //temp
        //private void Start()
        //{
        //    Initialize();
        //}

        public void FirstPoolingInitialize()
        {
            if (enemyMovement == null)
            {
                Debug.Log("Enemy Movement not assigned");
                enemyMovement = GetComponent<EnemyMovement>();
            }
            enemyMovement.Initialize(this);
            if (healthBar == null)
            {
                Debug.Log("Health Bar not assigned");
            }
            EachPoolingInitialize();
        }

        public void EachPoolingInitialize()
        {
            gameObject.SetActive(true);
        }

        public void SetEnemy(int newWaypointsNumber, float newSpeed, int newHealth, int newDropMoney)  // Run by game logic class with enemy data reading
        {
            initialSpeed = newSpeed;
            speed = initialSpeed;
            //isSlowed = false;
            isAlive = true;
            enemyMovement.SetWaypointData(0);
            enemyMovement.SetInitialSpeed(initialSpeed);

            //wayPointsNumber = newWaypointsNumber;
            //myWaypoints = WayPoints.GetPoints(wayPointsNumber);
            totalHealth = newHealth;
            health = newHealth;
            healthBar.fillAmount = health / totalHealth;
            dropMoney = newDropMoney;

            //beforeTargetPos = myWaypoints[wayPointIndex].position;
            //target = myWaypoints[++wayPointIndex];  // First waypoint after departure
            //transform.position = beforeTargetPos;
        }

        //private void Update()
        //{
        //    if (isAlive)
        //    {
        //        dir = target.position - transform.position;

        //        dir = dir - new Vector3(0, dir.y, 0);
        //        overDistance = dir.magnitude - speed * Time.deltaTime;
        //        if (overDistance < 0) // Should go over target position in this frame
        //        {
        //            beforeTargetPos = target.position;

        //            GetNextWayPoiot();

        //            transform.position = (target.position - beforeTargetPos).normalized * overDistance + beforeTargetPos;
        //        }
        //        else
        //        {
        //            transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        //        }

        //    }
        //}

        public void TakeDamage(float damage)
        {
            if (health > 0)
            {
                health -= damage;
                healthBar.fillAmount = health / totalHealth;
                if (health <= 0)
                {
                    Die();
                }
            }
        }

        public void Slow(float slowAmount)
        {
            speed = initialSpeed * (1f - slowAmount);
            //if (isSlowed)
            //{
            //    // To do : Add slow stack logic
            //}
            //else
            //{
            //    isSlowed = true;
            //    speed = startSpeed * (1f - slowAmount);
            //    enemyMovement.SetSpeed(speed);
            //}
        }

        private void Die()
        {
            Destroy(Instantiate(deathEffect, transform.position, Quaternion.identity), 2.5f);
            PlayerStats.Money += dropMoney;
            isAlive = false;
            enemyMovement.isAlive = false;
            gameObject.SetActive(false);
        }

        public void RollBackSpeedCall()
        {
            speed = initialSpeed;
        }

        //void GetNextWayPoiot()
        //{
        //    wayPointIndex++;
        //    if (wayPointIndex >= myWaypoints.Length)
        //    {
        //        PassGoal();
        //    }
        //    else
        //    {
        //        target = myWaypoints[wayPointIndex];

        //    }
        //}

        //void PassGoal()
        //{
        //    if (PlayerStats.Life > 0)
        //    {
        //        PlayerStats.Life--;
        //    }
        //    gameObject.SetActive(false);
        //}

    }
}
