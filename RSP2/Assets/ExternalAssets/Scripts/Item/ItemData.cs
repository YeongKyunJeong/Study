using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{

    public enum ItemType
    {
        Equipable,
        Consumable,
    }

    public enum ConsumableType
    {
        Health,
    }

    [System.Serializable]
    public class ItemDataForConsumable
    {
        public ConsumableType type;
        public int value;
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Custom/New Item")]
    public class ItemData : ScriptableObject
    {
        [Header("Info")]
        public string displayName;
        public ItemType type;
        public GameObject dropPrefab;

        [Header("Stacking")]
        public bool canStack;
        public int maxStackAmount;

        public AudioClip usageSoundClip;
        //[Header("Consumable")]
        //public ItemDataForConsumable[] consumables;


    }

}