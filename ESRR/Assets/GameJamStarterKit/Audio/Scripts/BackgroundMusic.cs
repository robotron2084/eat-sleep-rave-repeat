using UnityEngine;

namespace GameJamStarterKit.Audio
{
    public class BackgroundMusic : SingletonBehaviour<BackgroundMusic>
    {
        public AudioClipCollection ClipCollection;

        public bool PlayOnStart;

        public bool FadeIn;
        public float FadeInDuration = 2f;

        [Tooltip("This will force the background music to always fade in, even if the last clip was not faded out.")]
        public bool AlwaysFadeIn;

        public bool FadeOut;
        public float FadeOutDuration = 2f;

        public bool CrossFade;

        public float CrossFadeDuration = 2f;

        private AudioSource _currentSource;

        private AudioSource _primarySource;

        private AudioSource _secondarySource;

        private TimeSince _timeUntilFade;

        private void Start()
        {
            // setup primary source.
            _primarySource = gameObject.GetOrAddComponent<AudioSource>();
            SetupSource(_primarySource);
            var callback = gameObject.GetOrAddComponent<AudioSourceCallback>();
            callback.Source = _primarySource;

            // setup secondary source
            if (CrossFade)
            {
                CreateSecondarySource();
            }
            else
            {
                callback.OnStop.AddListener(OnSourceStop);
            }

            // set primary as the current.
            _currentSource = _primarySource;

            if (PlayOnStart)
            {
                PlayCollection(ClipCollection);
            }
        }

        private void CreateSecondarySource()
        {
            _secondarySource = gameObject.AddComponent<AudioSource>();
            SetupSource(_secondarySource);
            var callback = gameObject.AddComponent<AudioSourceCallback>();
            callback.Source = _secondarySource;
        }

        public void PlayCollection(AudioClipCollection clipCollection, float volume = 1f)
        {
            if (volume <= 0f)
            {
                Debug.LogError("[PersistentBackgroundMusic] Volume must be higher than 0f.");
                return;
            }

            _currentSource.loop = false;
            ClipCollection = clipCollection;
            var clip = clipCollection.GetClip();

            if (CrossFade)
            {
                CrossFadeClip(clip, volume);
                return;
            }

            _currentSource.clip = clip;
            if (FadeIn)
            {
                _currentSource.FadeIn(FadeInDuration, volume);
            }
            else
            {
                _currentSource.volume = volume;
                _currentSource.Play();
            }
        }

        private void CrossFadeClip(AudioClip clip, float volume)
        {
            if (_secondarySource == null)
            {
                CreateSecondarySource();
                // we're probably coming from not being a cross fader
                _primarySource.GetCallback().OnStop.RemoveListener(OnSourceStop);
            }

            AudioSource fadeOutSource;
            AudioSource fadeInSource;
            if (_currentSource == _primarySource && _currentSource.isPlaying)
            {
                fadeOutSource = _primarySource;
                fadeInSource = _secondarySource;
            }
            else
            {
                fadeOutSource = _secondarySource;
                fadeInSource = _primarySource;
            }

            fadeOutSource.FadeOut(CrossFadeDuration);

            fadeInSource.clip = clip;
            fadeInSource.FadeIn(CrossFadeDuration, volume);

            _currentSource = fadeInSource;

            _timeUntilFade = -FindTimeUntilFade(clip);
        }

        public void PlayOneShot(AudioClip clip, float volume = 1f, bool looping = false)
        {
            _currentSource.loop = looping;
            if (CrossFade)
            {
                CrossFadeClip(clip, volume);
            }

            var startVolume = FadeIn || AlwaysFadeIn ? 0.01f : volume;

            _currentSource.PlayOneShot(clip, startVolume);
            if (FadeIn || AlwaysFadeIn)
            {
                _currentSource.FadeIn(FadeInDuration, volume);
            }
        }

        private void OnSourceStop(AudioSource source)
        {
            if (!ClipCollection.IsEmpty)
            {
                source.clip = ClipCollection.GetClip();

                if (FadeIn && FadeOut || AlwaysFadeIn)
                {
                    source.FadeIn(FadeInDuration, source.volume);
                }
                else
                {
                    source.Play();
                }
            }
        }

        private void SetupSource(AudioSource source)
        {
            source.loop = false;
            source.spatialize = false;
            source.spatialBlend = 0f;
            source.playOnAwake = false;
        }

        private float FindTimeUntilFade(AudioClip clip)
        {
            return clip.length - FadeOutDuration;
        }

        private void Update()
        {
            if (_timeUntilFade >= 0)
            {
                var clip = ClipCollection.GetClip();
                CrossFadeClip(clip, _currentSource.volume);
            }
        }
    }
}