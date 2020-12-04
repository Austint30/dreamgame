using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

    private static List<Sound> sfxSounds = new List<Sound>(){
        Sound.PlayerMove,
        Sound.PlayerJump,
        Sound.PowerupSound,
        Sound.PlayerTalking,
        Sound.MenuButtonSound,
        Sound.EnterSound,
    };

    private static List<Sound> musicSounds = new List<Sound>(){
        Sound.DenialBackgroundMusic,
        Sound.GeneralHappyBackgroundMusic,
        Sound.AngerBackgroundMusic,
        Sound.IntenseBackgroundMusic,
    };

    private static GameObject oneShotGameObject;
    private static AudioSource oneShotAudioSource;
    public static float musicVolume = 0.07f;
    public static float sfxVolume = 0.07f;

    public static void PlaySound(Sound sound, float volume = 0.07f){
        if(CanPlaySound(sound)){
            if(oneShotGameObject == null){
                oneShotGameObject = new GameObject("Sound");
                oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
            }

            if(musicSounds.Contains(sound)){
                oneShotAudioSource.volume = musicVolume;
            }
            else if(sfxSounds.Contains(sound)){
                oneShotAudioSource.volume = sfxVolume;
            }

            AudioClip audioClip = GetAudioClip(sound);
            oneShotAudioSource.PlayOneShot(audioClip);
        }
        
    }

    private static bool CanPlaySound(Sound sound){

        if(sound == Sound.PlayerMove){
            float lastTimePlayed = 0f;
            float playerMoveTimerMax = 1f;
            if(lastTimePlayed + playerMoveTimerMax < Time.time){
                lastTimePlayed = Time.time;
                return true;
            } else {
                return false;
            }
        }
        else{
            return true;
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
