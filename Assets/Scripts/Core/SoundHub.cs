using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;

    public static void PlaySound(Sound sound, float volume = 1f){
        if(CanPlaySound(sound)){
            if(oneShotGameObject == null){
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }
            oneShotAudioSource.volume = volume;
            AudioClip audioClip = GetAudioClip(sound);
            oneShotAudioSource.PlayOneShot(audioClip);
        }
        
    }

    private static bool CanPlaySound(Sound sound){
        switch(sound){
            default:
                return true;
            case Sound.PlayerMove:
                float lastTimePlayed = 0f;
                float playerMoveTimerMax = 0.05f;
                if(lastTimePlayed + playerMoveTimerMax < Time.time){
                    lastTimePlayed = Time.time;
                    return true;
                } else {
                    return false;
                }
        }
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
