// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;
// using System.Collections.Generic;

// public static class EnemySound
// {

//     public enum Sound{
//         RatSound,
//         RoachSound
//     }

//     private static GameObject oneShotGameObject;
//     private static AudioSource oneShotAudioSource;

//     public static void PlaySound(Sound sound, float volume = 0.07f){
//         if(CanPlaySound(sound)){
//             if(oneShotGameObject == null){
//                 oneShotGameObject = new GameObject("ESound");
//                 oneShotAudioSource = oneShotGameObject.AddComponent<AudioSource>();
//             }
            
//             oneShotAudioSource.volume = SoundHub.sfxVolume;

//             AudioClip audioClip = GetAudioClip(sound);
//             oneShotAudioSource.PlayOneShot(audioClip);
//         }
        
//     }

//     private static bool CanPlaySound(Sound sound){

//         if(sound == Sound.RatSound){
//             float lastTimePlayed1 = 0f;
//                 float playerMoveTimerMax1 = 0.9f;
//                 if(lastTimePlayed1 + playerMoveTimerMax1 < Time.time){
//                     lastTimePlayed1 = Time.time;
//                     return true;
//                 } else {
//                     return false;
//                 }
//         }
//         else if(sound == Sound.RoachSound){
//             float lastTimePlayed1 = 0f;
//                 float playerMoveTimerMax1 = 0.5f;
//                 if(lastTimePlayed1 + playerMoveTimerMax1 < Time.time){
//                     lastTimePlayed1 = Time.time;
//                     return true;
//                 } else {
//                     return false;
//                 }
//         }
//         else{
//             return true;
//         }
//     }

//     public static AudioClip GetAudioClip(Sound sound){
//         foreach(SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClipArray){
//             if(soundAudioClip.soundE == sound && soundAudioClip.audioClip != null){
//                 return soundAudioClip.audioClip;
//             }
//         }
//         Debug.LogError("Sound");
//         return null;
//     }
// }
