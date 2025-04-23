using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public enum EquipmentType
    {
        Weapon,
        Armor,
        Aaccessory
    }

    public class Weapon : Equipment
    {
        public ItemInstance ItemInstance { get; private set; }
        public void Initialize(ItemInstance _itemInstance)
        {
            ItemInstance = _itemInstance;
        }
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Custom/New Weapon")]
    public class WeaponData : ItemData
    {
        [Header("Equip Prefab")]
        public GameObject EquipPrefab;

        [Header("Statistics Data")]
        public EquipmentType equipmentType;
        public float RangeModifier;
        public float SpeedModifier;
        public float DamageBonus;
        public float IntensityBonus;
    }
}
