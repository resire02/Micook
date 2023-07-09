using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  https://pixabay.com/music/search/cafe%20jazz/

/*
Use this code to play sounds:
    AudioHandler aux = FindObjectOfType<AudioHandler>();
    aux.PlaySound("soundName");
*/
public class AudioHandler : MonoBehaviour
{
    public float globalVolume;
    private Dictionary<string, AudioSource> audioIndex;

    private void Start()
    {   
        audioIndex = new Dictionary<string, AudioSource>();

        PlaySound("BackgroundMusic", true);

        audioIndex["BackgroundMusic"].volume = 0.025f;

        // PlaySound("MicrowaveAmbient", true);
    }

    public void PlaySound(string sound, bool looped = false)
    {
        CreateNewSource(sound);
        PlayAudioClip(sound, looped);
    }

    public void StopSound(string sound)
    {
        if(audioIndex.ContainsKey(sound) && audioIndex[sound].isPlaying)
        {
            audioIndex[sound].Stop();
        }
    }

    private bool CreateNewSource(string soundID)
    {
        if(audioIndex.ContainsKey(soundID)) return false;

        AudioSource source = transform.gameObject.AddComponent<AudioSource>() as AudioSource;

        source.transform.SetParent(transform);

        source.volume = globalVolume;

        // Debug.Log("Created New Audio Source");

        audioIndex.Add(soundID, source);

        return true;
    }

    private void PlayAudioClip(string clip, bool looped)
    {
        if(audioIndex[clip].isPlaying) return;
        
        if(audioIndex[clip].clip == null)
            audioIndex[clip].clip = (AudioClip) Resources.Load($"Audio/{clip}");
        
        audioIndex[clip].loop = looped;
        audioIndex[clip].Play();
    }

}
