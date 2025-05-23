using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [System.Serializable]
    public class AttackDataLibrary
    {

        [field: SerializeField] public AttackData BaseAttackData { get; private set; }
        [field: SerializeField] public List<AttackData> AttackDataList { get; private set; }

        public AttackData GetAttackInfo(int index)
        {
            return AttackDataList.Count <= index ? null : AttackDataList[index];
        }

        public void Initialize()
        {
            BaseAttackData.GenerateHash();
            foreach (var attackInfo in AttackDataList)
            {
                attackInfo.GenerateHash();
            }
        }

    }
}
