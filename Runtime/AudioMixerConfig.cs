using UnityEngine;
using UnityEngine.Audio;

namespace Tesseract.Audio
{
    [CreateAssetMenu(fileName = "AudioMixerConfig", menuName = "Tesseract/Audio/AudioMixerConfig")]
    public class AudioMixerConfig : ScriptableObject
    {
        [Header("Volume Parameters")]
        public string masterVolumeParameter = "MasterVolume";
        public string bgmVolumeParameter = "BGMVolume";
        public string gameSfxVolumeParameter = "GameSFXVolume";
        public string uiSfxVolumeParameter = "UISFXVolume";
        public string extraVolumeParameter = "ExtraVolume";

        [Header("Mixer Groups")]
        public AudioMixer gameMixer;
        public AudioMixerGroup bgmMixerGroup;
        public AudioMixerGroup gameSfxMixerGroup;
        public AudioMixerGroup uiSfxMixerGroup;
        public AudioMixerGroup extraMixerGroup;
    }
}
