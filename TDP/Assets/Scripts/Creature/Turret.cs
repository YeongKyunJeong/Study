using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class Turret : MonoBehaviour, IPoolableObject
    {
        // To do : Load ans set turret data from data file 
        public Transform target;
        private Collider[] detectedColldiers;
        private Coroutine detectingCoroutine;
        private float distanceToEnemy;
        private float closestDistance;
        private int closestDistIndex;
        private Vector3 dir;
        private Vector3 rotation;
        private Quaternion targetQuaternion;

        #region Turret Object Motion Control

        [SerializeField] private Transform rotatingPart;

        #endregion


        #region Turret Data
        public float range;
        private float attackInterval;
        private int damage;

        #endregion

        public float turnSpeed;
        private float detectionInterval;
        private LayerMask enemyLayerMask; // bitMask
        private WaitForSeconds detectionWaitForSec;
        private WaitForSeconds attackWaitForSec;

        // temp
        private void Start()
        {
            FirstPoolingInitialize();
            SetTurretData();
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }
            else
            {
                dir = target.position - transform.position;
                //rotation = Quaternion.LookRotation(dir).eulerAngles;
                targetQuaternion = Quaternion.LookRotation(dir);
                rotation = Quaternion.Lerp(rotatingPart.rotation, targetQuaternion, Time.deltaTime * turnSpeed).eulerAngles;
                rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }

        public void FirstPoolingInitialize()
        {
            turnSpeed = 10f;
            detectionInterval = 0.125f;
            enemyLayerMask = LayerMask.GetMask("Enemy");
            detectionWaitForSec = new WaitForSeconds(detectionInterval);
            EachPoolingInitialize();
        }

        public void EachPoolingInitialize()
        {
            gameObject.SetActive(true);

        }

        public void SetTurretData()
        {
            #region Assigning loaded data
            // range, attackInterval, etc
            attackInterval = 0.125f;
            range = 15f;
            damage = 3;
            #endregion

            attackWaitForSec = new WaitForSeconds(attackInterval);
            detectingCoroutine = null;
            detectingCoroutine = StartCoroutine(DetectEnemyCoroutine());
        }

        private void UpdateTarget()
        {
            detectedColldiers = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
            closestDistance = range;

            if (detectedColldiers.Length > 0)
            {
                for (int i = 0; i < detectedColldiers.Length; i++)
                {
                    distanceToEnemy = Vector3.Distance(detectedColldiers[i].transform.position, transform.position); // To do : Change distance from turret to distance to goal
                    if (distanceToEnemy < closestDistance)
                    {
                        closestDistance = distanceToEnemy;
                        closestDistIndex = i;
                    }
                }
                target = detectedColldiers[closestDistIndex].transform;
            }
            else
            {
                target = null;
            }
            return;
        }

        private IEnumerator DetectEnemyCoroutine()
        {
            while (true)
            {
                UpdateTarget();

                if (target == null)
                {
                    yield return detectionWaitForSec;
                }
                else
                {
                    yield return attackWaitForSec;
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

    }

}