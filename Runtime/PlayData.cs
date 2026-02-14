using UnityEngine;

namespace Tesseract.Audio
{
    [System.Serializable]
    public struct PlayData
    {
        [Range(0.0f, 1.0f)]
        public float Volume;
        public AudioClip clip;
    }
}
