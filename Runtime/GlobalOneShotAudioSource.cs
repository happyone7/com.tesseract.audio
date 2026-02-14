using UnityEngine;

namespace Tesseract.Audio
{
    public class GlobalOneShotAudioSource : CustomAudioSourceBase
    {
        public bool playOnEnabled;
        public PlayData data;
        public AudioCategory category;
        public ESoundType soundType = ESoundType.UI_SFX;

        public override void Play()
        {
            if (category != null && category.IsFull())
                return;

            AudioManager.Instance.PlayOneShot(data.clip, data.Volume, soundType);

            if (category != null)
                category.AddCount(data.clip);
        }

        protected virtual void OnEnable()
        {
            if (playOnEnabled)
                Play();
        }
    }
}
