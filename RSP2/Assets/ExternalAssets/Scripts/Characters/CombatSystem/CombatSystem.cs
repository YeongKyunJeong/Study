using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace RSP2
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private float healthChangeDelay = .5f;

        private StatisticsHandlerForCharacter statisticsHandler;

        [SerializeField] protected Faction myFaction;
        public Faction MyFaction { get => myFaction; set { myFaction = value; } }

        [SerializeField] protected bool isDead;

        public bool IsDead { get => isDead; set { isDead = value; } }

        private float timeSinceLastChange = float.MaxValue;

        private Coroutine hPRegenCoroutine;
        private Coroutine hPRegenDelayCoroutine;
        private float hpRegenDelayTime = 5f;

        protected AttackHitBox attackHitBox;
        public event Action DamageEvent;
        public event Action HealEvent;
        public event Action DieEvent;
        public event Action InvicibilityEndEvent;

        public float CurrentHP { get; private set; }
        public float MaxHP => statisticsHandler.CurrentStatistics.MaxHP;
        private bool isInitialized;

        protected virtual void Awake()
        {
            statisticsHandler = GetComponent<StatisticsHandlerForCharacter>();
            isInitialized = false;
            isDead = false;
        }

        public void InitHealth()
        {
            isInitialized = true;
            CurrentHP = MaxHP;
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

        public bool TakeDamage(float value, DamageType damageType, bool applyDef = true)
        {


            if (value == 0 || timeSinceLastChange < healthChangeDelay)
            {
                return false;
            }

            timeSinceLastChange = 0;

            float reducedDamage;

            if (applyDef)
            {
                // HP & Def = 10 => 2HP & Def = 0 
                reducedDamage = (10 / (10 + statisticsHandler.CurrentStatistics.Deffence)) * value;
                reducedDamage = Mathf.Round(reducedDamage * 10f) / 10f;
            }
            else
            {
                reducedDamage = value;
            }

            ChangeHealth(reducedDamage);

            Debug.Log($"{name} got {reducedDamage} damage");

            SoundManager.PlayDamageSoundClip(damageType);


            return true;
        }

        public bool ChangeHealth(float value)
        {
            if (!isInitialized) InitHealth();

            //if (value == 0 || timeSinceLastChange < healthChangeDelay)
            //{
            //    return false;
            //}

            //timeSinceLastChange = 0;

            CurrentHP += value;
            CurrentHP = Mathf.Round(CurrentHP * 10) / 10;
            CurrentHP = CurrentHP > MaxHP ? MaxHP : CurrentHP;
            CurrentHP = CurrentHP < 0 ? 0 : CurrentHP;
            Debug.Log(CurrentHP);

            if (value > 0)
            {
                HealEvent?.Invoke();
            }
            else
            {
                DamageEvent?.Invoke();

                if (hPRegenCoroutine != null)
                {
                    StopCoroutine(hPRegenCoroutine);
                }

                hPRegenDelayCoroutine = StartCoroutine(StartRegenAfterDelay());


            }

            if (CurrentHP <= 0f)
            {
                Die();
            }

            return true;
        }

        private void Die()
        {
            isDead = true;
            DieEvent?.Invoke();
        }

        private IEnumerator StartRegenAfterDelay()
        {
            yield return new WaitForSeconds(hpRegenDelayTime);

            if (CurrentHP < MaxHP)
            {
                hPRegenCoroutine = StartCoroutine(HPRegen());
            }

            hPRegenDelayCoroutine = null;

            yield return null;
        }

        private IEnumerator HPRegen()
        {
            while (CurrentHP < MaxHP)
            {
                CurrentHP += statisticsHandler.CurrentStatistics.HPRegen;
                CurrentHP = CurrentHP > MaxHP ? MaxHP : CurrentHP;

                Debug.Log($"{name} HP È¸º¹ Áß : {CurrentHP}/{MaxHP}");

                yield return new WaitForSeconds(1);
            }

            hPRegenCoroutine = null;

            yield return null;
        }

    }
}
