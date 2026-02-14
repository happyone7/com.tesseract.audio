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
            return AudioManager.Instance.IsCategoryFullPlaying(this);
        }

        public void AddCount(AudioClip clip)
        {
            AudioManager.Instance.OnPlayOneShot(this, clip);
        }
    }
}
