using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class EquipmentDataLibrary
    {
        [field: SerializeField] public List<WeaponData> WeaponData { get; private set; }
    }
}
