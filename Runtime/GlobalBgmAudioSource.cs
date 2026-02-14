namespace Tesseract.Audio
{
    public class GlobalBgmAudioSource : CustomAudioSourceBase
    {
        public bool playOnEnabled;
        public PlayData data;

        public override void Play()
        {
            AudioManager.Instance.PlayBgm(data.clip, data.Volume);
        }

        protected virtual void OnEnable()
        {
            if (playOnEnabled)
                Play();
        }
    }
}
