using UnityEngine;

namespace Tesseract.Audio
{
    [System.Serializable]
    public class WeightedAudioClip
    {
        public AudioClip clip;
        public int weight = 1;
    }
}
