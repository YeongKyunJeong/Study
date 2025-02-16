using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace TDP
{
    public enum StageUITMPType
    {
        WaveIndex,
        WaveCountDown,
        Money
    }

    public class StageUITMPSetter : MonoBehaviour
    {
        private int waveIndex;
        [SerializeField] TextMeshProUGUI WaveIndexTMP;
        private const int MAX_WAVE_INDEX = 99;
        public int WaveIndex
        {
            get { return waveIndex; }
            private set
            {
                waveIndex = value > MAX_WAVE_INDEX ? MAX_WAVE_INDEX : value;
                WaveIndexTMP.text = waveIndex.ToString();
            }
        }

        private float waveCountDown;
        [SerializeField] TextMeshProUGUI WaveCountDownTMP;
        private const float MAX_COUNTDOWN_NUMBER = 10;
        private const float MIN_COWNTDOWN_NUMBER = 0;
        public float WaveCountDown
        {
            get { return waveCountDown; }
            private set
            {
                waveCountDown = value;
                if (value < MIN_COWNTDOWN_NUMBER)
                {
                    value = 0;
                }
                WaveCountDownTMP.text = $"{value:F2}";
                //WaveCountDownTMP.text = $"{value:00.00}";
            }
        }
        private int money;
        [SerializeField] TextMeshProUGUI moneyTMP;
        public int Money
        {
            get { return money;}
            private set
            {
                money = value;
                moneyTMP.text = $"$ {money}";
            }
        }


        private void Update()
        {
            moneyTMP.text = $"$ {PlayerStats.Money}";
        }

        public void ChangeValue(StageUITMPType targetTMP, int changedValue)
        {
            switch (targetTMP)
            {
                case StageUITMPType.WaveIndex:
                    {
                        WaveIndex = changedValue;
                        break;
                    }
                case StageUITMPType.WaveCountDown:
                    {
                        WaveCountDown = changedValue;
                        break;
                    }
                case StageUITMPType.Money:
                    {
                        Money = changedValue;
                        break;
                    }
            }

        }

        public void ChangeValue(StageUITMPType targetTMP, float changedValue)
        {
            switch (targetTMP)
            {
                case StageUITMPType.WaveIndex:
                    {
                        Debug.Log("Wrong value type, need int not float");
                        break;
                    }
                case StageUITMPType.WaveCountDown:
                    {
                        WaveCountDown = changedValue;
                        break;
                    }
            }
        }


    }

}