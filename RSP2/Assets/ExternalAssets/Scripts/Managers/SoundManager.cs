using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField][Range(0f, 1f)] private float soundEffectVolume;
        [SerializeField][Range(0f, 1f)] private float soundEffectPitchVariance;
        [SerializeField][Range(0f, 1f)] private float musicVolume;

        ObjectPool objectPool;

        private AudioSource musicAudioSource;
        public AudioClip musicClip;


        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
