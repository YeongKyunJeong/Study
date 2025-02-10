using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TDP
{

    public class WayPoints : MonoBehaviour
    {
        private static List<Transform[]> pointsList = new List<Transform[]>();  // Add multiple way 
        //private static Transform[][] pointsArray = new Transform[0][];

        // temp
        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            Transform[] points = new Transform[transform.childCount];
            for (int i = 0; i < points.Length; i++)
            {
                points[i] = transform.GetChild(i);
            }
            pointsList.Add(points);
        }

        public static Transform[] GetPoints(int wayPointsNumber)
        {
            if (pointsList.Count > wayPointsNumber)
                return pointsList[wayPointsNumber];
            else
                return pointsList[pointsList.Count - 1];
        }
    }
}
