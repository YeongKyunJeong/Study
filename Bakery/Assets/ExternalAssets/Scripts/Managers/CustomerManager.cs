using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;

namespace Bakery
{
    public class CustomerManager : ObjectPool
    {
        private GameManager gameManager;
        private string customerTag = "Customer";
        [field: SerializeField] private int maxCount;
        [field: SerializeField] private int customerCount;
        [field: SerializeField] private int startCustomerCount;
        [field: SerializeField] private List<int> areaCount;

        [field: SerializeField] private Transform ExitPoint;
        private Vector3 exitPos;
        [field: SerializeField] private Transform rallyPoint;
        private Vector3 rallyPos;
        [field: SerializeField] private Transform takeOutLinePoint;
        private Vector3 takeOutLinePos;
        [field: SerializeField] private Transform tableLinePoint;
        private Vector3 tableLinePos;
        [field: SerializeField] private Transform stallLinePoint;
        private Vector3 stallLinePos;
        [field: SerializeField] private Transform chairPoint;
        private Vector3 chairPos;

        [field: SerializeField] private HashSet<Customer> customers;
        private Coroutine customerAddingCoroutine;

        public override void Initialize()
        {
            gameManager = GameManager.Instance;
            gameManager.GameStartEvent += OnGameStart;
            areaCount = new List<int>();
            exitPos = ExitPoint.position;
            rallyPos = rallyPoint.position;
            takeOutLinePos = takeOutLinePoint.position;
            tableLinePos = tableLinePoint.position;
            stallLinePos = stallLinePoint.position;
            // chairPos = chairPoint.position;

            base.Initialize();

            customers = new HashSet<Customer>();
            customerCount = 0;
        }

        public void CallUpdate()
        {
            foreach (Customer customer in customers)
            {
                customer.CallUpdate();
            }
        }

        public void OnGameStart() 
        {
            customerAddingCoroutine = StartCoroutine(AddNewCustomerCoroutine());
        }

        private IEnumerator AddNewCustomerCoroutine() 
        {
            while (true) 
            {
                Customer newCustomer = SpawnFromPool(customerTag, true).GetComponent<Customer>();
                int type = Random.Range(0, 1);
                int requirement = Random.Range(0, 3);
                newCustomer.Initialize(type, requirement, rallyPos, exitPos);
                customers.Add(newCustomer);
                customerCount++;

                while(customerCount >= maxCount) 
                {
                    yield return null;
                }

                float waitTime = Random.Range(0.1f, 2f);
                yield return new WaitForSeconds(waitTime);
            }
        }

    }
}
