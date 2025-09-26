using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bakery
{
    public class BreadManager : ObjectPool
    {
        private GameManager gameManager;
        public Stack<Bread> breadsInBasket;
        public List<Bread> breadsInTray;
        public Stack<Bread> breadsInStall;

        [SerializeField] private string breadPoolTag = "Bread";
        private Coroutine bakingCoroutine;
        private float timer;

        [field: SerializeField] private bool isGameRunning;

        [Header("Basket")]
        [field: SerializeField] private float bakingTime = 5;
        [field: SerializeField] private float basketSize = 15;
        [field: SerializeField] private Vector3 spawnPos;
        private int breadCountOnBasket;
        private bool isBasketFulled;

        [Header("Tray")]
        [field: SerializeField] private float traySize = 8;
        private int breadCountOnTray;
        private bool isTrayFulled;

        [Header("Stall")]
        [field: SerializeField] private float stallSize = 30;
        private int breadCountOnStall;
        private bool isStallFulled;

        public void Initialize()
        {
            gameManager = GameManager.Instance;
            gameManager.GameStartEvent += OnGameStart;
            isGameRunning = false;
            isBasketFulled = false;
            isTrayFulled = false;
            isStallFulled = false;

            InitializeTray();
            breadCountOnBasket = 0;
            breadCountOnBasket = 0;
        }

        private void InitializeTray()
        {
            for (int i = 0; i < traySize; i++) 
            {
                Bread newBread = SpawnWithoutPool(breadPoolTag).GetComponent<Bread>();
                breadsInTray.Add(newBread);
                //newBread.InitializeOnTray(i);
            }
            breadCountOnTray = 0;
        }
        
        private void AddBreadToTray(int count) 
        {
            //////////////
        }

        private void AddBreadToBasket() 
        {
            Bread newBread = SpawnFromPool(breadPoolTag, true).GetComponent<Bread>();
            breadsInBasket.Push(newBread);
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
            timer = Time.time;
            while (isGameRunning)
            {
                while (breadCountOnBasket >= basketSize)
                {
                    yield return null;
                }

                if(Time.time - timer >= bakingTime) 
                {
                    AddBreadToBasket();
                    timer = Time.time;
                }

                yield return null;
            }
        }
    }






}
