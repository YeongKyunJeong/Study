using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class BattleSystem : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = .5f;

        StatisticsHandlerForCharacter statisticsHandler;
        private float timeSinceLastChange = float.MaxValue;

        public event Action OnDamage;
        public event Action OnHeal;
        public event Action OnDeath;
        public event Action OnInvicibilityEnd;

        public float CurrentHealth { get; private set; }
        public float MaxHP => statisticsHandler.CurrentStatistics.MaxHP;

        private void Awake()
        {
            statisticsHandler = GetComponent<StatisticsHandlerForCharacter>();
        }

        public void InitHealth()
        {
            CurrentHealth = MaxHP;
        }

        private void Update()
        {
            if (timeSinceLastChange < healthChangeDelay)
            {
                timeSinceLastChange += Time.deltaTime;
                if (timeSinceLastChange >= healthChangeDelay)
                {
                    OnInvicibilityEnd?.Invoke();
                }
            }
        }

        public bool ChangeHealth(float value)
        {
            if (value == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0;
            CurrentHealth += value;
            CurrentHealth = CurrentHealth > MaxHP ? MaxHP : CurrentHealth;
            CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

            if (value > 0)
            {
                OnHeal?.Invoke();
            }
            else
            {
                OnDamage?.Invoke();
            }

            if (CurrentHealth <= 0f)
            {
                Death();
            }

            return true;
        }

        private void Death()
        {
            OnDeath?.Invoke();


        }
    }
}
