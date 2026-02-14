using System.Collections.Generic;
using Tesseract.Core;
using UnityEngine;
using UnityEngine.Audio;

namespace Tesseract.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private AudioMixerConfig _config;
        public AudioSource BgmSource => _bgmSource;
        private AudioSource _bgmSource;
        private AudioSource _gameSfxSource;
        private AudioSource _uiSfxSource;
        private AudioSource _extraSource;

        private Dictionary<AudioCategory, AudibleClipList> _playingAudios = new Dictionary<AudioCategory, AudibleClipList>();

        private bool _initialized;
        private float _volumeBeforeMute = 0f;

        protected override void Awake()
        {
            base.Awake();
            Init();
        }

        public void Init(AudioMixerConfig config = null)
        {
            if (_initialized)
            {
                if (config != null)
                    _config = config;
                return;
            }

            _config = config != null ? config : Resources.Load<AudioMixerConfig>("Audio/AudioMixerConfig");

            if (_config == null)
            {
                Debug.LogError("[AudioManager] AudioMixerConfig not found. Create one via Tesseract/Audio/AudioMixerConfig.");
                return;
            }

            _bgmSource = CreateAudioSource("BGM", _config.bgmMixerGroup, true);
            _gameSfxSource = CreateAudioSource("GameSFX", _config.gameSfxMixerGroup);
            _uiSfxSource = CreateAudioSource("UI_SFX", _config.uiSfxMixerGroup);
            _extraSource = CreateAudioSource("Extra", _config.extraMixerGroup);
            _initialized = true;
        }

        private AudioSource CreateAudioSource(string name, AudioMixerGroup mixerGroup, bool loop = false)
        {
            var source = new GameObject(name).AddComponent<AudioSource>();
            source.transform.SetParent(transform);
            source.playOnAwake = false;
            source.loop = loop;
            if (mixerGroup != null)
                source.outputAudioMixerGroup = mixerGroup;
            return source;
        }

        protected void Update()
        {
            foreach (var b in _playingAudios.Values)
                b.UpdateAudibles(Time.realtimeSinceStartup);
        }

        #region Volume Control

        public void SetVolume(ESoundType type, float volume)
        {
            if (_config == null || _config.gameMixer == null) return;
            string param = GetVolumeParameter(type);
            if (!string.IsNullOrEmpty(param))
                _config.gameMixer.SetFloat(param, LogarithmicDbTransform(Mathf.Clamp01(volume)));
        }

        public float GetVolume(ESoundType type)
        {
            if (_config == null || _config.gameMixer == null) return 0f;
            string param = GetVolumeParameter(type);
            if (!string.IsNullOrEmpty(param) && _config.gameMixer.GetFloat(param, out float value))
                return InverseLogarithmicDbTransform(value);
            return 0f;
        }

        public bool IsMuted()
        {
            if (_config == null || _config.gameMixer == null) return false;
            _config.gameMixer.GetFloat(_config.masterVolumeParameter, out float value);
            return value <= -79f;
        }

        public void MuteAll()
        {
            if (_config?.gameMixer == null) return;
            _config.gameMixer.GetFloat(_config.masterVolumeParameter, out _volumeBeforeMute);
            _config.gameMixer.SetFloat(_config.masterVolumeParameter, -80f);
        }

        public void UnmuteAll()
        {
            if (_config?.gameMixer == null) return;
            _config.gameMixer.SetFloat(_config.masterVolumeParameter, _volumeBeforeMute);
        }

        private string GetVolumeParameter(ESoundType type)
        {
            if (_config == null) return null;
            return type switch
            {
                ESoundType.MASTER => _config.masterVolumeParameter,
                ESoundType.BGM => _config.bgmVolumeParameter,
                ESoundType.GameSFX => _config.gameSfxVolumeParameter,
                ESoundType.UI_SFX => _config.uiSfxVolumeParameter,
                ESoundType.Extra => _config.extraVolumeParameter,
                _ => null
            };
        }

        #endregion

        #region SFX

        public void PlayOneShot(AudioClip clip, float volume, ESoundType soundType)
        {
            if (clip == null) return;

            AudioSource source = soundType switch
            {
                ESoundType.GameSFX => _gameSfxSource,
                ESoundType.UI_SFX => _uiSfxSource,
                ESoundType.Extra => _extraSource,
                _ => null
            };

            source?.PlayOneShot(clip, volume);
        }

        public void OnPlayOneShot(AudioCategory category, AudioClip clip)
        {
            if (!_playingAudios.ContainsKey(category))
                _playingAudios.Add(category, new AudibleClipList(category.MaxAudible));

            _playingAudios[category].AddClip(Time.realtimeSinceStartup + clip.length);
        }

        #endregion

        #region BGM

        public void PlayBgm(AudioClip clip, float volume, bool overrideSameClip = true, bool loop = true)
        {
            if (clip == null) return;

            if (overrideSameClip && _bgmSource.clip != null)
            {
                if (_bgmSource.clip == clip || _bgmSource.clip.name == clip.name)
                    return;
            }

            _bgmSource.clip = clip;
            _bgmSource.volume = volume;
            _bgmSource.loop = loop;
            _bgmSource.Play();
        }

        public void PauseBgm() => _bgmSource.Pause();
        public void ResumeBgm() => _bgmSource.UnPause();
        public bool IsBgmPlaying() => _bgmSource.isPlaying;

        public bool IsBgmFinished()
        {
            return !_bgmSource.isPlaying && _bgmSource.time == 0;
        }

        #endregion

        #region Utility

        public bool IsCategoryFullPlaying(AudioCategory category)
        {
            if (_playingAudios.TryGetValue(category, out AudibleClipList list))
                return list.IsFull();
            return false;
        }

        protected float LogarithmicDbTransform(float volume)
        {
            return (Mathf.Log(89 * volume + 1) / Mathf.Log(90)) * 80 - 80;
        }

        protected float InverseLogarithmicDbTransform(float dbValue)
        {
            return (Mathf.Pow(10, (dbValue + 80) / 80 * Mathf.Log10(90)) - 1) / 89;
        }

        #endregion
    }
}
