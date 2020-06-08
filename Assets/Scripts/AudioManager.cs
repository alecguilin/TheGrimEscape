using UnityEngine.Audio;
using System;
using UnityEngine;

/// <AudioManager>
/// Handles the audio of sound clips that may need to be replayed
/// Some audio clips are inherently played on spawn, they need not be included in the Audio Manager
/// </summary>
public class AudioManager : MonoBehaviour
{
    public Sound[] sounds; //Array of sounds
    public static AudioManager instance; //static link

    private void Awake() {

        instance = this;
        foreach(Sound s in sounds) { //initialize Audio sources
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    private void Start() {

    }

    /// <Play>
    /// Call this when you want to play a sound stored in the audio manager
    /// 
    /// </summary>
    /// <param name="name"> The name of the sound clip </param>
    public void Play(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }

    /// <summary>
    /// Call this when you want to get the runtime of a sound clip
    /// </summary>
    /// <param name="name">The name of the sound clip</param>
    /// <returns></returns>
    public float SoundRunTime(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        return s.source.clip.length;
    }
}
