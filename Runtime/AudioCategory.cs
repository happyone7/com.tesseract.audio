using Tesseract.Core;
using UnityEngine;

namespace Tesseract.Audio
{
    [CreateAssetMenu(fileName = "AudioCategory", menuName = "Tesseract/Audio/AudioCategory")]
    public class AudioCategory : ScriptableObject
    {
        public string Name;
        public int Priority;
        public int MaxAudible;

        public bool IsFull()
        {
            if (!Singleton<AudioManager>.HasInstance) return true;
            return AudioManager.Instance.IsCategoryFullPlaying(this);
        }

        public void AddCount(AudioClip clip)
        {
            if (!Singleton<AudioManager>.HasInstance) return;
            AudioManager.Instance.OnPlayOneShot(this, clip);
        }
    }
}
