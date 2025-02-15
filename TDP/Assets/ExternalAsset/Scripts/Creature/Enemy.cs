using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class Enemy : MonoBehaviour, IPoolableObject
    {
        public float speed = 10f;

        private Transform target;
        private Vector3 beforeTargetPos;

        [SerializeField] private int wayPointsNumber = 0; // which waypoints
        [SerializeField] private Transform[] myWaypoints;

        private int wayPointIndex = 0;
        private Vector3 dir;
        private float overDistance;
        public bool isAlive;

        //temp
        //private void Start()
        //{
        //    Initialize();
        //}

        public void FirstPoolingInitialize()
        {
            EachPoolingInitialize();
        }

        public void EachPoolingInitialize()
        {
            gameObject.SetActive(true);
            wayPointIndex = 0;  // Departure waypoint
        }

        public void SetEnemy(int newWaypointsNumber, float newSpeed)  // Run by game logic class with enemy data reading
        {
            speed = newSpeed;
            wayPointsNumber = newWaypointsNumber;
            myWaypoints = WayPoints.GetPoints(wayPointsNumber);

            isAlive = true;
            beforeTargetPos = myWaypoints[wayPointIndex].position;
            target = myWaypoints[++wayPointIndex];  // First waypoint after departure
            transform.position = beforeTargetPos;
        }

        private void Update()
        {
            if (isAlive)
            {
                dir = target.position - transform.position;

                dir = dir - new Vector3(0, dir.y, 0);
                overDistance = dir.magnitude - speed * Time.deltaTime;
                if (overDistance < 0) // Should go over target position in this frame
                {
                    beforeTargetPos = target.position;

                    GetNextWayPoiot();

                    transform.position = (target.position - beforeTargetPos).normalized * overDistance + beforeTargetPos;
                }
                else
                {
                    transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
                }

            }
        }

        void GetNextWayPoiot()
        {
            wayPointIndex++;
            if (wayPointIndex >= myWaypoints.Length)
            {
                PassGoal();
            }
            else
            {
                target = myWaypoints[wayPointIndex];

            }
        }

        void PassGoal()
        {
            gameObject.SetActive(false);
        }

    }
}
