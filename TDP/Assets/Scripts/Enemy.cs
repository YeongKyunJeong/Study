using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 10f;

        private Transform target;
        private Vector3 beforeTargetPos;
        [SerializeField] private int wayPointsNumber = 0; // which waypoints  
        private int wayPointIndex = 0;
        private Vector3 dir;
        private float overDistance;
        public bool isAlive;

        //temp
        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            isAlive = true;
            target = WayPoints.GetPoint[0];
            transform.position = target.position;
        }

        private void Update()
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

        void GetNextWayPoiot()
        {
            wayPointIndex++;
            if (wayPointIndex >= WayPoints.GetPoint.Length)
            {
                PassGoal();
            }
            else
            {
                target = WayPoints.GetPoint[wayPointIndex];

            }
        }

        void PassGoal()
        {
            Debug.Log($"{gameObject.name} passed the goal");
        }
    }
}
