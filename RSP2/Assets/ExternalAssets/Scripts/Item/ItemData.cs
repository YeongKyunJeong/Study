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


    [CreateAssetMenu(fileName = "Item", menuName = "Custom/New Item")]
    public class ItemData : ScriptableObject
    {
        public const int EQUIPMENT_KEY_CONST = 10000;
        public const int CONSUMABLE_KEY_CONST = 20000;

        [Header("Info")]
        public Sprite ItemSprite;
        public string DisplayName;
        public int Key;
        public ItemType Type;
        public GameObject DropPrefab;

        [Header("Stacking")]
        public bool CanStack;
        public int MaxStackAmount;

        public AudioClip UsageSoundClip;
        //[Header("Consumable")]
        //public ItemDataForConsumable[] consumables;


    }

}