using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class DayNightManager : MonoBehaviour
    {
        //[field: SerializeField] private string sunMoonParentName { get; set; } 

        [Range(0f, 1f)] public float Time;
        [field: SerializeField] public float StartTime { get; private set; }

        [Header("Day")]
        [field: SerializeField] private float dayLength;
        public float DayLength => dayLength;
        private float dayTimeRate;
        private Light sun;
        [field: SerializeField] private Gradient sunGradient;
        [field: SerializeField] private AnimationCurve sunIntensityCurve;

        [Header("Night")]
        [field: SerializeField] private float nightLength;
        public float NightLength => nightLength;
        private float nightTimeRate;
        private Light moon;
        [field: SerializeField] private Gradient moonGradient;
        [field: SerializeField] private AnimationCurve moonIntensityCurve;

        private bool isDay;
        private Material skyBoxMaterial;

        public void Initialize()
        {
            SunAndMoon sunAndMoon = FindObjectOfType<SunAndMoon>();
            if (sunAndMoon == null)
            {
                Debug.LogError("Sun and Moon not found");
                return;
            }

            sun = sunAndMoon.Sun;
            moon = sunAndMoon.Moon;
            skyBoxMaterial = new Material(RenderSettings.skybox);
            RenderSettings.skybox = skyBoxMaterial;

            if (dayLength < 1)
            {
                Debug.LogError("Day length is 0");
                return;
            }
            dayTimeRate = 1f / dayLength;

            if (nightLength < 1)
            {
                Debug.LogError("Night length is 0");
                return;
            }
            nightTimeRate = 1f / nightLength;

            Time = StartTime;

        }
    }
}
