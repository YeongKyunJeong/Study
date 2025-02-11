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

        #region Turret Data
        public float range;
        private float attackInterval;
        private int damage;

        #endregion

        private float detectionInterval;
        private LayerMask enemyLayerMask; // bitMask
        private WaitForSeconds detectionWaitForSec;
        private WaitForSeconds attackWaitForSec;

        public void FirstPoolingInitialize()
        {
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

        }

        private IEnumerator DetectEnemyCoroutine()
        {
            while (true)
            {
                detectedColldiers = Physics.OverlapSphere(transform.position, range, enemyLayerMask);
                // To do : Add target chosing logic

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