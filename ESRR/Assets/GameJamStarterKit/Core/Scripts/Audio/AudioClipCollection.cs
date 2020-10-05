using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameJamStarterKit
{
    [Serializable]
    public class AudioClipCollection
    {
        [SerializeField]
        private List<AudioClip> Clips = null;

        private AudioClip RandomClip => Clips.RandomItem();

        private int _currentIndex;

        private AudioClip NextClip()
        {
            if (_currentIndex >= Clips.Count)
                _currentIndex = 0;

            return Clips[_currentIndex++];
        }

        [Tooltip("If this collection should return a random clip or not.")]
        public bool RandomizedCollection;

        public AudioClip GetClip(bool forceRandom)
        {
            return forceRandom ? RandomClip : NextClip();
        }

        public AudioClip GetClip()
        {
            return GetClip(RandomizedCollection);
        }

        public bool IsEmpty => Clips.Count == 0;
    }
}