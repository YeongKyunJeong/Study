using UnityEngine;

namespace RSP
{
    /// <summary>
    /// Holds individual State Data (such as the specific State Speed Modifier or Rotation Reach Time)
    /// </summary>
    [CreateAssetMenu(fileName = "Player", menuName = "Custom/Characters/Player")]
    public class PlayerSO : ScriptableObject
    {
        [field: SerializeField] public PlayerGroundedData GroundedData { get; private set; }

        [field: SerializeField] public PlayerAirborneData AirborneData { get; private set; }
    }
}
