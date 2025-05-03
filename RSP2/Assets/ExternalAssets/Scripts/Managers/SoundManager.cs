using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RSP2
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [field: SerializeField] [Range(0f, 1f)] private float soundEffectVolume;
        [field: SerializeField] [Range(0f, 1f)] private float soundEffectPitchVariance;
        [field: SerializeField][Range(0f, 1f)] private float musicVolume;


        private ObjectPool objectPool;

        private AudioSource musicAudioSource;
        public AudioClip musicClip;


        [field: SerializeField] public SFXScriptableObject SFXSOData { get; private set; }
        private static SFXScriptableObject SFXSO { get; set; }

        void Awake()
        {
            musicAudioSource = Camera.main.GetComponent<AudioSource>();
            musicAudioSource.volume = musicVolume;
            musicAudioSource.loop = true;

            SFXSO = SFXSOData;
            objectPool = GetComponent<ObjectPool>();
        }

        private void Start()
        {
            ChangeBackGroundMusic(musicClip);
        }

        public static void ChangeBackGroundMusic(AudioClip musicClip)
        {
            Instance.musicAudioSource.Stop();
            Instance.musicAudioSource.clip = musicClip;
            Instance.musicAudioSource.Play();
        }

        public static void PlayClip(AudioClip clip, float volumeMultiplier = 1.0f)
        {
            GameObject go = Instance.objectPool.SpawnFromPool("SoundSource", false);
            go.SetActive(true);
            SoundSource soundSource = go.GetComponent<SoundSource>();
            soundSource.Play(clip, Instance.soundEffectVolume * volumeMultiplier, Instance.soundEffectPitchVariance);
        }

        public static void PlayDamageSoundClip(DamageType damageType, float volumeMultiplier = 1.0f)
        {
            GameObject go = Instance.objectPool.SpawnFromPool("SoundSource", false);
            go.SetActive(true);
            SoundSource soundSource = go.GetComponent<SoundSource>();

            switch (damageType)
            {
                case DamageType.None:
                    break;
                case DamageType.Slashing:
                    {
                        soundSource.Play(SFXSO.SFXDataLibrary.SlashingHitSounds[0], Instance.soundEffectVolume * volumeMultiplier, Instance.soundEffectPitchVariance);

                        break;
                    }
                case DamageType.Blunging:
                    {
                        soundSource.Play(SFXSO.SFXDataLibrary.BlungingHitSounds[0], Instance.soundEffectVolume * volumeMultiplier, Instance.soundEffectPitchVariance);
                        break;
                    }
            }
        }
    }
}
