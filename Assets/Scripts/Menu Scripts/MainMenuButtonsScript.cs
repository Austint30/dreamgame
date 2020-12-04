using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.Audio;

public class MainMenuButtonsScript : MonoBehaviour
{
    public static float sfxVol = 0.07f;

    public void PlayGame () {
        PlayMenuSound();
        System.Threading.Thread.Sleep(1000);
        SceneManager.LoadScene(2); //will have to change this with where current player last saved if already started game
    }

    public void ContinueScene () {
        PlayMenuSound();
        System.Threading.Thread.Sleep(1000);
        SceneManager.LoadScene(3); //will have to change this with where current player last saved if already started game
    }

    public void QuitGame()
    {
        PlayMenuSound();
        System.Threading.Thread.Sleep(1000);
        Application.Quit();
    }

    public void PlayMenuSound(){
        SoundHub.PlaySound(SoundHub.Sound.MenuButtonSound, sfxVol);
    }

    public void SetMainVolume(float volume){
        AudioSource bgMusic = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
        bgMusic.volume = volume;
        SoundHub.musicVolume = volume;
    }

    public void SetSFXVolume(float volume){
        sfxVol = volume;
        SoundHub.sfxVolume = volume;
        PlayMenuSound();
    }
}