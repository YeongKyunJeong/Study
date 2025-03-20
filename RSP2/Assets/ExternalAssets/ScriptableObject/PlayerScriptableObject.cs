using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    [CreateAssetMenu(fileName = "PlayerSO", menuName ="Custom/SO/Characters/Player")]
    public class PlayerScriptableObject : ScriptableObject
    {
        [field : SerializeField] public MovementStateDataForPlayer MovementStateData { get; private set; }
    }
}
