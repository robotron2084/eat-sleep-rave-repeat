using System.Collections;
using System.Linq;
using UnityEngine;

namespace GameJamStarterKit
{
    public static class AudioSourceExtensions
    {
        public static void FadeOut(this AudioSource source, float duration)
        {
            FadeTo(source, duration, 0.0f);
        }

        public static void FadeTo(this AudioSource source, float duration, float targetVolume)
        {
            source.GetComponent<MonoBehaviour>().StartCoroutine(Fade(source, duration, targetVolume));
        }

        public static void FadeIn(this AudioSource source, float duration, float targetVolume = 1.0f)
        {
            FadeTo(source, duration, targetVolume);
        }

        public static IEnumerator Fade(AudioSource source, float duration, float targetVolume)
        {
            if (!source.isPlaying)
            {
                source.volume = 0.01f;
                source.Play();
            }

            var startVolume = source.volume;

            UnscaledTimeSince timeSinceStart = 0f;

            // ReSharper disable once LoopVariableIsNeverChangedInsideLoop
            while (timeSinceStart < duration)
            {
                source.volume = Mathf.Lerp(startVolume, targetVolume, timeSinceStart / duration);
                yield return null;
            }

            source.volume = targetVolume;

            if (Mathf.Approximately(source.volume, 0f))
            {
                source.Stop();
            }
        }

        public static AudioSourceCallback GetCallback(this AudioSource source)
        {
            var callback = source.GetComponents<AudioSourceCallback>()
                .FirstOrDefault(cb => cb.Source == source);

            if (callback == null)
            {
                callback = source.gameObject.AddComponent<AudioSourceCallback>();
                callback.Source = source;
            }

            return callback;
        }
    }
}