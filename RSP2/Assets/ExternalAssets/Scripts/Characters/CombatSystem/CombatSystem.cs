using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace RSP2
{
    public class CombatSystem : MonoBehaviour
    {
        public CombatUnit MyUnit { get; private set; }

        [SerializeField] private float healthChangeDelay = .5f;

        private StatisticsHandlerForCharacter statisticsHandler;

        [SerializeField] protected Faction myFaction;
        public Faction MyFaction { get => myFaction; set { myFaction = value; } }

        [SerializeField] protected bool isDead;

        public bool IsDead { get => isDead; set { isDead = value; } }

        private float timeSinceLastChange = float.MaxValue;


        protected AttackHitBox attackHitBox;
        public event Action DamageEvent;
        public event Action HPHealEvent;
        public event Action DieEvent;
        public event Action InvicibilityEndEvent;
        private Coroutine hPRegenCoroutine;
        private Coroutine hPRegenDelayCoroutine;
        private float hpRegenDelayTime = 5f;

        public event Action MPSpendEvent;
        public event Action MPRecoveryEvent;
        private Coroutine mPRegenCoroutine;
        private Coroutine mPRegenDelayCoroutine;
        private float mPRegenDelayTime = 5f;

        public event Action StaminaSpendEvent;
        public event Action StaminaRecoveryEvent;
        private Coroutine staminaRegenCoroutine;
        private Coroutine staminaRegenDelayCoroutine;
        private float staminaRegenDelayTime = 2f;
        private float exhaustionDelayTime = 5f;

        private bool isInitialized;
        public float CurrentHP { get; private set; }
        public float MaxHP => statisticsHandler.CurrentStatistics.MaxHP;

        public float CurrentMP { get; private set; }
        public float MaxMP => statisticsHandler.CurrentStatistics.MaxMP;

        public float CurrentStamina { get; private set; }
        public float MaxStamina => statisticsHandler.CurrentStatistics.MaxStamina;

        protected virtual void Awake()
        {
            MyUnit = GetComponent<CombatUnit>();
            statisticsHandler = GetComponent<StatisticsHandlerForCharacter>();
            isInitialized = false;
            isDead = false;
        }

        public void InitStatistics()
        {
            isInitialized = true;
            CurrentHP = MaxHP;
            CurrentMP = MaxMP;
            CurrentStamina = MaxStamina;
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

            SFXManager.PlayDamageSoundClip(damageType, transform.position);


            return true;
        }


        public bool ChangeHealth(float value)
        {
            if (!isInitialized) InitStatistics();

            CurrentHP += value;
            CurrentHP = Mathf.Round(CurrentHP * 10) / 10;
            CurrentHP = CurrentHP > MaxHP ? MaxHP : CurrentHP;
            CurrentHP = CurrentHP < 0 ? 0 : CurrentHP;
            Debug.Log(CurrentHP);

            if (value > 0)
            {
                HPHealEvent?.Invoke();
            }
            else
            {
                DamageEvent?.Invoke();

                if (hPRegenCoroutine != null)
                {
                    StopCoroutine(hPRegenCoroutine);
                    hPRegenCoroutine = null;
                }

                if (hPRegenDelayCoroutine != null)
                {
                    StopCoroutine(hPRegenDelayCoroutine);
                    hPRegenDelayCoroutine = null;
                }

                hPRegenDelayCoroutine = StartCoroutine(StartHPRegenAfterDelay());
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

        public void ChangeMana(float value)
        {
            if (!isInitialized) InitStatistics();

            CurrentMP += value;
            CurrentMP = Mathf.Round(CurrentMP * 10) / 10;
            CurrentMP = CurrentMP > MaxMP ? MaxMP : CurrentMP;
            CurrentMP = CurrentMP < 0 ? 0 : CurrentMP;
            Debug.Log(CurrentMP);

            if (value > 0)
            {
                MPRecoveryEvent?.Invoke();
            }
            else
            {
                MPSpendEvent?.Invoke();

                if (mPRegenCoroutine != null)
                {
                    StopCoroutine(mPRegenCoroutine);
                    mPRegenCoroutine = null;
                }

                if (mPRegenDelayCoroutine != null)
                {
                    StopCoroutine(mPRegenDelayCoroutine);
                    mPRegenDelayCoroutine = null;
                }

                mPRegenDelayCoroutine = StartCoroutine(StartMPRegenAfterDelay());
            }
        }

        public void ChangeStamina(float value)
        {
            if (!isInitialized) InitStatistics();

            CurrentStamina += value;
            CurrentStamina = Mathf.Round(CurrentStamina * 10) / 10;
            CurrentStamina = CurrentStamina > MaxStamina ? MaxStamina : CurrentStamina;
            CurrentStamina = CurrentStamina < 0 ? 0 : CurrentStamina;
            Debug.Log(CurrentStamina);

            if (value > 0)
            {
                StaminaRecoveryEvent?.Invoke();
            }
            else
            {
                StaminaSpendEvent?.Invoke();

                if (staminaRegenCoroutine != null)
                {
                    StopCoroutine(staminaRegenCoroutine);
                    staminaRegenCoroutine = null;
                }

                if (staminaRegenDelayCoroutine != null)
                {
                    StopCoroutine(staminaRegenDelayCoroutine);
                    staminaRegenDelayCoroutine = null;
                }

                staminaRegenDelayCoroutine = StartCoroutine(StartStaminaRegenAfterDelay());
            }
        }

        #region HP Regen Coroutines
        private IEnumerator StartHPRegenAfterDelay()
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

                //Debug.Log($"{name} HP 회복 중 : {CurrentHP}/{MaxHP}");

                yield return new WaitForSeconds(1);
            }

            hPRegenCoroutine = null;

            yield return null;
        }
        #endregion

        #region Mp Regen Coroutines
        private IEnumerator StartMPRegenAfterDelay()
        {
            yield return new WaitForSeconds(mPRegenDelayTime);

            if (CurrentMP < MaxMP)
            {
                mPRegenCoroutine = StartCoroutine(MPRegen());
            }

            mPRegenDelayCoroutine = null;

            yield return null;
        }

        private IEnumerator MPRegen()
        {
            while (CurrentMP < MaxMP)
            {
                CurrentMP += statisticsHandler.CurrentStatistics.MPRegen;
                CurrentMP = CurrentMP > MaxMP ? MaxMP : CurrentMP;

                //Debug.Log($"{name} MP 회복 중 : {CurrentMP}/{MaxMP}");

                yield return new WaitForSeconds(1);
            }

            mPRegenCoroutine = null;

            yield return null;
        }
        #endregion


        #region Stamina Regen Coroutines
        private IEnumerator StartStaminaRegenAfterDelay()
        {
            yield return new WaitForSeconds(staminaRegenDelayTime);

            if (CurrentStamina < MaxStamina)
            {
                staminaRegenCoroutine = StartCoroutine(StaminaRegen());
            }

            staminaRegenDelayCoroutine = null;

            yield return null;
        }

        private IEnumerator StaminaRegen()
        {
            while (CurrentStamina < MaxStamina)
            {
                CurrentStamina += 0.25f * statisticsHandler.CurrentStatistics.StaminaRegen;
                CurrentStamina = CurrentStamina > MaxStamina ? MaxStamina : CurrentStamina;

                //Debug.Log($"{name} 스태미나 회복 중 : {CurrentStamina}/{MaxStamina}");

                yield return new WaitForSeconds(0.25f);
            }

            staminaRegenCoroutine = null;

            yield return null;
        }
        #endregion
    }
}
