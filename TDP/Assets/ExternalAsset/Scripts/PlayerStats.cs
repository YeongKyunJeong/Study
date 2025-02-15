using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{
    public class PlayerStats : MonoBehaviour
    {
        public static int Money;
        [SerializeField] private int startMoney = 400;

        public void Initialize()
        {
            Money = startMoney;
        }
    }
}
