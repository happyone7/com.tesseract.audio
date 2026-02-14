using System.Collections;
using UnityEngine;

namespace Tesseract.Audio
{
    public class FadingAudioSource : MonoBehaviour
    {
        public float FadeTime { get; set; } = 0.5f;
        public float GlobalVolume { get; protected set; } = 1.0f;
        public float Progress { get; protected set; }
        public AudioClipPlayConfig CurrentClipOrNull { get; protected set; }
        public AudioSource AudioSource { get; private set; }
        public bool IsPaused { get; protected set; }

        public void Initialize(AudioSource source)
        {
            AudioSource = source;
        }

        public void SetVolume(float globalVolume)
        {
            GlobalVolume = globalVolume;
            if (CurrentClipOrNull != null && AudioSource != null)
                AudioSource.volume = GlobalVolume * CurrentClipOrNull.Volume * Progress;
        }

        public void PlayWithFadeOutIn(AudioClipPlayConfig clip)
        {
            if (AudioSource == null) return;
            if (IsPaused)
            {
                CurrentClipOrNull = clip;
                return;
            }
            StopAllCoroutines();
            StartCoroutine(FadeOutAndIn(clip));
        }

        public void StopWithFade()
        {
            if (AudioSource == null) return;
            StopAllCoroutines();
            StartCoroutine(FadeOut(0));
        }

        public void PauseWithFade()
        {
            if (AudioSource == null || IsPaused) return;
            IsPaused = true;
            StopAllCoroutines();
            StartCoroutine(FadeOut(0));
        }

        public void ResumeWithFade()
        {
            if (AudioSource == null || !IsPaused) return;
            IsPaused = false;
            if (CurrentClipOrNull == null) return;
            StopAllCoroutines();
            StartCoroutine(FadeIn(CurrentClipOrNull.Volume));
        }

        private IEnumerator FadeOutAndIn(AudioClipPlayConfig clip)
        {
            yield return StartCoroutine(FadeOut(0));
            CurrentClipOrNull = clip;
            AudioSource.clip = clip.Clip;
            AudioSource.Play();
            yield return StartCoroutine(FadeIn(clip.Volume));
        }

        private IEnumerator FadeIn(float targetVolume)
        {
            Progress = GlobalVolume > 0 ? Mathf.Clamp(AudioSource.volume / GlobalVolume, 0f, 1f) : 0f;
            float fadeInSpeed = 1 / FadeTime;
            while (Progress < 1)
            {
                Progress += fadeInSpeed * Time.deltaTime;
                AudioSource.volume = GlobalVolume * targetVolume * Progress;
                yield return null;
            }
            Progress = 1;
            AudioSource.volume = GlobalVolume * targetVolume;
        }

        private IEnumerator FadeOut(float targetVolume)
        {
            Progress = GlobalVolume > 0 ? Mathf.Clamp(AudioSource.volume / GlobalVolume, 0f, 1f) : 0f;
            float fadeOutSpeed = 1 / FadeTime;
            while (Progress > 0)
            {
                Progress -= fadeOutSpeed * Time.deltaTime;
                AudioSource.volume = GlobalVolume * targetVolume * Progress;
                yield return null;
            }
            Progress = 0;
            AudioSource.volume = 0;
        }
    }
}
