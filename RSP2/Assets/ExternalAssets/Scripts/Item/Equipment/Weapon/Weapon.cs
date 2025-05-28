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
        public WeaponData WeaponData { get; set; }
        public void Initialize(ItemInstance _itemInstance)
        {
            ItemInstance = _itemInstance;
            WeaponData = ItemInstance.ItemData as WeaponData;
        }
    }

    [CreateAssetMenu(fileName = "Item", menuName = "Custom/New Weapon")]
    public class WeaponData : ItemData
    {
        [Header("Equip Prefab")]
        public GameObject EquipPrefab;
        public AudioClip AttackSoundClip;

        [Header("Statistics Data")]
        public EquipmentType EquipmentType;
        public DamageType DamageType;
        public float RangeModifier;
        public float SpeedModifier;
        public float DamageBonus;
        public float IntensityBonus;
    }
}
