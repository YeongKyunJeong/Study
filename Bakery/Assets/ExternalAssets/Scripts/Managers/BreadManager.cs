using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class BreadManager : ObjectPool
    {
        private GameManager gameManager;
        public Queue<Bread> breadsInBasket;
        public List<Bread> breadsInTray;
        public List<Bread> breadsInStall;

        [SerializeField] private string breadPoolTag = "Bread";
        private Coroutine bakingCoroutine;
        private float timer;
        private Coroutine basketToTrayCoroutine;
        private Coroutine trayToStallCoroutine;

        [field: SerializeField] private bool isGameRunning;

        [Header("Basket")]
        [field: SerializeField] private float bakingTime = 2;
        [field: SerializeField] private int basketSize = 8;
        [field: SerializeField] private Transform spawnPoint;
        [field: SerializeField] private Vector3 spawnPos;
        private int breadCountOnBasket;
        private bool isBasketFulled;

        [Header("Tray")]
        [field: SerializeField] private int traySize = 8;
        [field: SerializeField] private int breadCountOnTray; // For display
        private bool isTrayFulled;
        private PlayerRuntimeData playerRuntimeData;

        [Header("Stall")]
        [field: SerializeField] private int stallSize = 8;
        private int breadCountOnStall;
        private bool isStallFulled;

        public override void Initialize()
        {
            base.Initialize();

            gameManager = GameManager.Instance;
            gameManager.GameStartEvent += OnGameStart;
            breadsInBasket = new Queue<Bread>();
            playerRuntimeData = gameManager.Player.runtimeData;

            isGameRunning = false;
            isBasketFulled = false;
            isTrayFulled = false;
            isStallFulled = false;

            spawnPos = spawnPoint.position;
            breadCountOnBasket = 0;
            InitializeTray();
            InitializeStall();
        }

        private void InitializeTray()
        {
            for (int i = 0; i < traySize; i++)
            {
                breadsInTray[i].InitializeOnTray();
            }
            breadCountOnTray = 0;
            playerRuntimeData.isCarryingBread = false;
        }

        private void InitializeStall()
        {
            for (int i = 0; i < stallSize; i++)
            {
                breadsInStall[i].InitializeOnStall();
            }
            breadCountOnStall = 0;
        }

        private void AddBreadToBasket()
        {
            Bread newBread = SpawnFromPool(breadPoolTag, true).GetComponent<Bread>();
            breadsInBasket.Enqueue(newBread);
            breadCountOnBasket++;
            newBread.SetBreadToBasket(spawnPos);
        }

        private void OnGameStart()
        {
            isGameRunning = true;
            bakingCoroutine = StartCoroutine(Bake());
        }

        private IEnumerator Bake()
        {
            if (breadCountOnBasket < basketSize) AddBreadToBasket();
            timer = Time.time;
            while (isGameRunning)
            {
                while (breadCountOnBasket >= basketSize)
                {
                    yield return null;
                }

                if (Time.time - timer >= bakingTime)
                {
                    AddBreadToBasket();
                    timer = Time.time;
                }

                yield return null;
            }
        }

        public void MoveBreadToTray()
        {
            if (basketToTrayCoroutine != null) return;

            int moveCount = traySize - breadCountOnTray;
            moveCount = breadCountOnBasket > moveCount ? moveCount : breadCountOnBasket;

            if (moveCount == 0) return;

            basketToTrayCoroutine = StartCoroutine(BasketToTrayCoroutine());
        }

        public void MoveBreadToStall()
        {
            if (trayToStallCoroutine != null) return;

            int moveCount = stallSize - breadCountOnStall;
            moveCount = breadCountOnTray > moveCount ? moveCount : breadCountOnTray;

            if (moveCount == 0) return;

            trayToStallCoroutine = StartCoroutine(TrayToStallCoroutine());
        }

        private IEnumerator BasketToTrayCoroutine()
        {
            Bread nextBread = breadsInBasket.Dequeue();
            nextBread.ReturnToPool();
            breadCountOnBasket--;

            breadsInTray[breadCountOnTray].MoveToTray(nextBread.transform);
            breadCountOnTray++;

            if (breadCountOnTray >= 0)
            {
                playerRuntimeData.isCarryingBread = true;
            }

            yield return new WaitForSeconds(0.1f);

            basketToTrayCoroutine = null;
            yield return null;
        }

        private IEnumerator TrayToStallCoroutine()
        {
            breadCountOnTray--;
            if (breadCountOnTray == 0)
            {
                playerRuntimeData.isCarryingBread = false;
            }
            breadsInStall[breadCountOnStall].MoveToStall(breadsInTray[breadCountOnTray].transform);
            breadsInTray[breadCountOnTray].gameObject.SetActive(false);
            breadCountOnStall++;


            yield return new WaitForSeconds(0.1f);

            trayToStallCoroutine = null;
            yield return null;
        }

    }






}
