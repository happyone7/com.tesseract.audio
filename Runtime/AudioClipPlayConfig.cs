using UnityEngine;

namespace Tesseract.Audio
{
    public class AudioClipPlayConfig
    {
        public AudioClip Clip;
        public float Volume;

        public AudioClipPlayConfig(AudioClip clip, float volume)
        {
            Clip = clip;
            Volume = volume;
        }
    }
}
