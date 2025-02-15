using System;
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
        private Bullet bulletInitializer;

        #region Fixed Parameter
        private float detectionInterval;
        private LayerMask enemyLayerMask; // bitMask
        private WaitForSeconds detectionWaitForSec;
        private float turnSpeed;
        #endregion

        #region External Refference
        [SerializeField] private GameObject bulletPrefab; // To Do : Use objectpool
        [SerializeField] Transform firePoint;
        #endregion

        #region Turret Object Motion Control

        private float distanceToEnemy;
        private float closestDistance;
        private int closestDistIndex;
        [SerializeField] private Transform rotatingPart;
        private Vector3 dir;
        private Vector3 rotation;
        private Quaternion targetQuaternion;

        #endregion

        #region Turret Data
        [Header("Attributes")]
        [SerializeField] public float range;
        [SerializeField] private int damage;

        [SerializeField] private float fireRate;
        [SerializeField] private float fireCountDown;

        #endregion


        // temp
        private void Start()
        {
            FirstPoolingInitialize();
            SetTurretData();
        }
        public void FirstPoolingInitialize()
        {
            detectionInterval = 0.125f;
            enemyLayerMask = LayerMask.GetMask("Enemy");
            detectionWaitForSec = new WaitForSeconds(detectionInterval);
            turnSpeed = 10f;

            EachPoolingInitialize();
        }

        public void EachPoolingInitialize()
        {
            gameObject.SetActive(true);
            fireCountDown = 0;
        }

        public void SetTurretData()
        {
            #region Assigning loaded data
            // range, attackInterval, etc
            //range = 15f;
            //damage = 3;
            //fireRate = 2;
            #endregion

            detectingCoroutine = null;
            detectingCoroutine = StartCoroutine(DetectEnemyCoroutine());
        }

        private void Update()
        {
            if (target == null)
            {
                return;
            }
            RotateHead();

            if (fireCountDown <= 0f)
            {
                Shoot();

                fireCountDown = 1f / fireRate;  // Countdown Initialize
            }

            fireCountDown -= Time.deltaTime;
        }


        private void Shoot()
        {
            bulletInitializer = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation).GetComponent<Bullet>();

            if (bulletInitializer != null)
            {
                bulletInitializer.Seek(target);
            }
        }

        private void RotateHead()
        {
            dir = target.position - transform.position;
            //rotation = Quaternion.LookRotation(dir).eulerAngles;
            targetQuaternion = Quaternion.LookRotation(dir);
            rotation = Quaternion.Lerp(rotatingPart.rotation, targetQuaternion, Time.deltaTime * turnSpeed).eulerAngles;
            rotatingPart.rotation = Quaternion.Euler(0f, rotation.y, 0f);
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

                yield return detectionWaitForSec;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, range);
        }

    }

}