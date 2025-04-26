using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = .5f;

        StatisticsHandlerForCharacter StatisticsHandler;
        
        [SerializeField] protected Faction myFaction;
        public Faction MyFaction { get => myFaction; set { myFaction = value; } }

        [SerializeField] protected bool isDead;

        public bool IsDead { get => isDead; set { isDead = value; } }

        private float timeSinceLastChange = float.MaxValue;

        protected AttackHitBox attackHitBox;
        public event Action DamageEvent;
        public event Action HealEvent;
        public event Action DeathEvent;
        public event Action InvicibilityEndEvent;

        public float CurrentHealth { get; private set; }
        public float MaxHP => StatisticsHandler.CurrentStatistics.MaxHP;
        private bool isInitialized;

        protected virtual void Awake()
        {
            StatisticsHandler = GetComponent<StatisticsHandlerForCharacter>();
            isInitialized = false;
            isDead = false;
        }

        public void InitHealth()
        {
            isInitialized = true;
            CurrentHealth = MaxHP;
            isDead = false;
        }

        private void Update()
        {
            if (timeSinceLastChange < healthChangeDelay)
            {
                timeSinceLastChange += Time.deltaTime;
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    InvicibilityEndEvent?.Invoke();
                }
            }
        }

        public bool ChangeHealth(float value)
        {
            if (!isInitialized) InitHealth(); 

            if (value == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0;
            
            CurrentHealth += value;
            CurrentHealth = CurrentHealth > MaxHP ? MaxHP : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;
            Debug.Log(CurrentHealth);

            if (value > 0)
            {
                HealEvent?.Invoke();
            }
            else
            {
                DamageEvent?.Invoke();
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }

        private void Death()
        {
            DeathEvent?.Invoke();


        }
    }
}
