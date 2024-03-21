using DG.Tweening;
using UnityEngine;

namespace DancingLineFanmade.Level
{
    public static class AudioManager
    {
        public static void PlayClip(AudioClip clip)
        {
            AudioSource audioSource = new GameObject(clip.name).AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.Play();
            Object.Destroy(audioSource.gameObject, clip.length);
        }

        public static AudioSource PlayClip(AudioClip clip, float volume)
        {
            AudioSource audioSource = new GameObject(clip.name).AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();
            return audioSource;
        }

        public static float Time
        {
            get => Player.Instance.track.time;
        }

        public static float Progress
        {
            get => Player.Instance.track.time / Player.Instance.track.clip.length;
        }

        public static void Stop()
        {
            Player.Instance.track.Stop();
        }

        public static Tween FadeOut(float volume, float duration)
        {
            return Player.Instance.track.DOFade(volume, duration).SetEase(Ease.Linear).OnComplete(new TweenCallback(Stop));
        }
    }
}