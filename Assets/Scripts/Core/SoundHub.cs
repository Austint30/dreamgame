using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundHub
{

    public enum Sound{
        PlayerMove,
        PlayerJump,
        DenialBackgroundMusic,
        GeneralHappyBackgroundMusic,
        AngerBackgroundMusic,
        IntenseBackgroundMusic,
        PowerupSound,
        PlayerTalking,
        MenuButtonSound,
        EnterSound,
    }

    public static void PlaySound(Sound sound){
        GameObject soundGameObject = new GameObject("Sound");
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        AudioClip audioClip = GetAudioClip(sound);
        audioSource.PlayOneShot(audioClip);
    }

    public static AudioClip GetAudioClip(Sound sound){
        foreach(SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClipArray){
            if(soundAudioClip.sound == sound && soundAudioClip.audioClip != null){
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound");
        return null;
    }
}
