using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class WayPoints : MonoBehaviour
    {
        private static Transform[] points;  // Add multiple way logic
        public static Transform[] GetPoint { get { return points; } private set { points = value; } }

        // temp
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            points = new Transform[transform.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i);
            }

        }
    }
}
