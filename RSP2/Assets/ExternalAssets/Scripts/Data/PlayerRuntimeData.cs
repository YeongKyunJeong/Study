using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class PlayerRuntimeData
    {
        public Vector2 MovementInput { get; set; }

        public float MovementSpeedModifier { get; set; } = 1;
    }
}
